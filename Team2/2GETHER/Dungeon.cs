namespace _2GETHER
{
    class Dungeon
    {
        Monster monster = new Monster();
        IOManager iomanager = new IOManager();

        private List<Monster> monsters = new List<Monster>();
        public void CreateMonster()
        {
            monsters.Clear();
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
        public void StartBattle(Player player)
        {
            int count = 0;
            CreateMonster();
            int select;

            string[] monsterMessages = new string[monsters.Count];

            for (int i = 0; i < monsters.Count; i++)
            {
                monsterMessages[i] = monsters[i].GetMonsterInfo();
            }

            iomanager.PrintMessage("전투시작!\n", true);

            iomanager.PrintMessage(monsterMessages, false);

            string[] battleInfo =
            {
                "",
                "[내정보]",
                "",
                $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                $"HP {player.Hp}/100"
            };
            iomanager.PrintMessage(battleInfo, false);

            string[] atkchoice = { "공격", "도망가기" };
            select = iomanager.PrintMessageWithNumberForSelect(atkchoice, false);
            if (select == 2)
            {
                return;
            }
            select = iomanager.PrintMessageWithNumberForSelect(monsterMessages, true);

            while (player.Hp > 0 && monsters.Count > 0)
            {
                if (count % 2 == 0)
                {
                    iomanager.PrintMessage(monsters[select - 1].MonsterDamageTaken(player));
                    Console.WriteLine("0. 취소");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        iomanager.PrintMessage(player.PlayerDamageTaken(monsters[i]));
                    }
                    Console.ReadKey();
                }
                count++;
            }





        }
    }
}