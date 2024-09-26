using System;
using System.Threading;


namespace _2GETHER
{

    public class Dungeon
    {
        public void CreateMonster()
        {
            Random random = new Random();
            int monsterCount = random.Next(1, 5);
            List<EMonsterName> monsters = new List<EMonsterName>();
            for (int i = 0; i < monsterCount; i++)
            {
                EMonsterName randomMonster = (EMonsterName)random.Next(Enum.GetValues(typeof(EMonsterName)).Length);
                monsters.Add(randomMonster);
            }
            foreach (EMonsterName monster in monsters)
            {
                Console.WriteLine(monster);
            }
        }
        public void StartBattle()
        { 
            Console.WriteLine($"전투 시작~!");
            CreateMonster();          
        }


    }    
}





