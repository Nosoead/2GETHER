namespace _2GETHER
{
    class Inventory
    {
        public void EnterInventory(Player player, IOManager ioManager, ItemManager itemManager)
        {
            int selectNumber = -1;
            int inputNum = 0;
            do
            {
                ioManager.PrintMessage(ShowInventoryWindow(player), true);
                //그냥 상점창
                if (selectNumber == -1)
                {
                    ioManager.PrintMessage(GetInventoryItemList(player), false);
                    selectNumber = ioManager.PrintMessageWithNumberForSelectZeroExit(ShowInventoryMenu(), false);
                }
                //구매하기 상점창
                else if (selectNumber == 1)
                {
                    inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetInventoryItemList(player), false);
                    selectNumber = ItemEquipment(inputNum, player, itemManager);
                }
            }
            while (selectNumber != 0);
        }
        public string[] ShowInventoryWindow(Player player)
        {
            List<string> storeList = new List<string>()
            {
                "인벤토리",
                "보유 중인 아이템을 관리할 수 있습니다.\n",
                "[아이템 목록]"
            };
            return storeList.ToArray();
        }

        public string[] ShowInventoryMenu()
        {
            List<string> menuList = new List<string>()
            {
                "장착 관리"
            };
            return menuList.ToArray();
        }
        public string[] GetInventoryItemList(Player player)
        {
            List<string> inventoryItemList = new List<string>();
            foreach (var equipmentItem in player.equipmentInventory)
            {
                inventoryItemList.Add(equipmentItem.GetItemInfo(EItemDo.ItemView));
            }
            return inventoryItemList.ToArray();
        }

        public int ItemEquipment(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                // EquipItem(inputNum, player);
                Item selectItem = player.equipmentInventory[inputNum - 1];
                if (selectItem.eItemType==EItemType.Armor)
                {
                    EquipArmor(inputNum, player);
                }
                else if (selectItem.eItemType == EItemType.Weapon)
                {
                    EquipWeapon(inputNum, player);
                }
            }
            return 1;
        }

        public void EquipArmor(int inputNum, Player player)
        {
            if (player.ArmorEquipment[0] == null)
            {
                player.equipmentInventory[inputNum - 1].IsPlayerEquip = true;
                player.ArmorEquipment[0] = player.equipmentInventory[inputNum - 1];
                player.UpdateStatsOnEquip(player.ArmorEquipment[0]);
            }
            else
            {

                if (player.ArmorEquipment[0].eItem == player.equipmentInventory[inputNum - 1].eItem)
                {
                    player.UpdateStatsOnUnequip(player.ArmorEquipment[0]);
                    player.ArmorEquipment[0] = null;
                    player.equipmentInventory[inputNum - 1].IsPlayerEquip = false;
                }
                else
                {
                    for (int i = 0; i < player.equipmentInventory.Count; i++)
                    {
                        if (player.ArmorEquipment[0].eItem == player.equipmentInventory[i].eItem)
                        {
                            player.equipmentInventory[i].IsPlayerEquip = false;
                            player.UpdateStatsOnUnequip(player.equipmentInventory[i]);
                            player.equipmentInventory[inputNum - 1].IsPlayerEquip = true;
                            player.ArmorEquipment[0] = player.equipmentInventory[inputNum - 1];
                            player.UpdateStatsOnEquip(player.ArmorEquipment[0]);
                        }
                    }
                }
            }
        }
        public void EquipWeapon(int inputNum, Player player)
        {
            if (player.WeaponEquipment[0] == null)
            {
                player.equipmentInventory[inputNum - 1].IsPlayerEquip = true;
                player.WeaponEquipment[0] = player.equipmentInventory[inputNum - 1];
                player.UpdateStatsOnEquip(player.WeaponEquipment[0]);
            }
            else
            {

                if (player.WeaponEquipment[0].eItem == player.equipmentInventory[inputNum - 1].eItem)
                {
                    player.UpdateStatsOnUnequip(player.WeaponEquipment[0]);
                    player.WeaponEquipment[0] = null;
                    player.equipmentInventory[inputNum - 1].IsPlayerEquip = false;
                }
                else
                {
                    for (int i = 0; i < player.equipmentInventory.Count; i++)
                    {
                        if (player.WeaponEquipment[0].eItem == player.equipmentInventory[i].eItem)
                        {
                            player.equipmentInventory[i].IsPlayerEquip = false;
                            player.UpdateStatsOnUnequip(player.equipmentInventory[i]);
                            player.equipmentInventory[inputNum - 1].IsPlayerEquip = true;
                            player.WeaponEquipment[0] = player.equipmentInventory[inputNum - 1];
                            player.UpdateStatsOnEquip(player.WeaponEquipment[0]);
                        }
                    }
                }
            }
        }

    }
}
