using System.Threading;

namespace _2GETHER
{
    class Dungeon
    {   
       
        public string[] PlayerTurn(Player player, Monster monster, int monSelect, int skillSelect, bool isSkillUsed = false )
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
           
            double damage=0;
            

            if (isSkillUsed && skillSelect == 1)
            {
                damage = player.UseSkillOne(monster.monsters[monSelect]);
            }
            else if(isSkillUsed && skillSelect == 2)
            {
                
                damage = player.UseSkillTwo(monster.monsters);
                List<Monster> attackedMonster = player.GetAttackedMonster();
                string areaAttack2 = $"Lv.{player.Level} {player.Name} 의 {AreaAttackName} 사용!\n";
                
                foreach (var target in attackedMonster)
                {
                    string status = (target.Hp == 0) ? "Dead" : target.Hp.ToString();
                    areaAttack2 += $"Lv.{target.Level} {target.Name} {target.Hp+damage} -> {target.Hp}[ 데미지 : {damage}]\n"; 
                }
                attackedMonster.Clear();
                

                return new string[]
                {
                    "Battle!!",
                    "",
                    $"{areaAttack2}",
                    "",
                    ">>",
                    ""
                };
            }
            else
            {
                damage = player.AttackWithEffects();
                player.PlayerAttack(monster.monsters[monSelect]);
            }
            
            string hpInfo = (monster.monsters[monSelect].Hp == 0) ? "Dead" : monster.monsters[monSelect].Hp.ToString();

            string criticalHitMessage = (damage > player.Attack && damage < player.Attack * 2) ? "치명타 공격!!" : "";

                
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
        public string[] MonsterTurn(Player player, Monster monster, int i)
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
        
        public void StartBattle(Player player, Monster monster, IOManager ioManager)
        {
            monster.CreateMonster();

            int count = 0;
            
            int monSelect;
            int atkSelect;
            int skillSelect =0;

            string[] randomMonstersInfo = new string[monster.monsters.Count];
            for (int i = 0; i < monster.monsters.Count; i++)
            {
                randomMonstersInfo[i] = $"Lv. {monster.monsters[i].Level} {monster.monsters[i].Name} HP {monster.monsters[i].Hp}";
            }

            ioManager.PrintMessage("전투시작!\n", true);
            ioManager.PrintMessage(randomMonstersInfo, false);

            string[] battleInfo =
            {
                "\n[내정보]\n",
                $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                $"HP {player.Hp}/{player.MaxHp}",
                $"MP {player.Mp}/{player.MaxMp}\n"
            };
            ioManager.PrintMessage(battleInfo, false);

            while (player.Hp > 0 && monster.monsters.Count > 0)
            {
                battleInfo = new string[]
                {
                    "\n[내정보]\n",
                    $"Lv.{player.Level}  {player.Name}  ({player.Job})",
                    $"HP {player.Hp}/{player.MaxHp}",
                    $"MP {player.Mp}/{player.MaxMp}\n"
                };


                for (int i = 0; i < monster.monsters.Count; i++)
                {
                    string monsterStatus = monster.monsters[i].Hp == 0 ? "Dead" : $"HP {monster.monsters[i].Hp}";
                    randomMonstersInfo[i] = $"Lv. {monster.monsters[i].Level} {monster.monsters[i].Name} {monsterStatus}";
                }
                if (count % 2 == 0)
                {
                    string[] atkChoice = { "공격", "스킬" };
                    atkSelect = ioManager.PrintMessageWithNumberForSelect(atkChoice, false);

                    if (atkSelect == 1) // 평타 사용
                    {
                        monSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(randomMonstersInfo, battleInfo, true);
                        ioManager.PrintMessage(PlayerTurn(player, monster, monSelect - 1, skillSelect, false ), false);

                        Console.ReadKey();

                    }
                    else if (atkSelect == 2) // 스킬 사용
                    {
                        string[] playerSkills = { player.GetSkillNameOne(), player.GetSkillNameTwo() };
                        skillSelect = ioManager.PrintMessageWithNumberForSelect(playerSkills, false);
                        

                        if (skillSelect == 1) // 스킬 1
                        {
                            monSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(randomMonstersInfo, battleInfo, true);
                            
                            
                            ioManager.PrintMessage(PlayerTurn(player, monster, monSelect - 1, skillSelect, true), false);
                            Console.ReadKey();
                        }
                        else if (skillSelect == 2) // 스킬 2
                        {

                            ioManager.PrintMessage(randomMonstersInfo, true);
                            ioManager.PrintMessage(battleInfo, false);
                            Console.ReadKey();
                            ioManager.PrintMessage(PlayerTurn(player, monster,0, skillSelect, true), false);
                            Console.ReadKey();
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < monster.monsters.Count; i++)
                    {
                        if (monster.monsters[i].Hp > 0)
                        {
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