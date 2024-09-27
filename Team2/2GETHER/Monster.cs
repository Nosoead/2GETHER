namespace _2GETHER
{
    class Monster
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public double Hp { get; private set; }
        public double Attack { get; private set; }

        public Monster(string name, int level, int hp, double attack)
        {
            Name = name;
            Level = level;
            Hp = hp;
            Attack = attack;
        }

        public string GetMonsterInfo()
        {
            return $"Lv: {Level}, {Name}, HP: {Hp}, ATK: {Attack}";
        }

        public void MonsterAtk()
        {

        }

        public string MonsterDamageTaken(Player player)
        {
            double previousHp = Hp;

            Hp -= player.Attack;

            string hpInfo;

            if (Hp <= 0)
            {
                Hp = 0;
                hpInfo = "Dead";
            }
            else
            {
                hpInfo = Hp.ToString();
            }

            string[] monsterDamageTaken = new string[]
            {
               "Battle!!",
               "",
               $"Lv.{player.Level} {player.Name} 의 공격!",
               $"{Name} 을(를) 맞췄습니다. [ 데미지 : {player.Attack} ]",
               "",
               $"Lv.{Level} {Name}",
               $"HP {previousHp} -> {hpInfo}",
               ""
            };           

            return string.Join("\n", monsterDamageTaken);
        }
    }

    class Goblin : Monster
    {
        public Goblin() : base("고블린", 1, 50, 10) { }
    }

    class Oak : Monster
    {
        public Oak() : base("오크", 2, 70, 15) { }
    }

    class Ooger : Monster
    {
        public Ooger() : base("오우거", 3, 90, 20) { }
    }

    class GoblinKing : Monster
    {
        public GoblinKing() : base("고블린킹", 5, 120, 25) { }
    }
}