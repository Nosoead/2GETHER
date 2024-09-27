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
                $"{player.name} ({player.job})",
                $"공격력 : {player.attack}",
                $"방어력 : {player.defense}",
                $"체  력 : {player.hp}",
                $"Gold : {player.gold}",
                "",
                "0. 나가기",
                "",
                "원하시는 행동을 입력해주세요.",
                ">>"
            };

            return string.Join("\n", statusinfo);
        }
    }
}
