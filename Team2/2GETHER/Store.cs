using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace _2GETHER
{
    class Store
    {
        public void EnterStore(Player player, IOManager ioManager, ItemManager itemManager)
        {
            int selectNumber = -1;
            int inputNum = 0;
            do
            {
                ioManager.PrintMessage(ShowStoreWindow(player), true);
                //그냥 상점창
                if (selectNumber == -1)
                {
                    ioManager.PrintMessage(GetStoreEquipmentItemList(player, itemManager), false);
                    selectNumber = ioManager.PrintMessageWithNumberForSelectZeroExit(ShowStoreMenu(), false);
                }
                //구매하기 상점창
                else if (selectNumber == 1)
                {
                    inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetStoreEquipmentItemList(player, itemManager), GetInventoryItemList(player), false);
                    selectNumber = ItemBuy(inputNum, player, itemManager);
                }
                else if (selectNumber == 2)
                {
                    //if
                    inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetInventoryItemList(player), false);
                    selectNumber = ItemSell(inputNum, player, itemManager);
                    //esle if
                }
            }
            while (selectNumber != 0);
        }

        public string[] ShowStoreWindow(Player player)
        {
            List<string> storeList = new List<string>()
            {
                $"{player.Job}전용 상점",
                "필요한 아이템을 얻을 수 있는 상점입니다.\n",
                "[보유 골드]",
                $"{player.Gold} G\n",
                "[아이템 목록]"
            };
            return storeList.ToArray();
        }

        public string[] ShowStoreMenu()
        {
            List<string> menuList = new List<string>()
            {
                "아이템 구매",
                "아이템 판매"
            };
            return menuList.ToArray();
        }

        public string[] GetStoreEquipmentItemList(Player player, ItemManager itemManager)
        {
            List<string> storeItemList = new List<string>();
            foreach (var equipmentItem in itemManager.GetItemsByJob(player.Job))
            {
                storeItemList.Add(equipmentItem.GetItemInfo(EItemDo.ItemBuy));
            }
            return storeItemList.ToArray();
        }
        public string[] GetStoreConsumableItemList(Player player, ItemManager itemManager)
        {
            List<string> storeItemList = new List<string>();
            foreach (var consumableItem in itemManager.GetItemsByJob(player.Job))
            {
                storeItemList.Add(consumableItem.GetItemInfo(EItemDo.ItemBuy));
            }
            return storeItemList.ToArray();
        }
        public string[] GetInventoryItemList(Player player)
        {
            List<string> inventoryItemList = new List<string>();
            foreach (var item in player.equipmentInventory)
            {
                inventoryItemList.Add(item.GetItemInfo(EItemDo.ItemSell));
            }
            return inventoryItemList.ToArray();
        }
        public int ItemBuy(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                EquipmentItem selectItem = itemManager.GetItemsByJob(player.Job)[inputNum - 1];
                if (selectItem.Price <= player.Gold)
                {
                    player.Buy(selectItem);
                    selectItem.AddCount();
                    if (!player.equipmentInventory.Contains(selectItem))
                    { player.equipmentInventory.Add(selectItem); }
                }
                //if 샀는데 이미 같은 이름이 있다 -> 추가 안하고 해당아이템.Count +1
                else
                {
                    Console.WriteLine("\n돈이 모자랍니다.\n아무 키나 입력해주세요");
                    Console.ReadKey();
                }
            }
            return 1;
        }

        public int ItemSell(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else //낱개
            {
                EquipmentItem selectItem = player.equipmentInventory[inputNum - 1];
                if (!selectItem.IsPlayerEquip)
                {
                    player.Sell(selectItem);
                    selectItem.RemoveCount();
                    if (selectItem.ItemCount == 0)
                        player.equipmentInventory.Remove(selectItem);
                }
                else if (selectItem.IsPlayerEquip && selectItem.ItemCount > 1)
                {
                    player.Sell(selectItem);
                    selectItem.RemoveCount();
                }
                else
                {
                    Console.WriteLine("\n장착 아이템은 판매할 수 없습니다.\n아무 키나 입력해주세요");
                    Console.ReadKey();
                }
            }
            //else 묶음판매
            return 2;
        }
    }
}
