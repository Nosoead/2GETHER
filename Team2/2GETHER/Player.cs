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
        public bool EquippedWeapon { get; private set; }
        public bool EquippedArmor { get; private set; }

        private Random random = new Random();

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

        public void PlayerDamageTaken(Monster monster)
        {
            double damage = AttackWithEffects();

            Hp -= monster.Attack;
        }

        public Item[] WeaponEquipment = new Item[1];
        public Item[] ArmorEquipment = new Item[1];
        public List<Item> InventoryItems = new List<Item>();

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
    }

    public enum EJob { 전사, 궁수, 마법사 }
}