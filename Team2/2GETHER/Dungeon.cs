﻿namespace _2GETHER
{
    class Dungeon
    {
        int Count;
        int DeadMonsterCount;

        public void StartBattle(Player player, Monster monster, IOManager ioManager, Quest quest) // 던전 전투 시작
        {
            DeadMonsterCount = 0;
            Count = 0;

            monster.CreateMonster();

            while (player.Hp > 0 && monster.monsters.Count > DeadMonsterCount)
            {
                ExcuteTurn(player, monster, ioManager);
            }

            if (monster.monsters.Count == DeadMonsterCount)
            {
                BattleResult(1, player, monster, ioManager, quest); // 전투결과 승리로 이동
                return;
            }
            if (player.Hp == 0)
            {
                BattleResult(2, player, monster, ioManager, quest); // 전투결과 패배로 이동
                return;
            }

        }

        public void ExcuteTurn(Player player, Monster monster, IOManager ioManager) // 턴 실행 전 선택지 선택
        {
            int monSelect;
            int atkSelect;
            int skillSelect = 0;
            

            if (Count % 2 == 0) // 결과값이 0일때 플레이어의 턴을 실행
            {
                string[] atkChoice = { "공격", "스킬" };


                string[] randomMonstersInfo = GetRandomMonstersInfo(monster, ioManager);

                ioManager.PrintMessage(randomMonstersInfo, true, true);

                string[] battleInfo =
                {
                    "\n[내정보]\n",
                    $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                    $"HP {player.Hp}/{player.MaxHp}",
                    $"MP {player.Mp}/{player.MaxMp}\n"
                };
                ioManager.PrintMessage(battleInfo, false, false);

                atkSelect = ioManager.PrintMessageWithNumberForSelect(atkChoice, false, false);



                if (atkSelect == 1)
                {
                    monSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(randomMonstersInfo, battleInfo, true, true);
                    if (monSelect == 0)
                    {
                        return;
                    }
                    else
                    {
                        ioManager.PrintMessage(PlayerTurn(player, monster, ioManager, monSelect - 1, skillSelect, false), true, false);

                        Console.ReadKey(true);
                    }
                }
                else if (atkSelect == 2)
                {
                    string[] playerSkills = { player.GetSkillNameOne(), player.GetSkillNameTwo() };

                    ioManager.PrintMessage(randomMonstersInfo, true, true);

                    ioManager.PrintMessage(battleInfo, false);

                    skillSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(playerSkills, false, false);

                    if (skillSelect == 0)
                    {
                        return;
                    }
                    if (skillSelect == 1)
                    {
                        monSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(randomMonstersInfo, battleInfo, true, true);

                        if (monSelect == 0)
                        {
                            return;
                        }
                        else
                        {
                            ioManager.PrintMessage(PlayerTurn(player, monster, ioManager, monSelect - 1, skillSelect, true), true, false);
                            Console.ReadKey(true);
                        }
                    }
                    else if (skillSelect == 2)
                    {
                        ioManager.PrintMessage(randomMonstersInfo, true, true);

                        ioManager.PrintMessage(battleInfo, false, false);

                        ioManager.PrintMessage(PlayerTurn(player, monster, ioManager, 0, skillSelect, true), true, false);
                        Console.ReadKey(true);
                    }
                }
            }
            
            else if (Count % 2 == 1) // 결과값이 1일때 몬스터의 턴을 실행
            {
                for (int i = 0; i < monster.monsters.Count; i++)
                {
                    if (monster.monsters[i].Hp > 0)
                    {
                        ioManager.PrintMessage(MonsterTurn(player, monster, i, ioManager), true, false);
                        Console.ReadKey(true);
                    }
                }
            }
            Count++;

        }
        public string[] PlayerTurn(Player player, Monster monster, IOManager ioManager, int monSelect, int skillSelect, bool isSkillUsed = false) // 플레이어의 턴
        {
            string AreaAttackName = player.GetSkillNameTwo();

            if (monster.monsters[monSelect].Hp <= 0)
            {
                string[] againPlayerTurn =
                {
                    $"\n{monster.monsters[monSelect].Name} 은(는) 이미 죽었습니다.",
                    "",
                    "다른 몬스터를 선택하세요.",
                    "",
                    ">>"
                };

                string[] randomMonstersInfo = GetRandomMonstersInfo(monster, ioManager);

                ioManager.PrintMessage(againPlayerTurn, false, false);

                Console.ReadKey();

                ExcuteTurn(player, monster, ioManager); // 죽은 몬스터 공격 시 턴 선택으로 돌아가기
                return new string[] { };

            }

            double previousHp = monster.monsters[monSelect].Hp;

            double damage = 0;

            if (isSkillUsed && skillSelect == 1)
            {
                damage = player.UseSkillOne(monster.monsters[monSelect]); // 스킬 1 사용
            }
            else if (isSkillUsed && skillSelect == 2)
            {
                double previousMp = player.Mp;          

                damage = player.UseSkillTwo(monster.monsters); // 스킬 2 사용

                List<Monster> attackedMonster = player.GetAttackedMonster(); // 스킬 2 에 공격당한 몬스터의 리스트

                string areaAttackMsg = $"Lv.{player.Level} {player.Name} 의 {AreaAttackName}\n";

                if (damage == 0 && isSkillUsed == true ) // 스킬 2 의 마나가 부족할때 턴 선택으로 돌아가기
                {
                    Console.ReadKey();
                    ExcuteTurn(player, monster, ioManager);
                }

                else
                {

                    foreach (var target in attackedMonster)
                    {
                        if (target.Hp <= 0)
                        {
                            DeadMonsterCount++;
                            player.IncrementMonsterKills();
                        }


                        string status = (target.Hp == 0) ? "Dead" : target.Hp.ToString();

                        areaAttackMsg += $"Lv.{target.Level} {target.Name} {target.Hp + damage} -> {status}[ 데미지 : {damage}]\n";

                    }
                    attackedMonster.Clear();
                }


                return new string[]
                {
                    "Battle!!",
                    "",
                    $"{areaAttackMsg}",
                    "",
                    "계속 하려면 아무키나 입력해주세요.",
                    "",
                    ">>"
                };

            }
            else
            {
                damage = player.PlayerAttack(monster.monsters[monSelect]); // 일반공격
            }

            if (monster.monsters[monSelect].Hp <= 0)
            {
                DeadMonsterCount++; // 몬스터 사망시 카운트 
                player.IncrementMonsterKills(); // 퀘스트 몬스터 카운트
            }

            if (damage == 0 && isSkillUsed == true) // 스킬 1 의 마나가 부족할때 턴 선택으로 돌아가기
            {
                Console.ReadKey();
                ExcuteTurn(player, monster, ioManager);
                return new string[] { };
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
                "계속 하려면 아무키나 입력해주세요.",
                "",
                ">>",
            };

            return playerTurn;
        }
        public string[] MonsterTurn(Player player, Monster monster, int i, IOManager ioManager) // 몬스터의 턴
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
                "계속 하려면 아무키나 입력해주세요.",
                "",
                ">>",
            };

            return monsterTurn;
        }
        public string[] GetRandomMonstersInfo(Monster monster, IOManager ioManager) // 랜덤한 몬스터의 정보를 불러옴
        {
            string[] randomMonstersInfo = new string[monster.monsters.Count];

            for (int i = 0; i < monster.monsters.Count; i++)
            {

                string monsterHpDisplay = (monster.monsters[i].Hp == 0) ? "Dead" : $"HP {monster.monsters[i].Hp.ToString()}";
                randomMonstersInfo[i] = $"Lv. {monster.monsters[i].Level} {monster.monsters[i].Name} {monsterHpDisplay}";

            }
            return randomMonstersInfo;
        }
        public void BattleResult(int result, Player player, Monster monster, IOManager ioManager, Quest quest) // 전투결과
        {
            if (result == 1) // 승리
            {
                int previousExp = player.Exp;

                for (int i = 0; i < monster.monsters.Count; i++)
                {
                    player.LevelSystem(monster.monsters[i]);
                }

                int currentExp = player.Exp;
                string[] winMessage =
                {
                    "Battle!! - Result",
                    "",
                    "Victory",
                    "",
                    $"던전에서 몬스터 {DeadMonsterCount}마리를 잡았습니다.",
                    "",
                    "[캐릭터 정보]",
                    $"Lv.{player.Level} {player.Name}",
                    $"HP {player.MaxHp} -> {player.Hp}",
                    $"exp {previousExp} -> {currentExp}",
                    "",
                    "계속 하려면 아무키나 입력해주세요.",
                    "",
                    ">>"
                };

                ioManager.PrintMessage(winMessage, true, false);

                Console.ReadKey(true);

                GiveDungeonReward(player, quest, ioManager);
            }

            else if (result == 2) // 패배
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
                    ">>"
                };
                ioManager.PrintMessage(loseMessage, true, false);

                Console.ReadKey(true);
            }
        }

        public void GiveDungeonReward(Player player, Quest quest, IOManager ioManager)
        {
            // 랜덤 아이템 드랍
            EquipmentItem randomItem = quest.RandomItemDrop(); // Quest 클래스의 RandomItemDrop 사용
            EquipmentItem selectItem = randomItem;
            player.equipmentInventory.Add(randomItem);
            selectItem.AddCount();
            if (!player.equipmentInventory.Contains(selectItem))
            { player.equipmentInventory.Add(selectItem); }

            // 랜덤 골드 드랍 (1000 ~ 5000 골드 사이)
            int randomGold = quest.RandomGoldDrop(1000, 5000); // Quest 클래스의 RandomGoldDrop 사용
            player.AddGold(randomGold);

            // 보상 출력
            string[] rewardMessage = new string[]
            {
                "던전 클리어 보상",
                "",
                $"아이템: {randomItem.eItem}",
                $"골드: {randomGold} G",
                "",
                "계속 하려면 아무키나 입력해주세요.",
                "",
                ">>"
            };

            ioManager.PrintMessage(rewardMessage, true, false);
            Console.ReadKey(true);
        }
    }
}