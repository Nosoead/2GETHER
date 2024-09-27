namespace _2GETHER
{
    class Dungeon
    {   
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
            CreateMonster();
            int select;
            string[] monsterMessages = new string[monsters.Count];
            for (int i = 0; i < monsters.Count; i++)
            {
                monsterMessages[i] = monsters[i].GetMonsterInfo();
            }
            iomanager.PrintMessage("전투 시작~!\n", true);
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

            string[] atkchoice = {"공격", "도망가기"};
            select=iomanager.PrintMessageWithNumberForSelect(atkchoice, false);
            if(select == 1)
            {

            }
            else
            {
                
            }
        }
    }
}