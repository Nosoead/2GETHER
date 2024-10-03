namespace _2GETHER
{
    class Dungeon
    {
        int Count;

        int DeadMonsterCount;
        
        public void StartBattle(Player player, Monster monster, IOManager ioManager, Quest quest) // 던전 전투 시작 // 던전 전투 결과로 이동
        {
            double initialPlayerHp = player.Hp;

            DeadMonsterCount = 0;

            Count = 0;

            monster.CreateMonster();

            while (player.Hp > 0 && monster.Monsters.Count > DeadMonsterCount) // 턴 진행
            {
                ExcuteTurn(player, monster, ioManager);
            }

            if (monster.Monsters.Count == DeadMonsterCount) // 전투 결과 승리로 이동
            {
                BattleResultWin(player, monster, ioManager, quest, initialPlayerHp);
                return;
            }
            else if (player.Hp == 0)
            {
                BattleResultLose(player, monster, ioManager, quest, initialPlayerHp); // 전투 결과 패배로 이동
                return;
            }
        }

        private void ExcuteTurn(Player player, Monster monster, IOManager ioManager) // 턴 실행 로직
        {
            if (Count % 2 == 0) // 결과값이 0일때 플레이어의 턴을 실행
            {
                PlayerTurn(player, monster, ioManager);
            }
            else if (Count % 2 == 1) // 결과값이 1일때 몬스터의 턴을 실행
            {
                for (int i = 0; i < monster.Monsters.Count; i++)
                {
                    if (monster.Monsters[i].Hp > 0)
                    {
                        MonsterTurn(player, monster, i, ioManager);
                        
                    }
                }
                Count++;
            }
        }

        private void PlayerTurn(Player player, Monster monster, IOManager ioManager) // 플레이어 턴
        {
            int monSelect;

            int atkSelect;

            int skillSelect;

            string[] atkChoice = { "공격", "스킬" };

            string[] randomMonstersInfo = GetRandomMonstersInfo(monster);

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
            if (atkSelect == 0)
            {
                return;
            }
            if (atkSelect == 1)
            {
                monSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(randomMonstersInfo, battleInfo, true, true);

                if (monSelect == 0)
                {
                    return;
                }
                else
                {
                    BasicAttack(player, monster, ioManager, monSelect - 1);
                }
            }
            else if (atkSelect == 2)
            {
                string[] playerSkills = { player.GetSkillNameOne(), player.GetSkillNameTwo() };

                ioManager.PrintMessage(randomMonstersInfo, true, true);

                ioManager.PrintMessage(battleInfo, false, false);

                skillSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(playerSkills, false, false);

                if (skillSelect == 0)
                {
                    return;
                }
                else if (skillSelect == 1)
                {
                    monSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(randomMonstersInfo, battleInfo, true, true);

                    SkillOne(player, monster, ioManager, monSelect - 1);
                }
                else if (skillSelect == 2)
                {
                    SkillTwo(player, monster, ioManager);
                }
            }
        }

        private void MonsterTurn(Player player, Monster monster, int i, IOManager ioManager) // 몬스터 턴
        {
            double damage = monster.Monsters[i].Attack;

            double previousHp = player.Hp;

            player.PlayerDamageTaken(damage);

            double currentHp = player.Hp;

            string[] monsterTurn = new string[]
            {
                "Battle!!",
                "",
                $"Lv.{monster.Monsters[i].Level} {monster.Monsters[i].Name} 의 공격!",
                "",
                $"Lv.{player.Level} {player.Name} 을(를) 맞췄습니다. [ 데미지 : {previousHp - currentHp} ]",
                $"HP {previousHp} ->{currentHp}",
                "",
                "계속 하려면 아무키나 입력해주세요.",
                "",
                ">>",
            };

            ioManager.PrintMessage(monsterTurn, true, false);

            Console.ReadKey(true);
        }

        private void BasicAttack(Player player, Monster monster, IOManager ioManager, int monSelect) // 일반공격 사용
        {
            var selectedMonster = monster.Monsters[monSelect];

            if (selectedMonster.Hp <= 0)
            {
                PrintMonsterDeadMessage(selectedMonster, ioManager);

                return;
            }
            double previousHp = selectedMonster.Hp;

            double damage = player.PlayerAttack(selectedMonster);
            Count++;
            PrintBasicAttackMessage(player, selectedMonster, previousHp, damage, ioManager);

            if (selectedMonster.Hp <= 0)
            {
                DeadMonsterCount++;

                player.IncrementMonsterKills();
            }
        }

        private void SkillOne(Player player, Monster monster, IOManager ioManager, int monSelect) // 스킬 1 사용
        {
            var selectedMonster = monster.Monsters[monSelect];

            if (selectedMonster.Hp <= 0)
            {
                PrintMonsterDeadMessage(selectedMonster, ioManager);


                return;
            }

            if (player.Mp < 10)
            {
                player.UseSkillOne(selectedMonster);

                Console.ReadKey(true);

                return;
            }

            double previousHp = selectedMonster.Hp;

            double damage = player.UseSkillOne(selectedMonster);
            Count++;
            PrintSkillOneMessage(player, selectedMonster, previousHp, damage, ioManager);
            
            if (selectedMonster.Hp <= 0)
            {
                DeadMonsterCount++;

                player.IncrementMonsterKills();
            }
        }

        private void SkillTwo(Player player, Monster monster, IOManager ioManager) // 스킬 2 사용
        {
            List<Monster> attackedMonsters = player.GetAttackedMonster();
            double firstHp;
            double secondHp;
            if (player.Mp < 15)
            {
                player.UseSkillTwo(monster.Monsters, out firstHp, out secondHp );

                Console.ReadKey(true);

                return;
            }
            
            double damage = player.UseSkillTwo(monster.Monsters, out firstHp, out secondHp);
            Count++;
            PrintSkillTwoMessage(player, attackedMonsters, damage, ioManager, firstHp, secondHp);

            //attackedMonsters.Clear();
        }

        private string[] GetRandomMonstersInfo(Monster monster) // 랜덤한 몬스터의 정보
        {
            string[] randomMonstersInfo = new string[monster.Monsters.Count];

            for (int i = 0; i < monster.Monsters.Count; i++)
            {
                string monsterHpDisplay = (monster.Monsters[i].Hp == 0) ? "Dead" : $"HP {monster.Monsters[i].Hp.ToString()}";

                randomMonstersInfo[i] = $"Lv. {monster.Monsters[i].Level} {monster.Monsters[i].Name} {monsterHpDisplay}";
            }

            return randomMonstersInfo;
        }

        private void BattleResultWin(Player player, Monster monster, IOManager ioManager, Quest quest, double playerBeforeHp) // 전투 결과 승리
        {
            double previousHp = playerBeforeHp;

            int previousExp = player.Exp;

            double previousMp = player.Mp;

            for (int i = 0; i < monster.Monsters.Count; i++)
            {
                player.LevelSystem(monster.Monsters[i]);
            }
            player.AddMp();

            int currentExp = player.Exp;

            double currentHp = player.Hp;

            double currentMp = player.Mp;

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
                $"HP {previousHp} -> {currentHp}",
                $"Mp {previousMp} -> {currentMp}",
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

        private void BattleResultLose(Player player, Monster monster, IOManager ioManager, Quest quest, double playerBeforeHp) // 전투 결과 패배
        {
            double previousHp = playerBeforeHp;

            string[] loseMessage =
            {
                "Battle!! - Result",
                "",
                "You Lose",
                "",
                $"Lv.{player.Level} {player.Name}",
                $"HP {previousHp} -> {player.Hp}",
                "",
                "계속 하려면 아무키나 입력해주세요.",
                "",
                ">>"
            };

            ioManager.PrintMessage(loseMessage, true, false);

            Console.ReadKey(true);
        }

        private void GiveDungeonReward(Player player, Quest quest, IOManager ioManager) // 던전 클리어 시 보상 추가
        {
            // 랜덤 아이템 드랍
            EquipmentItem randomItem = quest.RandomItemDrop(); // Quest 클래스의 RandomItemDrop 사용

            EquipmentItem foundItem = player.equipmentInventory.Find(item => item.eItem == randomItem.eItem);
            if (foundItem == null) player.equipmentInventory.Add(randomItem);


            randomItem.AddCount();

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

        private void PrintMonsterDeadMessage(Monster monster, IOManager ioManager) // 죽은 몬스터 공격 시 나오는 메시지
        {
            string[] monsterDeadMessage =
            {
                $"\n{monster.Name} 은(는) 이미 죽었습니다.",
                "",
                "다른 몬스터를 선택하세요.",
                "",
                ">>"
            };

            ioManager.PrintMessage(monsterDeadMessage, false, false);

            Console.ReadKey(true);
        }

        private void PrintSkillOneMessage(Player player, Monster monster, double previousHp, double damage, IOManager ioManager) // 스킬 1 사용 시 나오는 메시지
        {
            string hpInfo = (monster.Hp == 0) ? "Dead" : monster.Hp.ToString();

            string[] skillOneIMessage =
            {
                "Battle!!",
                "",
                $"Lv.{player.Level} {player.Name} 의 공격!",
                "",
                $"Lv.{monster.Level} {monster.Name}",
                $"HP {previousHp} -> {hpInfo}",
                "",
                $"{monster.Name} 을(를) 맞췄습니다. [ 데미지 : {damage} ]",
                "계속 하려면 아무키나 입력해주세요.",
                "",
                ">>"
            };

            ioManager.PrintMessage(skillOneIMessage, true, false);

            Console.ReadKey(true);
        }

        private void PrintSkillTwoMessage(Player player, List<Monster> attackedMonsters, double damage, IOManager ioManager, double firstHp, double secondHp) // 스킬 2 사용 시 나오는 메시지
        {   
            
            string areaAttackSkillName = player.GetSkillNameTwo();

            string message = $"Lv.{player.Level} {player.Name} 의 {areaAttackSkillName}\n";
            int iteration = 0;
            int parsedHp;

            foreach (var target in attackedMonsters)
            {
                // 몬스터 사망 체크
                if (target.Hp <= 0)
                {
                    DeadMonsterCount++;

                    player.IncrementMonsterKills();
                }
                
                string status = (target.Hp <= 0) ? "Dead" : target.Hp.ToString();

                

                if (iteration == 0)
                {
                    if (int.TryParse(status, out parsedHp))
                    {
                        message += $"Lv.{target.Level} {target.Name} {firstHp} -> {status} [ 데미지 : {(int)firstHp - (parsedHp)} ]\n";
                    }
                    else
                    {
                        message += $"Lv.{target.Level} {target.Name} {firstHp} -> {status} [ 데미지 : {(int)firstHp} ]\n";
                    }
                }
                else if(iteration == 1) 
                {
                    if (int.TryParse(status, out parsedHp))
                    {
                        message += $"Lv.{target.Level} {target.Name} {secondHp} -> {status} [ 데미지 : {(int)secondHp - int.Parse(status)} ]";
                    }
                    else
                    {
                        message += $"Lv.{target.Level} {target.Name} {secondHp} -> {status} [ 데미지 : {(int)secondHp} ]\n";
                    }
                }
                iteration++;

                
            }
            string[] SkillTwoMessage =
            {
                "Battle!!",
                "",
                message,
                "",
                "계속 하려면 아무키나 입력해주세요.",
                "",
                ">>"
            };

            ioManager.PrintMessage(SkillTwoMessage, true, false);

            Console.ReadKey(true);
        }

        private void PrintBasicAttackMessage(Player player, Monster target, double previousHp, double damage, IOManager ioManager) // 일반 공격 시 나오는 메시지
        {
            string hpInfo = (target.Hp == 0) ? "Dead" : target.Hp.ToString();

            string criticalHitMessage = (damage == player.Attack * 1.6) ? "치명타 공격!!" : "";

            string attackMessage = (damage == 0)
                ? $"{target.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다."
                : $"{target.Name} 을(를) 맞췄습니다. [ 데미지 : {damage} ] " + criticalHitMessage;

            string[] basicAttackMessage = new string[]
            {
                "Battle!!",
                "",
                $"Lv.{player.Level} {player.Name} 의 공격!",
                attackMessage,
                "",
                $"Lv.{target.Level} {target.Name}",
                $"HP {previousHp} -> {hpInfo}",
                "",
                "계속 하려면 아무키나 입력해주세요.",
                "",
                ">>"
            };

            ioManager.PrintMessage(basicAttackMessage, true, false);

            Console.ReadKey(true);
        }
    }
}