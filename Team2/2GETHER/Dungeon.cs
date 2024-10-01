namespace _2GETHER
{
    class Dungeon
    {
        int Count = 0;
        int DeadMonsterCount;
        public int QuestDeadMonsterCount { get; private set; }
        public void StartBattle(Player player, Monster monster, IOManager ioManager)
        {
            DeadMonsterCount = 0;
            monster.CreateMonster();
            GetRandomMonsterInfo(monster, ioManager);


            string[] battleInfo =
            {
                "\n[내정보]\n",
                $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                $"HP {player.Hp}/{player.MaxHp}",
                $"MP {player.Mp}/{player.MaxMp}\n"
            };
            ioManager.PrintMessage(battleInfo, false);

            while (player.Hp > 0 && monster.monsters.Count > DeadMonsterCount)
            {
                ExcuteTurn(player, monster, ioManager);
            }

            if (monster.monsters.Count == DeadMonsterCount)
            {
                BattleResult(1, player, monster, ioManager);
                return;
            }
            if (player.Hp == 0)
            {
                BattleResult(2, player, monster, ioManager);
                return;
            }

        }

        public void ExcuteTurn(Player player, Monster monster, IOManager ioManager)
        {
            int monSelect;
            int atkSelect;
            int skillSelect = 0;

            if (Count % 2 == 0)
            {
                string[] atkChoice = { "공격", "스킬" };


                string[] battleInfo =
                {
                    "\n[내정보]\n",
                    $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                    $"HP {player.Hp}/{player.MaxHp}",
                    $"MP {player.Mp}/{player.MaxMp}\n"
                };

                atkSelect = ioManager.PrintMessageWithNumberForSelect(atkChoice, false);

                string[] randomMonstersInfo = GetRandomMonsterInfo(monster, ioManager);
                if (atkSelect == 1)
                {
                    monSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(randomMonstersInfo, battleInfo, true);
                    ioManager.PrintMessage(PlayerTurn(player, monster, ioManager, monSelect - 1, skillSelect, false), true);

                    Console.ReadKey();

                }
                else if (atkSelect == 2)
                {
                    string[] playerSkills = { player.GetSkillNameOne(), player.GetSkillNameTwo() };
                    skillSelect = ioManager.PrintMessageWithNumberForSelect(playerSkills, false);


                    if (skillSelect == 1)
                    {
                        monSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(randomMonstersInfo, battleInfo, true);


                        ioManager.PrintMessage(PlayerTurn(player, monster, ioManager, monSelect - 1, skillSelect, true), true);
                        Console.ReadKey();
                    }
                    else if (skillSelect == 2)
                    {

                        ioManager.PrintMessage(randomMonstersInfo, true);
                        ioManager.PrintMessage(battleInfo, false);
                        Console.ReadKey();
                        ioManager.PrintMessage(PlayerTurn(player, monster, ioManager, 0, skillSelect, true), false);
                        Console.ReadKey();
                    }
                }

            }
            else if (Count % 2 == 1)
            {

                for (int i = 0; i < monster.monsters.Count; i++)
                {
                    if (monster.monsters[i].Hp > 0)
                    {
                        ioManager.PrintMessage(MonsterTurn(player, monster, i, ioManager), true);
                        Console.ReadKey();
                    }

                }


            }
            Count++;
        }
        public string[] PlayerTurn(Player player, Monster monster, IOManager ioManager, int monSelect, int skillSelect, bool isSkillUsed = false)
        {
            string AreaAttackName = player.GetSkillNameTwo();


            if (monster.monsters[monSelect].Hp <= 0)
            {


                return new string[]
                {
                    "전투!!",
                    "",
                    $"{monster.monsters[monSelect].Name} 은(는) 이미 죽었습니다.",
                    "",
                    ">>",
                    ""
                };
            }


            double previousHp = monster.monsters[monSelect].Hp;

            double damage = 0;


            if (isSkillUsed && skillSelect == 1)
            {
                damage = player.UseSkillOne(monster.monsters[monSelect]);
            }
            else if (isSkillUsed && skillSelect == 2)
            {

                damage = player.UseSkillTwo(monster.monsters);
                List<Monster> attackedMonster = player.GetAttackedMonster();
                string areaAttackMsg = $"Lv.{player.Level} {player.Name} 의 {AreaAttackName}\n";

                foreach (var target in attackedMonster)
                {
                    string status = (target.Hp == 0) ? "Dead" : target.Hp.ToString();
                    areaAttackMsg += $"Lv.{target.Level} {target.Name} {target.Hp + damage} -> {target.Hp}[ 데미지 : {damage}]\n";
                }
                attackedMonster.Clear();


                return new string[]
                {
                    "Battle!!",
                    "",
                    $"{areaAttackMsg}",
                    "",
                    ">>",
                    ""
                };
            }
            else
            {
                damage = player.PlayerAttack(monster.monsters[monSelect]);
            }



            if (monster.monsters[monSelect].Hp <= 0)
            {
                DeadMonsterCount++;// 몬스터 사망시 카운트 
                player.IncrementMonsterKills();// 퀘스트 몬스터 카운트
            }


            string hpInfo = (monster.monsters[monSelect].Hp == 0) ? "Dead" : monster.monsters[monSelect].Hp.ToString();

            string criticalHitMessage = (damage == player.Attack * 1.6) ? "치명타 공격!!" : "";


            string attackMessage = (damage == 0)
             ? $"{monster.monsters[monSelect].Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다."
             : $"{monster.monsters[monSelect].Name} 을(를) 맞췄습니다. [ 데미지 : {damage} ] " + criticalHitMessage;


            string[] playerTurn = new string[]
            {
                "Battle!!",
                "",
                $"Lv.{player.Level} {player.Name} 의 공격!",
                attackMessage,
                "",
                $"Lv.{monster.monsters[monSelect].Level} {monster.monsters[monSelect].Name}",
                $"HP {previousHp} -> {hpInfo}",
                "",
                "",
                ">>",
                ""
            };



            return playerTurn;
        }
        public string[] MonsterTurn(Player player, Monster monster, int i, IOManager ioManager)
        {
            double damage = monster.monsters[i].Attack;

            double previousHp = player.Hp;

            player.PlayerDamageTaken(damage);

            double currentHp = player.Hp;

            string[] monsterTurn = new string[]
            {
                "Battle!!",
                "",
                $"Lv.{monster.monsters[i].Level} {monster.monsters[i].Name} 의 공격!",
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
        public string[] GetRandomMonsterInfo(Monster monster, IOManager ioManager)
        {
            string[] randomMonstersInfo = new string[monster.monsters.Count];
            for (int i = 0; i < monster.monsters.Count; i++)
            {

                string monsterHpDisplay = (monster.monsters[i].Hp == 0) ? "Dead" : $"HP {monster.monsters[i].Hp.ToString()}";
                randomMonstersInfo[i] = $"Lv. {monster.monsters[i].Level} {monster.monsters[i].Name} {monsterHpDisplay}";

            }

            ioManager.PrintMessage(randomMonstersInfo, true);
            return randomMonstersInfo;
        }
        public void BattleResult(int result, Player player, Monster monter, IOManager ioManager)
        {
            if (result == 1) // 플레이어 승리
            {
                QuestDeadMonsterCount += DeadMonsterCount;
                string[] winMessage =
                {
                    "Battle!! - Result",
                    "",
                    "Victory",
                    "",
                    $"던전에서 몬스터 {DeadMonsterCount}마리를 잡았습니다.",
                    "",
                    $"Lv.{player.Level} {player.Name}",
                    $"HP {player.MaxHp} -> {player.Hp}",
                    "",
                    "계속 하려면 아무키나 입력해주세요.",
                    "",
                    ">>\n"
                };
                ioManager.PrintMessage(winMessage, true);

                Console.ReadKey();

            }
            else if (result == 2)// 플레이어 패배
            {
                string[] loseMessage =
                {
                    "Battle!! - Result",
                    "",
                    "You Lose",
                    "",
                    $"Lv.{player.Level} {player.Name}",
                    $"HP {player.MaxHp} -> {player.Hp}",
                    "",
                    "계속 하려면 아무키나 입력해주세요.",
                    "",
                ">>\n"
                };
                ioManager.PrintMessage(loseMessage, true);

                Console.ReadKey();
            }
        }
    }
}