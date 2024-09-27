using System;
using System.Threading;


namespace _2GETHER
{

    public class Dungeon
    {
        private List<Monster> monsters = new List<Monster>();

        public void CreateMonster()
        {
            Random random = new Random();
            int monsterCount = random.Next(1, 5);

            for (int i = 0; i < monsterCount; i++)
            {
                Monster newMonster = GenerateRandomMonster(random.Next(1, 5));
                monsters.Add(newMonster);
            }
        }

        private Monster GenerateRandomMonster(int monsterType)
        {
            switch (monsterType)
            {
                case 1:
                    return new Goblin();
                case 2:
                    return new Oak();
                case 3:
                    return new Ooger();
                case 4:
                    return new GoblinKing();
                default:
                    throw new ArgumentException("유효하지 않은 몬스터 타입입니다.");
            }
        }

        public void StartBattle()
        {
            Console.WriteLine("전투 시작!");
            CreateMonster();

            foreach (Monster monster in monsters)
            {
                Console.WriteLine(monster.GetMonsterInfo());
            }
        }
    }
}





