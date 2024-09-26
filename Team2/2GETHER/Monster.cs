using System;
using System.Reflection.Emit;

namespace _2GETHER
{
    public class Monster
    {
        EMonsterName name;
        int level;
        int hp;
        int attack;


        
        public void printMonsterInfo()
        {
            Console.WriteLine($"Lv: {level}, {name}, HP: {hp}, ATK: {attack}");
        }
    }

    public enum EMonsterName { 고블린, 오크, 오우거, 고블린킹 }

    

}
