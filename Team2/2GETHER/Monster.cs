namespace _2GETHER
{
    public class Monster
    {
        public string name;
        public int level;
        public int hp;
        public double attack;

        public Monster(string name, int level, int hp, double attack)
        {
            this.name = name;
            this.level = level;
            this.hp = hp;
            this.attack = attack;
        }

        public string GetMonsterInfo()
        {
            return $"Lv: {level}, {name}, HP: {hp}, ATK: {attack}";
        }               
    }
    
    public class Goblin : Monster
    {
        public Goblin() : base("고블린", 1, 50, 10) { }
    }

    public class Oak : Monster
    {
        public Oak() : base("오크", 2, 70, 15) { }
    }

    public class Ooger : Monster
    {
        public Ooger() : base("오우거", 3, 90, 20) { }
    }

    public class GoblinKing : Monster
    {
        public GoblinKing() : base("고블린킹", 5, 120, 25) { }
    }
}
