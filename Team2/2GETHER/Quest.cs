namespace _2GETHER
{
    class QuestInfo
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public EquipmentItem ItemReward { get; private set; }
        public int GoldReward { get; private set; }
        public Func<int> QuestCondition { get; private set; }

        public bool IsAccepted { get; set; }
        public bool IsCompleted { get; set; }

        public QuestInfo(string title, string description, EquipmentItem itemReward, int goldReward, Func<int> questCondition)
        {
            Title = title;
            Description = description;
            QuestCondition = questCondition;
            IsAccepted = false;
            IsCompleted = false;
            ItemReward = itemReward;
            GoldReward = goldReward;
        }

        public void AcceptQuest()
        {
            IsAccepted = true;
        }
    }

    class Quest
    {
        private Random random = new Random();
        private ItemManager itemManager;
        private Player player;
        private List<QuestInfo> quests;
        private List<QuestInfo> acceptedQuests;

        public Quest(ItemManager itemManager, Player player)
        {
            this.itemManager = itemManager ?? throw new InvalidOperationException("ItemManager는 null일 수 없습니다.");
            this.player = player ?? throw new InvalidOperationException("Player는 null일 수 없습니다.");

            quests = new List<QuestInfo>
            {
                new QuestInfo("장비를 장착해보자!",
                    "현재 당신은 장비를 착용하지 않았습니다.\n" +
                    "인벤토리에 있는 장비를 착용해보세요!\n" +
                    "장비를 착용하면 능력치가 증가합니다.\n" +
                    "더욱 강력한 전투를 위해 장비를 활용하세요!",
                    RandomItemDrop(),
                    RandomGoldDrop(100, 500),
                    () => IsPlayerEquipped()),

                new QuestInfo("전설의 사냥꾼!",
                    "전설의 사냥꾼이 되어 보상을 받으세요!\n" +
                    "강력한 적들과 맞서 싸워야 합니다.\n" +
                    "당신의 실력을 증명할 기회입니다.\n" +
                    "이제 진정한 사냥꾼으로 거듭나세요!",
                    RandomItemDrop(),
                    RandomGoldDrop(1000, 2000),
                    () => player.MonsterKills),

                new QuestInfo("나는 이제 초싸이언?",
                    "강력한 힘을 얻고 돌아오세요!\n" +
                    "이 퀘스트는 특정 능력치에 도달해야 완료됩니다.\n" +
                    "적을 물리치고 필요한 능력치를 채워야 합니다.\n" +
                    "강해지는 것은 항상 즐거운 일입니다. 힘을 증명하세요!",
                    RandomItemDrop(),
                    RandomGoldDrop(2000, 4000),
                    () => ((int)(player.Attack + player.Defense))),

                new QuestInfo("부자가 될꺼야!",
                    "많은 골드를 획득하세요!\n" +
                    "이 퀘스트는 부자가 되는 길로 이끌어줍니다.\n" +
                    "모든 적을 처치하고 많은 보상을 받으세요.\n" +
                    "당신의 꿈을 이루는 첫 걸음이 될 것입니다!",
                    RandomItemDrop(),
                    RandomGoldDrop(1000, 5000),
                    () => player.Gold)
            };

            acceptedQuests = new List<QuestInfo>();
        }

        public int IsPlayerEquipped()
        {
            int equippedCount = 0;

            foreach (var item in player.equipmentInventory)
            {
                if (item.IsPlayerEquip)
                {
                    equippedCount++;
                }
            }

            return equippedCount > 0 ? 1 : 0;
        }

        public EquipmentItem RandomItemDrop()
        {
            if (itemManager.equipmentItemList.Count == 0)
            {
                throw new InvalidOperationException("드랍 가능한 아이템이 없습니다.");
            }

            int randomIndex = random.Next(0, itemManager.equipmentItemList.Count);
            return itemManager.equipmentItemList[randomIndex];
        }

        public int RandomGoldDrop(int minGold, int maxGold)
        {
            return random.Next(minGold, maxGold + 1);
        }

        public void QuestList(IOManager ioManager)
        {
            ioManager.PrintMessage("퀘스트 목록!\n", true);

            List<QuestInfo> ongoingQuests = acceptedQuests.FindAll(q => !q.IsCompleted);

            if (ongoingQuests.Count > 0)
            {
                ioManager.PrintMessage("진행 중인 퀘스트:", false);
                foreach (var quest in ongoingQuests)
                {
                    ioManager.PrintMessage(new string[]
                    {
                        quest.Title,
                        $"조건: [{GetQuestConditionText(quest)}]",
                        ""
                    }, false);
                }
            }
            else
            {
                ioManager.PrintMessage("진행 중인 퀘스트가 없습니다.\n", false);
            }

            string[] questTitles = quests.ConvertAll(q =>
                q.IsCompleted ? $"{q.Title} (완료됨)" : q.Title
            ).ToArray();

            int select = ioManager.PrintMessageWithNumberForSelect(questTitles, false);

            if (select < 1 || select > quests.Count)
            {
                Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                return;
            }

            QuestInfo selectedQuest = quests[select - 1];

            if (selectedQuest.IsCompleted)
            {
                ioManager.PrintMessage(new string[] { "이미 완료된 퀘스트입니다.\n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, false);
                Console.ReadKey(true);
            }
            else if (selectedQuest.IsAccepted)
            {
                ShowAcceptedQuestInfo(ioManager, selectedQuest);
            }
            else
            {
                Item rewardItem = selectedQuest.ItemReward;
                int rewardGold = selectedQuest.GoldReward;

                ShowQuestInfoAndAccept(player, ioManager, selectedQuest, rewardItem, rewardGold);
            }
        }

        private string GetQuestConditionText(QuestInfo quest)
        {
            int conditionValue = quest.QuestCondition.Invoke();
            int goalValue = GetQuestGoal(quest);

            if (quest.Title == "장비를 장착해보자!")
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
            return $"현재 상태: {conditionValue} / {goalValue}";
        }

        private void ShowAcceptedQuestInfo(IOManager ioManager, QuestInfo quest)
        {
            string rewardItemInfo = quest.ItemReward != null ? quest.ItemReward.eItem.ToString() : "없음";

            string[] questInfo = new string[]
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

            ioManager.PrintMessage(questInfo, true);

            string[] options = { "보상 받기", "돌아가기" };
            int response = ioManager.PrintMessageWithNumberForSelect(options, false);

            if (response == 1)
            {
                CompleteQuest(player, quest, ioManager);
            }
            else if (response == 2)
            {
                ioManager.PrintMessage(new string[] { "취소하셨습니다. \n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, true);
                Console.ReadKey(true);
            }
        }

        public void CompleteQuest(Player player, QuestInfo quest, IOManager ioManager)
        {
            int conditionValue = quest.QuestCondition.Invoke();
            int goalValue = GetQuestGoal(quest);

            if (conditionValue != goalValue)
            {
                ioManager.PrintMessage(new string[] { $"퀘스트 완료 조건이 충족되지 않았습니다.\n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, true);
                Console.ReadKey(true);
                return;
            }

            quest.IsCompleted = true;
            acceptedQuests.Remove(quest);

            EquipmentItem selectItem = quest.ItemReward;

            player.equipmentInventory.Add(quest.ItemReward);
            selectItem.AddCount();
            if (!player.equipmentInventory.Contains(selectItem))
            {
                player.equipmentInventory.Add(selectItem);
            }
            player.AddGold(quest.GoldReward);

            string[] questCompletionInfo = new string[]
            {
                "퀘스트 완료!",
                "",
                "당신이 받은 보상",
                "",
                $"아이템: {quest.ItemReward.eItem}",
                $"골드: {quest.GoldReward} G"
            };

            ioManager.PrintMessage(questCompletionInfo, true);
            Console.ReadKey(true);
        }

        private int GetQuestGoal(QuestInfo quest)
        {
            if (quest.Title == "장비를 장착해보자!")
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
            return 1;
        }

        private void ShowQuestInfoAndAccept(Player player, IOManager ioManager, QuestInfo quest, Item rewardItem, int rewardGold)
        {
            string rewardItemInfo = rewardItem != null ? rewardItem.eItem.ToString() : "없음";

            string[] questInfo = new string[]
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

            ioManager.PrintMessage(questInfo, true);

            string[] options = { "퀘스트 수락", "취소" };
            int response = ioManager.PrintMessageWithNumberForSelect(options, false);

            if (response == 1)
            {
                quest.AcceptQuest();
                acceptedQuests.Add(quest);
                ioManager.PrintMessage(new string[] { $"{quest.Title} 퀘스트를 수락하셨습니다.\n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, true);
                Console.ReadKey(true);
            }
            else if (response == 2)
            {
                ioManager.PrintMessage(new string[] { "퀘스트 수락이 취소되었습니다.\n아무키나 눌러주시면 메인화면으로 이동하겠습니다." }, true);
                Console.ReadKey(true);
            }
        }
    }
}
