namespace _2GETHER
{
    public class Monster
    {
        public EMonsterName name;
        public int level;
        public int hp;
        public int attack;

        public Monster()
        {
            name = EMonsterName.고블린;
            level = 1;
            hp = 50;
            attack = 10;
        }




        public void MonsterAttack()

        // 몬스터 공격, 죽었을때 데미지 안들어가고 안때리고(bool값 이용)
        {


        }

        public string GetMonsterInfo()
        {
            string monsterInfo = $"Lv: {level}, {name}, HP: {hp}, ATK: {attack}";
            return monsterInfo;
        }
    }

    public enum EMonsterName { 고블린, 오크, 오우거, 고블린킹 }



}
