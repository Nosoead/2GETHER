using System.Xml.Linq;

namespace _2GETHER
{
    public class Dungeon
    { 
        
        
        public void CreateMonster()
        {
            List<EMonsterName> monsterList = new List<EMonsterName>();
            Random random = new Random();
            int monstercount = random.Next(1, 5);
            for(int i = 0; i < monstercount; i++)
            {
                int index = random.Next(monsterList.Count);
                
                
            }
            Console.WriteLine($"{monstercount}마리의 몬스터가 등장했습니다.");
        }
        public void StartBattle()
        {
            Monster mosnter= new Monster();
            Player player= new Player();
            Console.WriteLine($"전투 시작~! 플레이어 정보: 체력({player.hp}), 공격력({player.attack})");

            

        }
        
    }
}
