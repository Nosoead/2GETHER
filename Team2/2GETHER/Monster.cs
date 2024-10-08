﻿namespace _2GETHER
{
    class Monster
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public double Hp { get; private set; }
        public double Attack { get; private set; }

        public List<Monster> Monsters = new List<Monster>();

        public Monster()
        {
            Name = "";
            Level = 1;
            Hp = 1;
            Attack = 10.0;
        }

        public Monster(string name, int level, int hp, double attack)
        {
            Name = name;
            Level = level;
            Hp = hp;
            Attack = attack;
        }

        public double MonsterDamageTaken(double baseDamage)
        {
            double errorDamage = baseDamage * 0.1;
            Random random = new Random();
            double randomNumber = (random.NextDouble() * 2) - 1;
            double finalDamage = baseDamage + (errorDamage * randomNumber);
            finalDamage = Math.Round(finalDamage);
            Hp -= finalDamage;

            if (Hp < 0) Hp = 0;

            return finalDamage;
        }

        public void CreateMonster()
        {
            Monsters.Clear();
            Random random = new Random();
            int monsterCount = random.Next(1, 5);

            for (int i = 0; i < monsterCount; i++)
            {
                Monster addMonster = RandomMonster(random.Next(1, 5));
                Monsters.Add(addMonster);
            }
        }

        private Monster RandomMonster(int monsterType)
        {
            switch (monsterType)
            {
                case 1:
                    return new Goblin();
                case 2:
                    return new Oak();
                case 3:
                    return new Ooger();
                case 4:
                    return new GoblinKing();
                default:
                    throw new ArgumentException("유효하지 않은 몬스터 타입입니다.");
            }
        }
    }

    class Goblin : Monster
    {
        public Goblin() : base("고블린", 1, 10, 5) { }
    }

    class Oak : Monster
    {
        public Oak() : base("오크", 2, 20, 10) { }
    }

    class Ooger : Monster
    {
        public Ooger() : base("오우거", 3, 30, 15) { }
    }

    class GoblinKing : Monster
    {
        public GoblinKing() : base("고블린킹", 5, 50, 20) { }
    }
}
