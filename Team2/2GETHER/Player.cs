namespace _2GETHER
{
    public class Player
    {
        public string name;
        public int level;
        public double attack;
        public double defense;
        public int hp;
        public int maxHp;
        public int gold;
        public int exp;
        public int maxExp;
        public EJob job;
        public bool equipedWeapon;
        public bool equipedArmor;

        public Player()
        {
            name = "";
            level = 1;
            attack = 10.0;
            defense = 5.0;
            hp = 100;
            maxHp = 100;
            gold = 0;
            exp = 0;
            maxExp = 100;
            job = EJob.전사;
            equipedWeapon = false;
            equipedArmor = false;
        }

        public Player(string name, EJob job)
        {
            this.name = name;
            this.job = job;
            level = 1;
            attack = 10.0;
            defense = 5.0;
            hp = 100;
            maxHp = 100;
            gold = 0;
            exp = 0;
            maxExp = 100;
            equipedWeapon = false;
            equipedArmor = false;
        }        
    }
    public enum EJob { 전사, 궁수, 마법사 }
}
