namespace _2GETHER
{
    class Quest
    {
        public void QuestList(Player player, IOManager ioManager)
        {
            ioManager.PrintMessage("Quest!!", false);

            string[] questList = new string[]
            {
                "장비를 장착해보자",
                "고블린 슬레이어",
                "전설의 사냥꾼",
                "나는 이제 초싸이언?",
                "부자가 될꺼야!"
            };

            int select = ioManager.PrintMessageWithNumberForSelect(questList, false);

            switch (select)
            {
                case 1:
                    string[] questInfo1 = new string[]
                    {
                        "Quest!!",
                        "",
                        "장비를 장착해보자",
                        "",
                        "현재 당신은 장비를 착용하지 않았습니다.",
                        "더욱 강해지기 위해 인벤토리에 있는 장비를 착용해보세요!",
                        "",
                        $"- 장비 착용 ({player.EquippedWeapon}/1)",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",
                        "",

                    };

                    ioManager.PrintMessage(questInfo1, false);
                    return;

                case 2:
                    player.ChangeJob(EJob.마법사);
                    Console.WriteLine($"{player.Job}를 선택하셨습니다.");
                    return;

                case 3:
                    player.ChangeJob(EJob.궁수);
                    Console.WriteLine($"{player.Job}를 선택하셨습니다.");
                    return;

                case 4:


                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
                    break;
            }
        }
    }
}
