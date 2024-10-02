namespace _2GETHER
{
    class Player
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public double Attack { get; private set; }
        public double Defense { get; private set; }
        public double Hp { get; private set; }
        public double MaxHp { get; private set; }
        public double Mp { get; private set; }
        public double MaxMp { get; private set; }
        public int Gold { get; private set; }
        public int Exp { get; private set; }
        public int MaxExp { get; private set; }
        public EJob Job { get; private set; }
        public int Potions { get; private set; }
        public int MonsterKills { get; private set; }

        private const int maxLevel = 5;

        private Random random = new Random();

        public EquipmentItem[] weaponEquipment = new EquipmentItem[1];
        public EquipmentItem[] armorEquipment = new EquipmentItem[1];
        public List<EquipmentItem> equipmentInventory = new List<EquipmentItem>();
        public List<ConsumableItem> consumableInventory = new List<ConsumableItem>();

        List<Monster> attackedMonster = new List<Monster>();

        public Player()
        {
            Name = "Chad";
            Level = 1;
            Attack = 10.0;
            Defense = 5.0;
            Hp = 100;
            MaxHp = 100;
            Mp = 50;
            MaxMp = 50;
            Gold = 5000;
            Exp = 0;
            MaxExp = 100;
            Job = EJob.전사;
            Potions = 3;
            MonsterKills = 0;
        }

        public Player(string name, EJob job)
        {
            Name = name;
            Job = job;
            Level = 1;
            Attack = 10.0;
            Defense = 5.0;
            Hp = 100;
            MaxHp = 100;
            Mp = 50;
            MaxMp = 50;
            Gold = 0;
            Exp = 0;
            MaxExp = 100;
            Potions = 3;
            MonsterKills = 0;
        }

        public double AttackWithEffects()
        {
            if (random.NextDouble() <= 0.1)
            {
                return 0;
            }

            double damage = Attack;

            if (random.NextDouble() <= 0.15)
            {
                damage *= 1.6;
            }

            return damage;
        }

        public double PlayerAttack(Monster monster)
        {
            double damage = AttackWithEffects();
            double finalDamage = monster.MonsterDamageTaken(damage);
            return finalDamage;
        }

        public void PlayerDamageTaken(double damage)
        {
            damage = damage * (100 - Defense) / 100;

            damage = Math.Round(damage);

            Hp -= damage;

            if (Hp < 0) Hp = 0;
        }

        public double UseSkillOne(Monster monster)
        {
            if (Mp >= 10)
            {
                Mp -= 10;
                double damage = Attack * 2;
                monster.MonsterDamageTaken(damage);
                return damage;
            }
            else
            {
                Console.WriteLine("MP가 부족합니다!");
                return 0;
            }
        }

        public double UseSkillTwo(List<Monster> monsters)
        {
            if (Mp >= 15)
            {
                Mp -= 15;
                double damage = Attack * 1.5;

                var liveMonsters = monsters.Where(x => x.Hp > 0).ToList();

                if (liveMonsters.Count == 1)
                {
                    liveMonsters[0].MonsterDamageTaken(damage);
                    attackedMonster.Add(liveMonsters[0]);
                    return damage;
                }
                else
                {
                    int firstMonster = random.Next(liveMonsters.Count);
                    int secondMonster;

                    do
                    {
                        secondMonster = random.Next(liveMonsters.Count);
                    } while (secondMonster == firstMonster);

                    Monster firstTarget = liveMonsters[firstMonster];
                    Monster secondTarget = liveMonsters[secondMonster];

                    attackedMonster.Add(firstTarget);
                    attackedMonster.Add(secondTarget);

                    firstTarget.MonsterDamageTaken(damage);
                    secondTarget.MonsterDamageTaken(damage);
                    return damage;
                }
            }
            else
            {
                Console.WriteLine("MP가 부족합니다!");
                return 0;
            }
        }

        public List<Monster> GetAttackedMonster()
        {
            return attackedMonster;
        }

        public string GetSkillNameOne()
        {
            switch (Job)
            {
                case EJob.전사:
                    return "강타 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n";
                case EJob.궁수:
                    return "속사 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n";
                case EJob.마법사:
                    return "화염구 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n";
                default:
                    return "기본 공격";
            }
        }

        public string GetSkillNameTwo()
        {
            switch (Job)
            {
                case EJob.전사:
                    return "폭풍 가르기 - MP 15\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n";
                case EJob.궁수:
                    return "연속 사격 - MP 15\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n";
                case EJob.마법사:
                    return "얼음 폭풍 - MP 15\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n";
                default:
                    return "기본 공격";
            }
        }

        public int GainExp(int gainExp)
        {
            if (Level >= maxLevel)
            {
                Console.WriteLine("최대 레벨에 도달하여 더 이상 경험치를 얻을 수 없습니다.");
                Console.ReadKey(true);
                return 0;
            }

            Exp += gainExp;

            while (Exp >= MaxExp && Level < maxLevel)
            {
                LevelUp();
            }

            return Exp;
        }

        public void LevelSystem(Monster monster)
        {
            int gainExp = monster.Level * 1;
            GainExp(gainExp);
        }

        private void LevelUp()
        {
            Exp -= MaxExp;
            Level++;

            switch (Level)
            {
                case 1:
                    MaxExp = 10;
                    break;
                case 2:
                    MaxExp = 35;
                    break;
                case 3:
                    MaxExp = 65;
                    break;
                case 4:
                    MaxExp = 100;
                    break;
                case 5:
                    MaxExp = 0;
                    Console.WriteLine("축하드립니다! 최대 레벨에 도달했습니다!");
                    Console.ReadKey(true);
                    break;
            }

            Attack += 0.5;
            Defense += 1.0;
            MaxHp += 10.0;
            MaxMp += 10.0;

            Hp = MaxHp;
            Mp = MaxMp;

            Console.WriteLine("레벨업하셨습니다! 축하드립니다!");
            Console.ReadKey(true);
        }

        public void UsePotion(ConsumableItem consumableItem)
        {
            if (Potions > 0)
            {
                Hp += 30;

                if (Hp > MaxHp)
                {
                    Hp = MaxHp;
                }

                Potions--;
                consumableItem.RemoveCount();
                Console.WriteLine("회복을 완료했습니다. 현재 체력: {0}/{1}", Hp, MaxHp);
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("포션이 부족합니다.");
                Console.ReadKey(true);
            }
        }

        public void AddPotion(ConsumableItem consumableItem)
        {
            Potions++;
        }

        public void UpdateStatsOnEquip(Item item)
        {
            Attack += item.ItemATK;
            Defense += item.ItemDEF;
        }

        public void UpdateStatsOnUnequip(Item item)
        {
            Attack -= item.ItemATK;
            Defense -= item.ItemDEF;
        }

        public int Buy(Item item)
        {
            Gold -= item.Price;
            return Gold;
        }

        public int Sell(Item item)
        {
            Gold += item.Price * 85 / 100;
            return Gold;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeJob(EJob job)
        {
            Job = job;

            switch (job)
            {
                case EJob.전사:
                    Attack = 10.0;
                    Defense = 10.0;
                    MaxHp = 150.0;
                    MaxMp = 30.0;
                    MaxExp = 10;
                    break;

                case EJob.궁수:
                    Attack = 13.0;
                    Defense = 7.0;
                    MaxHp = 120.0;
                    MaxMp = 60.0;
                    MaxExp = 10;
                    break;

                case EJob.마법사:
                    Attack = 15.0;
                    Defense = 5.0;
                    MaxHp = 100.0;
                    MaxMp = 80.0;
                    MaxExp = 10;
                    break;

                default:
                    Console.WriteLine("알 수 없는 직업입니다.");
                    break;
            }

            Hp = MaxHp;
            Mp = MaxMp;
        }

        public void AddGold(int amount)
        {
            Gold += amount;
        }

        public void AddItem(EquipmentItem equipmentItem)
        {
            if (equipmentItem != null)
            {
                equipmentInventory.Add(equipmentItem);
            }
            else
            {
                Console.WriteLine("잘못된 아이템입니다.");
            }
        }

        public void AddMp()
        {
            Mp += 10;

            if (Mp > MaxMp)
            {
                Mp = MaxMp;
            }
        }

        public void IncrementMonsterKills()
        {
            MonsterKills++;
        }

        public void SetPlayerData(string name, int level, double attack, double defense, double hp, double maxHp, double mp, double maxMp, int gold, int exp, int maxExp, EJob job, int potions, int monsterKills)
        {
            Name = name;
            Level = level;
            Attack = attack;
            Defense = defense;
            Hp = hp;
            MaxHp = maxHp;
            Mp = mp;
            MaxMp = maxMp;
            Gold = gold;
            Exp = exp;
            MaxExp = maxExp;
            Job = job;
            Potions = potions;
            MonsterKills = monsterKills;
        }
    }

    public enum EJob
    {
        공용,
        전사,
        궁수,
        마법사
    }
}
