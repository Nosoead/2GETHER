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

        private const int MaxLevel = 5;

        private Random random = new Random();

        public Item[] WeaponEquipment = new Item[1];

        public Item[] ArmorEquipment = new Item[1];
        public List<Item> InventoryItems { get; private set; }  = new List<Item>();

        List<Monster> AttackedMonster = new List<Monster>();


        public Player()
        {
            Name = "";
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

        public void PlayerAttack(Monster monster)
        {
            double damage = AttackWithEffects();

            monster.MonsterDamageTaken(damage);
        }

        public void PlayerDamageTaken(double damage)
        {
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

                if (monsters.Count == 1)
                {
                    monsters[0].MonsterDamageTaken(damage);
                    AttackedMonster.Add(monsters[0]);
                    return damage;
                }
                else
                {
                    int firstMonster = random.Next(monsters.Count);
                    int secondMonster;

                    do
                    {
                        secondMonster = random.Next(monsters.Count);
                    } while (secondMonster == firstMonster);

                    Monster firstTarget = monsters[firstMonster];
                    Monster secondTarget = monsters[secondMonster];

                    AttackedMonster.Add(firstTarget);
                    AttackedMonster.Add(secondTarget);

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
            return AttackedMonster;
        }

        public string GetSkillNameOne()
        {
            switch (Job)
            {
                case EJob.전사:
                    return "\n강타 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n";

                case EJob.마법사:
                    return "\n화염구 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n";

                case EJob.궁수:
                    return "\n속사 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.\n";

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

                case EJob.마법사:
                    return "얼음 폭풍 - MP 15\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n";

                case EJob.궁수:
                    return "연속 사격 - MP 15\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.\n";

                default:
                    return "기본 공격";
            }
        }

        public void GainExp(int gainExp)
        {
            if (Level >= MaxLevel)
            {
                Console.WriteLine("최대 레벨에 도달하여 더 이상 경험치를 얻을 수 없습니다.");
                return;
            }

            Exp += gainExp;

            while (Exp >= MaxExp && Level < MaxLevel)
            {
                LevelUp();
            }
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
                    break;
            }

            Attack += 0.5;
            Defense += 1.0;
            MaxHp += 10.0;
            MaxMp += 10.0;

            Hp = MaxHp;
            Mp = MaxMp;

            Console.WriteLine($"레벨업! 현재 레벨: {Level}, 경험치: {Exp}/{MaxExp}, 공격력: {Attack}, 방어력: {Defense}, HP: {Hp}/{MaxHp}, MP: {Mp}/{MaxMp}");
        }

        public void UsePotion()
        {
            if (Potions > 0)
            {
                Hp += 30;

                if (Hp > MaxHp)
                {
                    Hp = MaxHp;
                }

                Potions--;
                Console.WriteLine("회복을 완료했습니다. 현재 체력: {0}/{1}", Hp, MaxHp);
            }
            else
            {
                Console.WriteLine("포션이 부족합니다.");
            }
        }

        public void UpdateStatsOnEquip(Item item)
        {
            Attack += item.itemATK;
            Defense += item.itemDEF;
        }

        public void UpdateStatsOnUnequip(Item item)
        {
            Attack -= item.itemATK;
            Defense -= item.itemDEF;
        }

        public void EquipItem(int inputNum, Inventory inventory)
        {
            if (inputNum < 1 || inputNum > InventoryItems.Count)
            {
                Console.WriteLine("유효하지 않은 아이템 번호입니다.");
                return;
            }

            Item item = InventoryItems[inputNum - 1];

            if (item.isArmor)
            {
                if (ArmorEquipment[0] != null)
                {
                    UpdateStatsOnUnequip(ArmorEquipment[0]);
                }
                ArmorEquipment[0] = item;
                UpdateStatsOnEquip(item);
            }
            else
            {
                if (WeaponEquipment[0] != null)
                {
                    UpdateStatsOnUnequip(WeaponEquipment[0]);
                }
                WeaponEquipment[0] = item;
                UpdateStatsOnEquip(item);
            }
        }

        public int Buy(Item item)
        {
            Gold -= item.price;
            return Gold;
        }

        public int Sell(Item item)
        {
            Gold += item.price*85/100;
            return Gold;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        public void ChangeJob(EJob job)
        {
            Job = job;
        }
        
        public void AddGold(int amount)
        {
            Gold += amount;
        }

        public void AddItem(Item item)
        {
            if (item != null)
            {
                InventoryItems.Add(item);
            }
            else
            {
                Console.WriteLine("잘못된 아이템입니다.");
            }
        }

        public void IncrementMonsterKills()
        {
            MonsterKills++;
        }
    }

    public enum EJob { 공용, 전사, 궁수, 마법사 }
}