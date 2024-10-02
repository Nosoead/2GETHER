namespace _2GETHER
{
    class QuestInfo
    {
        // 퀘스트 제목
        public string Title { get; private set; }
        // 퀘스트 설명
        public string Description { get; private set; }
        // 퀘스트 보상으로 주어질 아이템
        public EquipmentItem ItemReward { get; private set; }
        // 퀘스트 보상으로 주어질 금액
        public int GoldReward { get; private set; }
        // 퀘스트 조건을 확인하기 위한 함수
        public Func<int> QuestCondition { get; private set; }

        // 퀘스트 수락 여부
        public bool IsAccepted { get; set; }
        // 퀘스트 완료 여부
        public bool IsCompleted { get; set; }

        // QuestInfo 생성자
        public QuestInfo(string title, string description, EquipmentItem itemReward, int goldReward, Func<int> questCondition)
        {
            Title = title; // 퀘스트 제목 설정
            Description = description; // 퀘스트 설명 설정
            QuestCondition = questCondition; // 퀘스트 조건 설정
            IsAccepted = false; // 퀘스트 수락 여부 초기화
            IsCompleted = false; // 퀘스트 완료 여부 초기화
            ItemReward = itemReward; // 아이템 보상 설정
            GoldReward = goldReward; // 골드 보상 설정
        }

        // 퀘스트 수락
        public void AcceptQuest()
        {
            IsAccepted = true; // 퀘스트 수락 여부를 true로 설정
        }
    }

    class Quest
    {
        private Random random = new Random(); // 난수 생성기
        private ItemManager itemManager; // 아이템 관리 객체
        private Player player; // 플레이어 객체
        private List<QuestInfo> quests; // 퀘스트 목록
        private List<QuestInfo> acceptedQuests; // 수락한 퀘스트 목록

        // Quest 생성자
        public Quest(ItemManager itemManager, Player player)
        {
            this.itemManager = itemManager ?? throw new InvalidOperationException("ItemManager는 null일 수 없습니다."); // ItemManager가 null일 경우 예외 발생
            this.player = player ?? throw new InvalidOperationException("Player는 null일 수 없습니다."); // Player가 null일 경우 예외 발생

            // 퀘스트 목록 초기화
            quests = new List<QuestInfo>
            {
                new QuestInfo("장비를 장착해보자!",
                    "현재 당신은 장비를 착용하지 않았습니다.\n" +
                    "인벤토리에 있는 장비를 착용해보세요!\n" +
                    "장비를 착용하면 능력치가 증가합니다.\n" +
                    "더욱 강력한 전투를 위해 장비를 활용하세요!",
                    RandomItemDrop(), // 랜덤 아이템 보상
                    RandomGoldDrop(100, 500), // 랜덤 골드 보상
                    () => IsPlayerEquip()), // 퀘스트 조건: 장비 착용 여부

                new QuestInfo("전설의 사냥꾼!",
                    "전설의 사냥꾼이 되어 보상을 받으세요!\n" +
                    "강력한 적들과 맞서 싸워야 합니다.\n" +
                    "당신의 실력을 증명할 기회입니다.\n" +
                    "이제 진정한 사냥꾼으로 거듭나세요!",
                    RandomItemDrop(), // 랜덤 아이템 보상
                    RandomGoldDrop(1000, 2000), // 랜덤 골드 보상
                    () => player.MonsterKills), // 퀘스트 조건: 몬스터 킬 수

                new QuestInfo("나는 이제 초싸이언?",
                    "강력한 힘을 얻고 돌아오세요!\n" +
                    "이 퀘스트는 특정 능력치에 도달해야 완료됩니다.\n" +
                    "적을 물리치고 필요한 능력치를 채워야 합니다.\n" +
                    "강해지는 것은 항상 즐거운 일입니다. 힘을 증명하세요!",
                    RandomItemDrop(), // 랜덤 아이템 보상
                    RandomGoldDrop(2000, 4000), // 랜덤 골드 보상
                    () => ((int)(player.Attack + player.Defense))), // 퀘스트 조건: 능력치 합계

                new QuestInfo("부자가 될꺼야!",
                    "많은 골드를 획득하세요!\n" +
                    "이 퀘스트는 부자가 되는 길로 이끌어줍니다.\n" +
                    "모든 적을 처치하고 많은 보상을 받으세요.\n" +
                    "당신의 꿈을 이루는 첫 걸음이 될 것입니다!",
                    RandomItemDrop(), // 랜덤 아이템 보상
                    RandomGoldDrop(1000, 5000), // 랜덤 골드 보상
                    () => player.Gold) // 퀘스트 조건: 골드 수
            };

            acceptedQuests = new List<QuestInfo>(); // 수락한 퀘스트 초기화
        }

        // 플레이어가 장비를 착용했는지 확인
        public int IsPlayerEquip()
        {
            int equippedCount = 0; // 장비 착용 개수 초기화

            // 플레이어의 장비 인벤토리를 순회
            foreach (var item in player.equipmentInventory)
            {
                if (item.IsPlayerEquip) // 장비가 착용 중인지 확인
                {
                    equippedCount++; // 장비 착용 개수 증가
                }
            }

            return equippedCount > 0 ? 1 : 0; // 착용 중인 장비가 있으면 1, 없으면 0 반환
        }

        // 랜덤 아이템 드랍
        public EquipmentItem RandomItemDrop()
        {
            if (itemManager.equipmentItemList.Count == 0) // 드랍 가능한 아이템이 없을 경우 예외 발생
            {
                throw new InvalidOperationException("드랍 가능한 아이템이 없습니다.");
            }

            int randomIndex = random.Next(0, itemManager.equipmentItemList.Count); // 랜덤 인덱스 생성
            return itemManager.equipmentItemList[randomIndex]; // 랜덤 아이템 반환
        }

        // 랜덤 골드 드랍
        public int RandomGoldDrop(int minGold, int maxGold)
        {
            return random.Next(minGold, maxGold + 1); // 지정된 범위 내에서 랜덤 골드 반환
        }

        // 퀘스트 목록을 표시
        public void QuestList(IOManager ioManager)
        {
            ioManager.PrintMessage("퀘스트 목록!\n", true); // 퀘스트 목록 헤더 출력

            List<QuestInfo> ongoingQuests = acceptedQuests.FindAll(q => !q.IsCompleted); // 진행 중인 퀘스트 목록

            if (ongoingQuests.Count > 0) // 진행 중인 퀘스트가 있을 경우
            {
                ioManager.PrintMessage("진행 중인 퀘스트:", false); // 진행 중인 퀘스트 출력
                foreach (var quest in ongoingQuests) // 진행 중인 퀘스트 순회
                {
                    ioManager.PrintMessage(new string[] // 퀘스트 제목 및 조건 출력
                    {
                        quest.Title,
                        $"조건: [{GetQuestConditionText(quest)}]",
                        ""
                    }, false);
                }
            }
            else
            {
                ioManager.PrintMessage("진행 중인 퀘스트가 없습니다.\n", false); // 진행 중인 퀘스트가 없을 경우 메시지 출력
            }

            // 퀘스트 제목을 배열로 변환
            string[] questTitles = quests.ConvertAll(q =>
                q.IsCompleted ? $"{q.Title} (완료됨)" : q.Title
            ).ToArray();

            int select = ioManager.PrintMessageWithNumberForSelect(questTitles, false); // 퀘스트 선택 메뉴 출력

            if (select < 1 || select > quests.Count) // 잘못된 선택일 경우
            {
                Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요."); // 메시지 출력
                return; // 메서드 종료
            }

            QuestInfo selectedQuest = quests[select - 1]; // 선택한 퀘스트

            if (selectedQuest.IsCompleted) // 이미 완료된 퀘스트일 경우
            {
                ioManager.PrintMessage(new string[] { "이미 완료된 퀘스트입니다.\n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, false);
                Console.ReadKey(true); // 키 입력 대기
            }
            else if (selectedQuest.IsAccepted) // 이미 수락한 퀘스트일 경우
            {
                ShowAcceptedQuestInfo(ioManager, selectedQuest); // 수락한 퀘스트 정보 출력
            }
            else // 새로 수락할 퀘스트일 경우
            {
                Item rewardItem = selectedQuest.ItemReward; // 보상 아이템
                int rewardGold = selectedQuest.GoldReward; // 보상 골드

                ShowQuestInfoAndAccept(player, ioManager, selectedQuest, rewardItem, rewardGold); // 퀘스트 정보 및 수락 처리
            }
        }

        // 퀘스트 조건 텍스트를 반환
        private string GetQuestConditionText(QuestInfo quest)
        {
            int conditionValue = quest.QuestCondition.Invoke(); // 현재 조건값
            int goalValue = GetQuestGoal(quest); // 목표값

            if (quest.Title == "장비를 장착해보자!") // 조건에 따른 텍스트 반환
            {
                return $"장비 착용 {conditionValue} / 1";
            }
            else if (quest.Title == "전설의 사냥꾼!")
            {
                return $"몬스터 킬 {conditionValue} / 50";
            }
            else if (quest.Title == "나는 이제 초싸이언?")
            {
                return $"능력치 {conditionValue} / 100";
            }
            else if (quest.Title == "부자가 될꺼야!")
            {
                return $"골드 {conditionValue} / 10000";
            }
            return $"현재 상태: {conditionValue} / {goalValue}"; // 기본 반환
        }

        // 수락한 퀘스트 정보를 표시
        private void ShowAcceptedQuestInfo(IOManager ioManager, QuestInfo quest)
        {
            string rewardItemInfo = quest.ItemReward != null ? quest.ItemReward.eItem.ToString() : "없음"; // 보상 아이템 정보

            string[] questInfo = new string[] // 퀘스트 정보 배열
            {
                "진행 중인 퀘스트",
                "",
                quest.Title,
                "",
                quest.Description,
                "",
                $"퀘스트 조건: [{GetQuestConditionText(quest)}]",
                "",
                "- 보상 -",
                $"아이템: {rewardItemInfo}",
                $"골드: {quest.GoldReward}",
                "",
                "보상을 받으시겠습니까?"
            };

            ioManager.PrintMessage(questInfo, true); // 퀘스트 정보 출력

            string[] options = { "보상 받기", "돌아가기" }; // 옵션 배열
            int response = ioManager.PrintMessageWithNumberForSelect(options, false); // 사용자 선택

            if (response == 1) // 보상 받기를 선택한 경우
            {
                CompleteQuest(player, quest, ioManager); // 퀘스트 완료 처리
            }
            else if (response == 2) // 돌아가기를 선택한 경우
            {
                ioManager.PrintMessage(new string[] { "취소하셨습니다. \n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, true);
                Console.ReadKey(true); // 키 입력 대기
            }
        }

        // 퀘스트 완료 처리
        public void CompleteQuest(Player player, QuestInfo quest, IOManager ioManager)
        {
            int conditionValue = quest.QuestCondition.Invoke(); // 현재 조건값
            int goalValue = GetQuestGoal(quest); // 목표값

            if (conditionValue != goalValue) // 조건이 충족되지 않은 경우
            {
                ioManager.PrintMessage(new string[] { $"퀘스트 완료 조건이 충족되지 않았습니다.\n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, true);
                Console.ReadKey(true); // 키 입력 대기
                return; // 메서드 종료
            }

            quest.IsCompleted = true; // 퀘스트 완료
            acceptedQuests.Remove(quest); // 수락한 퀘스트 목록에서 제거

            EquipmentItem selectItem = quest.ItemReward; // 보상 아이템

            player.equipmentInventory.Add(quest.ItemReward); // 아이템 인벤토리에 추가
            selectItem.AddCount(); // 아이템 수 증가
            if (!player.equipmentInventory.Contains(selectItem))
            {
                player.equipmentInventory.Add(selectItem); // 아이템이 없다면 추가
            }
            player.AddGold(quest.GoldReward); // 플레이어에게 골드 추가

            string[] questCompletionInfo = new string[] // 퀘스트 완료 정보 배열
            {
                "퀘스트 완료!",
                "",
                "당신이 받은 보상",
                "",
                $"아이템: {quest.ItemReward.eItem}",
                $"골드: {quest.GoldReward} G"
            };

            ioManager.PrintMessage(questCompletionInfo, true); // 퀘스트 완료 메시지 출력
            Console.ReadKey(true); // 키 입력 대기
        }

        // 퀘스트 목표값을 반환
        private int GetQuestGoal(QuestInfo quest)
        {
            if (quest.Title == "장비를 장착해보자!") // 목표값 반환
            {
                return 1;
            }
            else if (quest.Title == "부자가 될꺼야!")
            {
                return 10000;
            }
            else if (quest.Title == "전설의 사냥꾼!")
            {
                return 50;
            }
            else if (quest.Title == "나는 이제 초싸이언?")
            {
                return 100;
            }
            return 1; // 기본 반환
        }

        // 퀘스트 정보 및 수락 처리
        private void ShowQuestInfoAndAccept(Player player, IOManager ioManager, QuestInfo quest, Item rewardItem, int rewardGold)
        {
            string rewardItemInfo = rewardItem != null ? rewardItem.eItem.ToString() : "없음"; // 보상 아이템 정보

            string[] questInfo = new string[] // 퀘스트 정보 배열
            {
                "퀘스트 수락",
                "",
                quest.Title,
                "",
                quest.Description,
                "",
                $"퀘스트 조건: [{GetQuestConditionText(quest)}]",
                "",
                "- 보상 -",
                $"아이템: {rewardItemInfo}",
                $"골드: {rewardGold}",
                "",
                "퀘스트를 수락하시겠습니까?"
            };

            ioManager.PrintMessage(questInfo, true); // 퀘스트 정보 출력

            string[] options = { "퀘스트 수락", "취소" }; // 옵션 배열
            int response = ioManager.PrintMessageWithNumberForSelect(options, false); // 사용자 선택

            if (response == 1) // 퀘스트 수락을 선택한 경우
            {
                quest.AcceptQuest(); // 퀘스트 수락
                acceptedQuests.Add(quest); // 수락한 퀘스트 목록에 추가
                ioManager.PrintMessage(new string[] { $"{quest.Title} 퀘스트를 수락하셨습니다.\n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, true);
                Console.ReadKey(true); // 키 입력 대기
            }
            else if (response == 2) // 취소를 선택한 경우
            {
                ioManager.PrintMessage(new string[] { "퀘스트 수락이 취소되었습니다.\n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, true);
                Console.ReadKey(true); // 키 입력 대기
            }
        }
    }
}
