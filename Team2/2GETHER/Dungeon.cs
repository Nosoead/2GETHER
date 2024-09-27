namespace _2GETHER
{
    class Dungeon
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
            int choice;
            Player player = new Player();
            Console.WriteLine("전투 시작~!\n\n");
            CreateMonster();

            foreach (Monster monster in monsters)
            {
                Console.WriteLine(monster.GetMonsterInfo());
            }
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}  {player.Name}  ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine("1. 공격\n0. 도망가기\n");
            Console.WriteLine("원하는 행동을 입력해 주세요.");
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 0 || choice > 1))
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.\n");
            }
            if (choice == 0)
            {
                //도망갈때 나오는 대사 입력
            }
            else
            {
                //플레이어가 공격하는 턴
            }

        }

    }
}





