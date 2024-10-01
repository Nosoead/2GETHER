namespace _2GETHER
{
    class Status
    {
        public void GetStatusInfo(Player player, IOManager ioManager)
        {
            string[] statusInfo = new string[]
            {
                "상태 보기",
                "",
                "캐릭터의 정보가 표시됩니다.",
                "",
                $"Lv.{player.Level}",
                $"{player.Name} ({player.Job})",
                $"공격력 : {player.Attack}",
                $"방어력 : {player.Defense}",
                $"체  력 : {player.Hp.ToString("N0")}",
                $"Gold : {player.Gold.ToString("N0")} G",
                "",
                "나가시려면 아무키나 눌러주세요."
            };

            ioManager.PrintMessage(statusInfo, true);
            Console.ReadKey(true);
        }
    }
}
