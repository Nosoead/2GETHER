using System.Numerics;

namespace _2GETHER
{
    class Player
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public double Attack{ get; private set; }
        public double Defense{ get; private set; }
        public double Hp{ get; private set; }
        public double MaxHp{ get; private set; }
        public int Gold{ get; private set; }
        public int Exp{ get; private set; }
        public int MaxExp{ get; private set; }
        public EJob Job{ get; private set; }
        public bool EquippedWeapon{ get; private set; }
        public bool EquippedArmor{ get; private set; }

        public Player()
        {
            Name = "";
            Level = 1;
            Attack = 10.0;
            Defense = 5.0;
            Hp = 100;
            MaxHp = 100;
            Gold = 0;
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
            Gold = 0;
            Exp = 0;
            MaxExp = 100;
            EquippedWeapon = false;
            EquippedArmor = false;
        }

        public string PlayerDamageTaken(Monster monster)
        {
            double previousHp = Hp;

            Hp -= monster.Attack;

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
                $"Lv.{monster.Level} {monster.Name} 의 공격!",
               $"{Name} 을(를) 맞췄습니다. [ 데미지 : {monster.Attack} ]",
               "",
               $"Lv.{Level} {Name}",
               $"HP {previousHp} -> {hpInfo}",
               ""
            };

            return string.Join("\n", monsterDamageTaken);
        }
    }

    public enum EJob { 전사, 궁수, 마법사 }
}