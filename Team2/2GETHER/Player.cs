﻿namespace _2GETHER
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
        public bool EquippedWeapon { get; private set; } = false;
        public bool EquippedArmor { get; private set; } = false;
        public int Potions { get; private set; }

        private const int MaxLevel = 5;

        private Random random = new Random();

        public Item[] WeaponEquipment = new Item[1];

        public Item[] ArmorEquipment = new Item[1];

        public List<Item> InventoryItems = new List<Item>();

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
            EquippedWeapon = false;
            EquippedArmor = false;
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
            EquippedWeapon = false;
            EquippedArmor = false;
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

        public string GetSkillNameOne()
        {
            switch (Job)
            {
                case EJob.전사:
                    return "강타";

                case EJob.마법사:
                    return "화염구";

                case EJob.궁수:
                    return "속사";

                default:
                    return "기본 공격";
            }
        }

        public string GetSkillNameTwo()
        {
            switch (Job)
            {
                case EJob.전사:
                    return "폭풍 가르기";

                case EJob.마법사:
                    return "얼음 폭풍";

                case EJob.궁수:
                    return "연속 사격";

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

        public void QuestReward()
        {

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
    }

    public enum EJob { 전사, 궁수, 마법사 }
}