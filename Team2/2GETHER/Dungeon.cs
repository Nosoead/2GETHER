namespace _2GETHER
{
    class Dungeon
    {

        public string[] PlayerTurn(Player player, Monster monster, int selectNum)
        {
            double damage = player.AttackWithEffects();

            double previousHp = monster.Hp;

            monster.MonsterDamageTaken(damage);

            string hpInfo = (monster.Hp == 0) ? "Dead" : monster.Hp.ToString();

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
                "0. 다음",
                "",
                ">>",
                ""
            };
            return playerTurn;
        }




        public void StartBattle(Player player, Monster monster, IOManager ioManager)
        {
            monster.CreateMonster();

            int count = 0;
            int select;
            string[] randomMonsters = new string[monster.monsters.Count];
            for (int i = 0; i < monster.monsters.Count; i++)
            {
                randomMonsters[i] = monster.monsters[i].GetMonsterInfo();
            }

            ioManager.PrintMessage("전투시작!\n", true);
            ioManager.PrintMessage(randomMonsters, false);

            string[] battleInfo =
            {
                "\n[내정보]\n",
                $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                $"HP {player.Hp}/100\n"
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
                if (count % 2 == 0)
                {
                    select = ioManager.PrintMessageWithNumberForSelect(randomMonsters, true);
                    ioManager.PrintMessage(battleInfo, false);
                    ioManager.PrintMessage(PlayerTurn(player, monster, select-1), false);

                    ioManager.PrintMessage("0. 취소");
                    Console.ReadKey();

                    int monstersDefeated = 0;
                    if (monster.monsters[select - 1].Hp <= 0)
                    {
                        ioManager.PrintMessage($"몬스터 {monster.monsters[select - 1].Name}를 처치했습니다.", false);
                        monstersDefeated++;
                        monster.monsters.RemoveAt(select - 1);
                    }
                    if (monster.monsters.Count == 0)
                    {
                        ioManager.PrintMessage($"던전에서 몬스터 {monstersDefeated}마리를 잡았습니다.\n", false);
                        ioManager.PrintMessage(battleInfo, false);
                    }
                }
                else
                {
                    for (int i = 0; i < monster.monsters.Count; i++)
                    {
                        Console.Clear();
                        ioManager.PrintMessage(PlayerTurn(player, monster, select -1), true);
                        Console.ReadKey();
                    }
                }
                count++;
            }
            if (player.Hp <= 0)
            {
                // 플레이어가 죽은 상태 표시
            }
            else if (monster.monsters.Count == 0)
            {
                // 전투승리 표시
            }
        }
    }
}