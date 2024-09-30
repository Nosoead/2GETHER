using System.Numerics;

namespace _2GETHER
{
    class QuestInfo
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Func<Item> ItemReward { get; private set; }
        public Func<int> GoldReward { get; private set; }
        public Func<bool> QuestCondition { get; private set; }

        public QuestInfo(string title, string description, Func<Item> itemReward, Func<int> goldReward, Func<bool> questCondition)
        {
            Title = title;
            Description = description;
            ItemReward = itemReward;
            GoldReward = goldReward;
            QuestCondition = questCondition;
        }
    }

    class Quest
    {
        private Random random = new Random();
        private ItemManager itemManager;
        private Player player;
        private List<QuestInfo> quests;


        public Quest(ItemManager itemManager, Player player)
        {
            this.itemManager = itemManager ?? throw new InvalidOperationException("ItemManager cannot be null.");
            this.player = player ?? throw new InvalidOperationException("Player cannot be null.");

            quests = new List<QuestInfo>
            {
                new QuestInfo("장비를 장착해보자!",
                    "현재 당신은 장비를 착용하지 않았습니다.\n" +
                    "인벤토리에 있는 장비를 착용해보세요!\n" +
                    "장비를 착용하면 능력치가 증가합니다.\n" +
                    "더욱 강력한 전투를 위해 장비를 활용하세요!",
                    () => RandomItemDrop(),
                    () => RandomGoldDrop(1, 500),
                    () => IsPlayerEquip(player) > 0),

                new QuestInfo("고블린 슬레이어!",
                    "고블린을 처치하고 돌아오세요!\n" +
                    "고블린은 던전에서 자주 출몰합니다.\n" +
                    "그들의 보물을 가져오는 것이 목표입니다.\n" +
                    "조심하세요! 그들은 매우 교활합니다.",
                    () => RandomItemDrop(),
                    () => RandomGoldDrop(50, 150),
                    () => IsPlayerEquip(player) > 0),

                new QuestInfo("전설의 사냥꾼!",
                    "전설의 사냥꾼이 되어 보상을 받으세요!\n" +
                    "강력한 적들과 맞서 싸워야 합니다.\n" +
                    "당신의 실력을 증명할 기회입니다.\n" +
                    "이제 진정한 사냥꾼으로 거듭나세요!",
                    () => RandomItemDrop(),
                    () => RandomGoldDrop(100, 300),
                    () => IsPlayerEquip(player) > 0),

                new QuestInfo("나는 이제 초싸이언?",
                    "강력한 힘을 얻고 돌아오세요!\n" +
                    "이 퀘스트는 특정 능력치에 도달해야 완료됩니다.\n" +
                    "적을 물리치고 필요한 능력치를 채워야 합니다.\n" +
                    "강해지는 것은 항상 즐거운 일입니다. 힘을 증명하세요!",
                    () => RandomItemDrop(),
                    () => RandomGoldDrop(200, 400),
                    () => IsPlayerEquip(player) > 0),

                new QuestInfo("부자가 될꺼야!",
                    "많은 골드를 획득하세요!\n" +
                    "이 퀘스트는 부자가 되는 길로 이끌어줍니다.\n" +
                    "모든 적을 처치하고 많은 보상을 받으세요.\n" +
                    "당신의 꿈을 이루는 첫 걸음이 될 것입니다!",
                    () => RandomItemDrop(),
                    () => RandomGoldDrop(500, 1000),
                    () => player.Gold > 10000)
            };
        }

        public void QuestList(IOManager ioManager)
        {
            ioManager.PrintMessage("퀘스트 목록", true);

            string[] questTitles = quests.ConvertAll(q => q.Title).ToArray();
            int select = ioManager.PrintMessageWithNumberForSelect(questTitles, true);

            if (select < 1 || select > quests.Count)
            {
                Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                return;
            }

            ShowQuestInfoAndAccept(player, ioManager, quests[select - 1]);
        }

        private void ShowQuestInfoAndAccept(Player player, IOManager ioManager, QuestInfo quest)
        {
            string[] questInfo = new string[]
            {
                "Quest!!",
                "",
                quest.Title,
                "",
                quest.Description,
                "",
                $"퀘스트 조건 충족 여부: {quest.QuestCondition()}",
                "",
                "- 보상 -",
                $"아이템: {quest.ItemReward().eItem}",
                $"골드: {quest.GoldReward()}"
            };

            ioManager.PrintMessage(questInfo, true);

            string[] acceptRejectOptions = { "수락", "거절" };
            int choice = ioManager.PrintMessageWithNumberForSelect(acceptRejectOptions, false);

            if (choice == 1)
            {
                CompleteQuest(player, quest, ioManager);
            }
            else if (choice == 2)
            {
                Console.WriteLine("퀘스트를 거절하셨습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 선택입니다.");
            }
        }

        public void CompleteQuest(Player player, QuestInfo quest, IOManager ioManager)
        {
            if (!quest.QuestCondition())
            {
                ioManager.PrintMessage(new string[] { "퀘스트 완료 조건이 충족되지 않았습니다." }, false);
                return;
            }

            Item droppedItem = quest.ItemReward();
            int goldAmount = quest.GoldReward();

            player.InventoryItems.Add(droppedItem);
            player.AddGold(goldAmount);

            string[] questCompletionInfo = new string[]
            {
                "퀘스트 완료!",
                "당신이 받은 보상:",
                $"아이템: {droppedItem.eItem}",
                $"골드: {goldAmount}"
            };

            ioManager.PrintMessage(questCompletionInfo, false);
        }

        public Item RandomItemDrop()
        {
            if (itemManager.items.Count == 0)
            {
                throw new InvalidOperationException("No items available for drop.");
            }

            int randomIndex = random.Next(0, itemManager.items.Count);
            return itemManager.items[randomIndex];
        }

        public int RandomGoldDrop(int mingold, int maxgold)
        {
            return random.Next(mingold, maxgold + 1);
        }

        public int IsPlayerEquip(Player player)
        {
            int equippedCount = 0;

            foreach (var item in player.InventoryItems)
            {
                if (item.isPlayerEquip)
                {
                    equippedCount++;
                }
            }

            return equippedCount;
        }
    }
}
