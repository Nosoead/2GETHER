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
                switch (selectNumber)
                {
                    case -1:
                        ioManager.PrintMessage(ShowInventoryStart(player, selectNumber), true);
                        selectNumber = ShowInventory(player, itemManager, ioManager);
                        break;
                    case 1:
                        ioManager.PrintMessage(ShowInventoryStart(player, selectNumber), true);
                        selectNumber = ShowEquipmentOnOff(player, itemManager, ioManager);
                        break;
                    case 2:
                        ioManager.PrintMessage(ShowInventoryStart(player, selectNumber), true);
                        selectNumber = ShowUsePotion(player, itemManager, ioManager);
                        break;
                }
            }
            while (selectNumber != 0);
        }
        //인벤토리 첫화면 함수
        public int ShowInventory(Player player, ItemManager itemManager, IOManager ioManager)
        {
            int menuselect = ioManager.PrintMessageWithNumberForSelectZeroExit(ShowInventoryEnd(), false);
            return menuselect;
        }

        //장착화면함수
        public int ShowEquipmentOnOff(Player player, ItemManager itemManager, IOManager ioManager)
        {
            ioManager.PrintMessage("[장비 아이템 목록]", false);
            int inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetEquipmentItemList(player), false);
            int menuselect = EquipItem(inputNum, player, itemManager);
            return menuselect;
        }
        //회복화면 함수
        public int ShowUsePotion(Player player, ItemManager itemManager, IOManager ioManager)
        {
            ioManager.PrintMessage("[소비 아이템 목록]", false);
            int inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetConsumableItemList(player), false);
            int menuselect = UseItem(inputNum, player, itemManager);
            return menuselect;
        }
        public string[] ShowInventoryStart(Player player, int selectNum)
        {
            List<string> storeList = new List<string>()
            {
                $"{player.Name}의 인벤토리"+ (selectNum==1? " - 장착관리":
                                            selectNum==2? " - 소비 아이템 관리":""),
                selectNum==2? $"보유 중인 포션을 사용하여 체력을 회복 할 수 있습니다. [보유량 : {player.Potions}]\n":
                                "보유 중인 아이템을 관리할 수 있습니다.\n",
                selectNum==1 ? "[아이템 목록] - 장비":
                selectNum ==2 ? "[아이템 목록] -소비":"[아이템 목록]"
            };
            return storeList.ToArray();
        }

        public string[] ShowInventoryEnd()
        {
            List<string> menuList = new List<string>()
            {
                "장착 관리\n",
                "회복 아이템"
            };
            return menuList.ToArray();
        }
        public string[] GetEquipmentItemList(Player player)
        {
            List<string> EquipmentItemList = new List<string>();
            foreach (var item in player.equipmentInventory)
            {
                EquipmentItemList.Add(item.GetItemInfo(EItemDo.ItemView));
            }
            return EquipmentItemList.ToArray();
        }
        public string[] GetConsumableItemList(Player player)
        {
            List<string> ConsumableItemList = new List<string>();
            foreach (var item in player.consumableInventory)
            {
                ConsumableItemList.Add(item.GetItemInfo(EItemDo.ItemView));
            }
            return ConsumableItemList.ToArray();
        }
        public int EquipItem(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                // EquipItem(inputNum, player);
                EquipmentItem selectItem = player.equipmentInventory[inputNum - 1];
                if (player.Job == selectItem.eJob || selectItem.eJob == EJob.공용)
                {
                    if (selectItem.eItemType == EItemType.Armor)
                    {
                        EquipArmor(inputNum, player);
                    }
                    else if (selectItem.eItemType == EItemType.Weapon)
                    {
                        EquipWeapon(inputNum, player);
                    }
                }
                else
                {
                    Console.WriteLine($"{selectItem.eJob}장비는 {player.Job}(이)가 장착할 수 없습니다.");
                    Console.ReadKey(true);
                }
            }
            return 1;
        }

        public void EquipArmor(int inputNum, Player player)
        {
            if (player.armorEquipment[0] == null)
            {
                player.equipmentInventory[inputNum - 1].IsPlayerEquip = true;
                player.armorEquipment[0] = player.equipmentInventory[inputNum - 1];
                player.UpdateStatsOnEquip(player.armorEquipment[0]);
            }
            else
            {
                if (player.armorEquipment[0].eItem == player.equipmentInventory[inputNum - 1].eItem)
                {
                    player.UpdateStatsOnUnequip(player.armorEquipment[0]);
                    player.armorEquipment[0] = null;
                    player.equipmentInventory[inputNum - 1].IsPlayerEquip = false;
                }
                else
                {
                    for (int i = 0; i < player.equipmentInventory.Count; i++)
                    {
                        if (player.armorEquipment[0].eItem == player.equipmentInventory[i].eItem)
                        {
                            player.equipmentInventory[i].IsPlayerEquip = false;
                            player.UpdateStatsOnUnequip(player.equipmentInventory[i]);
                            player.equipmentInventory[inputNum - 1].IsPlayerEquip = true;
                            player.armorEquipment[0] = player.equipmentInventory[inputNum - 1];
                            player.UpdateStatsOnEquip(player.armorEquipment[0]);
                        }
                    }
                }
            }
        }
        public void EquipWeapon(int inputNum, Player player)
        {
            if (player.weaponEquipment[0] == null)
            {
                player.equipmentInventory[inputNum - 1].IsPlayerEquip = true;
                player.weaponEquipment[0] = player.equipmentInventory[inputNum - 1];
                player.UpdateStatsOnEquip(player.weaponEquipment[0]);
            }
            else
            {

                if (player.weaponEquipment[0].eItem == player.equipmentInventory[inputNum - 1].eItem)
                {
                    player.UpdateStatsOnUnequip(player.weaponEquipment[0]);
                    player.weaponEquipment[0] = null;
                    player.equipmentInventory[inputNum - 1].IsPlayerEquip = false;
                }
                else
                {
                    for (int i = 0; i < player.equipmentInventory.Count; i++)
                    {
                        if (player.weaponEquipment[0].eItem == player.equipmentInventory[i].eItem)
                        {
                            player.equipmentInventory[i].IsPlayerEquip = false;
                            player.UpdateStatsOnUnequip(player.equipmentInventory[i]);
                            player.equipmentInventory[inputNum - 1].IsPlayerEquip = true;
                            player.weaponEquipment[0] = player.equipmentInventory[inputNum - 1];
                            player.UpdateStatsOnEquip(player.weaponEquipment[0]);
                        }
                    }
                }
            }
        }

        public int UseItem(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                ConsumableItem selectItem = player.consumableInventory[inputNum - 1];
                player.UsePotion(selectItem);

                if (selectItem.ItemCount == 0)
                    player.consumableInventory.Remove(selectItem);
            }
            return 2;
        }
    }
}
