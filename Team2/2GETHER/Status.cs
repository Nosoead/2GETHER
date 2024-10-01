namespace _2GETHER
{
    class Status
    {
        public void GetStatusInfo(Player player, IOManager ioManager, Inventory inventory)
        {
            EquipmentItem equippedWeapon = player.WeaponEquipment[0] as EquipmentItem;
            EquipmentItem equippedArmor = player.ArmorEquipment[0] as EquipmentItem;

            int weaponDamage = equippedWeapon != null ? equippedWeapon.ItemATK : 0;
            int armorDefense = equippedArmor != null ? equippedArmor.ItemDEF : 0;

            string[] statusInfo = new string[]
            {
                "상태 보기",
                "",
                "캐릭터의 정보가 표시됩니다.",
                "",
                $"Lv.{player.Level}",
                $"{player.Name} ({player.Job})",
                $"공격력 : {player.Attack} + {weaponDamage}",
                $"방어력 : {player.Defense} + {armorDefense}",
                $"체  력 : {player.Hp} / {player.MaxHp}",
                $"M P : {player.Mp} / {player.MaxMp}",    
                $"경험치 : {player.Exp.ToString("N0")} / {player.MaxExp.ToString("N0")}",
                $"Gold : {player.Gold.ToString("N0")} G",
                "",
                "나가시려면 아무키나 눌러주세요."
            };

            ioManager.PrintMessage(statusInfo, true);
            Console.ReadKey(true);
        }
    }
}
