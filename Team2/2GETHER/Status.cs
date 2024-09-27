namespace _2GETHER
{
    class Status
    {
        public string GetStatusInfo(Player player)
        {
            string[] statusinfo = new string[]
            {
                "상태 보기",
                "캐릭터의 정보가 표시됩니다.",
                "",
                $"{player.Name} ({player.Job})",
                $"공격력 : {player.Attack}",
                $"방어력 : {player.Defense}",
                $"체  력 : {player.Hp}",
                $"Gold : {player.Gold}",
                ""                
            };

            return string.Join("\n", statusinfo);
        }
    }
}
