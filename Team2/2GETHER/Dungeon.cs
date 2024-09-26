namespace _2GETHER
{
    public class Dungeon
    {   
        public void CreateMonster()
        {
            EMonsterName eMonsterName = new EMonsterName();
            Random random = new Random();


        }
        public void StartBattle()
        {
            Monster mosnter= new Monster();
            Player player= new Player();
            Console.WriteLine($"전투 시작~! 플레이어 정보: 체력({player.hp}), 공격력({player.attack})");

            

        }
        
    }
}
