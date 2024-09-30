namespace _2GETHER
{
    class Dungeon
    {
        //public void PrintMonsterStatus(Monster monster, IOManager ioManager)
        //{
        //    string[] monsterStatus = monster.monsters
        //        .Select(m => $"Lv. {m.Level} {m.Name} {(m.Hp == 0 ? "Dead" : $"HP {m.Hp}")}")
        //        .ToArray();

        //    ioManager.PrintMessage(monsterStatus, false);
        //}

        public string[] PlayerTurn(Player player, Monster monster, int selectNum)
        {
            if (monster.monsters[selectNum].Hp == 0)
            {
                return new string[]
                {
                    $"{monster.monsters[selectNum].Name} 은(는) 이미 처치되었습니다!",
                    "",
                    "0. 다음",
                    ">>",
                    ""
                };
            }
            double damage = player.AttackWithEffects();

            double previousHp = monster.monsters[selectNum].Hp;

            monster.monsters[selectNum].MonsterDamageTaken(damage);

            string hpInfo = (monster.monsters[selectNum].Hp == 0) ? "Dead" : monster.monsters[selectNum].Hp.ToString();

            string criticalHitMessage = (damage > player.Attack) ? "치명타 공격!!" : "";
            string attackMessage = (damage == 0)
             ? $"{monster.monsters[selectNum].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다."
             : $"{monster.monsters[selectNum].Name} 을(를) 맞췄습니다. [ 데미지 : {damage} ] " + criticalHitMessage;

            string[] playerTurn = new string[]
            {
                "Battle!!",
                "",
                $"Lv.{player.Level} {player.Name} 의 공격!",
                attackMessage,
                "",
                $"Lv.{monster.monsters[selectNum].Level} {monster.monsters[selectNum].Name}",
                $"HP {previousHp} -> {hpInfo}",
                "",
                "",
                ">>",
                ""
            };
            return playerTurn;
        }
        public string[] MonsterTurn(Player player, Monster monster, int selectNum)
        {
            double damage = monster.monsters[selectNum].Attack;

            double previousHp = player.Hp;

            player.PlayerDamageTaken(damage);

            double currentHp = player.Hp;

            string[] monsterTurn = new string[]
            {
                "Battle!!",
                "",
                $"Lv.{monster.monsters[selectNum].Level} {monster.monsters[selectNum].Name} 의 공격!",
                "",
                $"Lv.{player.Level} {player.Name} 을(를) 맞췄습니다. [데미지 : {damage} ]",
                $"HP {previousHp} ->{currentHp}",
                "",
                "0. 다음",
                "",
                ">>",
                ""
            };
            return monsterTurn;
        }

        public void StartBattle(Player player, Monster monster, IOManager ioManager)
        {
            monster.CreateMonster();

            int count = 0;
            int select;

            string[] randomMonsters = new string[monster.monsters.Count];
            for (int i = 0; i < monster.monsters.Count; i++)
            {
                randomMonsters[i] = $"Lv. {monster.monsters[i].Level} {monster.monsters[i].Name} HP {monster.monsters[i].Hp}";
            }

            ioManager.PrintMessage("전투시작!\n", true);
            ioManager.PrintMessage(randomMonsters, false);

            string[] battleInfo =
            {
                "\n[내정보]\n",
                $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                $"HP {player.Hp}/{player.MaxHp}\n"
            };
            ioManager.PrintMessage(battleInfo, false);

            string[] atkchoice = { "공격", "도망가기" };
            select = ioManager.PrintMessageWithNumberForSelect(atkchoice, false);
            if (select == 2)
            {
                return;
            }

            while (player.Hp > 0 && monster.monsters.Count > 0)
            {
                battleInfo = new string[]
                {
                    "\n[내정보]\n",
                    $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                    $"HP {player.Hp}/{player.MaxHp}\n"
                };
                ioManager.PrintMessage(battleInfo, false);
                for (int i = 0; i < monster.monsters.Count; i++)
                {
                    string monsterStatus = monster.monsters[i].Hp == 0 ? "Dead" : $"HP {monster.monsters[i].Hp}";
                    randomMonsters[i] = $"Lv. {monster.monsters[i].Level} {monster.monsters[i].Name} {monsterStatus}";
                }
                if (count % 2 == 0)
                {
                    select = ioManager.PrintMessageWithNumberForSelect(randomMonsters, true);

                    ioManager.PrintMessage(battleInfo, false);
                    ioManager.PrintMessage(PlayerTurn(player, monster, select - 1), false);

                    Console.ReadKey();

                    
                    if (monster.monsters[select - 1].Hp == 0)
                    {
                        ioManager.PrintMessage($"{monster.monsters[select - 1].Name} 은(는) 이미 처치되었습니다. 다른 몬스터를 선택하세요.\n", false);
                        continue;
                    }
                    if (monster.monsters.Count == 0)
                    {
                        ioManager.PrintMessage($"던전에서 몬스터 {monster.monsters.Count}마리를 잡았습니다.\n", false);
                        ioManager.PrintMessage(battleInfo, false);                  //필요없는 부분
                        Console.ReadKey();
                    }
                }
                else
                {
                    for (int i = 0; i < monster.monsters.Count; i++)
                    {
                        if (monster.monsters[i].Hp > 0)
                        {
                            Console.Clear();
                            ioManager.PrintMessage(MonsterTurn(player, monster, i), true);
                            Console.ReadKey();
                        }
                    }
                }
                count++;
            }
        }
    }
}