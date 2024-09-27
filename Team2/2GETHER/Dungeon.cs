namespace _2GETHER
{
    public class Dungeon
    {
        IOManager iomanager = new IOManager();
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
        public void StartBattle(Player player)
        {
            int choice;
            
            iomanager.PrintMessage("전투 시작~!\n\n", true);
            
            CreateMonster();
            foreach (Monster monster in monsters)
            {
                Console.WriteLine(monster.GetMonsterInfo());
            }
            iomanager.PrintMessage("[내정보]", false);
            string[] str =
            {
                $"Lv.{player.level}  {player.name}  ({player.job})",
                $"HP {player.hp}/100",
                "1. 공격\n0. 도망가기\n",
                "원하는 행동을 입력해 주세요."
            };
            iomanager.PrintMessage(str, false);
            string[] test = { "행동", "나가기" };
            int select = iomanager.PrintMessageWithNumberForSelect(test, true);
            iomanager.PrintMessage(select.ToString(), false);
            
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