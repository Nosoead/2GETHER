namespace _2GETHER
{
    class Intro
    {
        public string SetPlayerName(Player player, IOManager ioManager)
        {
            string[] setPlayerName = new string[]
            {
                "스파르타 던전에 오신 여러분을 환영합니다.",
                "원하시는 이름을 설정해주세요",
                ">>"
            };

            ioManager.PrintMessage(setPlayerName, false);
            string playerName = Console.ReadLine();
            player.ChangeName(playerName);
            return player.Name;
        }

        public void SetPlayerJob(Player player, IOManager ioManager, ItemManager itemManager)
        {
            string[] jobIntro = new string[]
            {
                "스파르타 던전 RPG에는 3가지의 직업이 있습니다.",
                "아래 3가지의 직업 중에 하나를 선택해주세요.",
                ""
            };

            ioManager.PrintMessage(jobIntro, true);
            string[] jobInfo = new string[]
            {
                "전사",
                "마법사",
                "궁수",
            };

            StartPotion(player, itemManager);
            while (true)
            {
                int select = ioManager.PrintMessageWithNumberForSelect(jobInfo, false);

                switch (select)
                {
                    case 1:
                        player.ChangeJob(EJob.전사);
                        Console.WriteLine($"{player.Job}를 선택하셨습니다.");
                        return;

                    case 2:
                        player.ChangeJob(EJob.궁수);
                        Console.WriteLine($"{player.Job}를 선택하셨습니다.");
                        return;

                    case 3:
                        player.ChangeJob(EJob.마법사);
                        Console.WriteLine($"{player.Job}를 선택하셨습니다.");
                        return;

                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                        break;
                }
            }
        }

        public void StartPotion(Player player, ItemManager itemManager)
        {
            for (int i = 0; i < player.Potions; i++)
            {
                ConsumableItem startPotion = itemManager.consumableItemList[0];
                startPotion.AddCount();
                if (!player.consumableInventory.Contains(startPotion))
                    player.consumableInventory.Add(startPotion);
            }
        }
    }
}
