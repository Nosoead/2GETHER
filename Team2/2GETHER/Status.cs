using System.Reflection.Emit;
using System.Xml.Linq;

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
                "\n",
                $"{player.name} ({player.job})",
                "",
                "",
                "",
                "",










            };

            return string.Join(",", statusinfo);
        }
    }
}
