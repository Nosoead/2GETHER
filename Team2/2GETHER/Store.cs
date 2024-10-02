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
                switch (selectNumber)
                {
                    case -1:
                        ioManager.PrintMessage(ShowStoreStart(player, selectNumber), true);
                        selectNumber = ShowStore(player, itemManager, ioManager);
                        break;
                    case 1:
                        ioManager.PrintMessage(ShowStoreStart(player, selectNumber), true);
                        selectNumber = ShowEquipmentBuy(player, itemManager, ioManager);
                        break;
                    case 2:
                        ioManager.PrintMessage(ShowStoreStart(player, selectNumber), true);
                        selectNumber = ShowEquipmentSell(player, itemManager, ioManager);
                        break;
                    case 3:
                        ioManager.PrintMessage(ShowStoreStart(player, selectNumber), true);
                        selectNumber = ShowConsumptionBuy(player, itemManager, ioManager);
                        break;
                    case 4:
                        ioManager.PrintMessage(ShowStoreStart(player, selectNumber), true);
                        selectNumber = ShowConsumptionSell(player, itemManager, ioManager);
                        break;
                }
            }
            while (selectNumber != 0);
        }

        //첫화면 함수
        public int ShowStore(Player player, ItemManager itemManager, IOManager ioManager)
        {
            ioManager.PrintMessage("[장비 아이템 목록]", false);
            ioManager.PrintMessage(GetStoreEquipmentItemList(player, itemManager), false,true);
            ioManager.PrintConsoleLine();
            ioManager.PrintMessage("[소비 아이템 목록]", false);
            ioManager.PrintMessage(GetStoreConsumableItemList(player, itemManager), false);
            ioManager.PrintConsoleLine();
            int menuselect = ioManager.PrintMessageWithNumberForSelectZeroExit(ShowStoreEnd(), false);
            return menuselect;
        }
        //장비 아이템 상점 함수
        public int ShowEquipmentBuy(Player player, ItemManager itemManager, IOManager ioManager)
        {
            ioManager.PrintMessage("[장비 아이템 목록]", false);
            ioManager.PrintMessageWithNumberNoSelect(GetStoreEquipmentItemList(player, itemManager), false);
            ioManager.PrintConsoleLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            ioManager.PrintMessage("[소비 아이템 목록]", false);
            ioManager.PrintMessage(GetStoreConsumableItemList(player, itemManager), false);
            Console.ResetColor();
            ioManager.PrintConsoleLine();
            ioManager.PrintMessage("[보유 장비 아이템 목록]", false);
            ioManager.PrintMessageWithNumberNoSelect(GetEquipmentItemList(player), false);
            ioManager.PrintConsoleLine();
            int inputNum = ioManager.PrintMessageAndSelectNum("\n0. 취소/나가기", itemManager.GetItemsByJob(player.Job).Count);
            int menuselect = EquipmentBuy(inputNum, player, itemManager);
            return menuselect;

        }

        //장비 아이템 판매 함수
        public int ShowEquipmentSell(Player player, ItemManager itemManager, IOManager ioManager)
        {
            ioManager.PrintMessage("[장비 아이템 목록]", false);
            int inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetEquipmentItemList(player), false);
            int menuselect = EquipmentSell(inputNum, player, itemManager);
            return menuselect;

        }

        //소비 아이템 상점 함수
        public int ShowConsumptionBuy(Player player, ItemManager itemManager, IOManager ioManager)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            ioManager.PrintMessage("[장비 아이템 목록]", false);
            ioManager.PrintMessage(GetStoreEquipmentItemList(player, itemManager), false);
            Console.ResetColor();
            ioManager.PrintConsoleLine();
            ioManager.PrintMessage("[소비 아이템 목록]", false);
            ioManager.PrintMessageWithNumberNoSelect(GetStoreConsumableItemList(player, itemManager), false);
            ioManager.PrintConsoleLine();
            ioManager.PrintMessage("[보유 소비 아이템 목록]", false);
            ioManager.PrintMessageWithNumberNoSelect(GetConsumableItemList(player), false);
            ioManager.PrintConsoleLine();
            int inputNum = ioManager.PrintMessageAndSelectNum("\n0. 취소/나가기", itemManager.consumableItemList.Count);
            int menuselect = ConsumptionBuy(inputNum, player, itemManager);
            return menuselect;

        }
        //소비 아이템 판매 함수
        public int ShowConsumptionSell(Player player, ItemManager itemManager, IOManager ioManager)
        {
            ioManager.PrintMessage("[소비 아이템 목록]", false);
            int inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetConsumableItemList(player), false);
            int menuselect = ConsumptionSell(inputNum, player, itemManager);
            return menuselect;

        }
        public string[] ShowStoreStart(Player player, int selectNum)
        {
            List<string> storeList = new List<string>()
            {
                $"{player.Job}전용 상점" + (selectNum==1? " - 장비 아이템 구매":
                                            selectNum==2? " - 장비 아이템 판매":
                                            selectNum==3? " - 소비 아이템 구매":
                                            selectNum==4? " - 소비 아이템 판매":""),
                "필요한 아이템을 얻을 수 있는 상점입니다.\n",
                "[보유 골드]",
                $"{player.Gold} G\n",
            };
            return storeList.ToArray();
        }

        public string[] ShowStoreEnd()
        {
            List<string> menuList = new List<string>()
            {
                "장비 아이템 구매",
                "장비 아이템 판매\n",
                "소비 아이템 구매",
                "소비 아이템 판매"
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
            foreach (var consumableItem in itemManager.consumableItemList)
            {
                storeItemList.Add(consumableItem.GetItemInfo(EItemDo.ItemBuy));
            }
            return storeItemList.ToArray();
        }
        public string[] GetEquipmentItemList(Player player)
        {
            List<string> EquipmentItemList = new List<string>();
            foreach (var item in player.equipmentInventory)
            {
                EquipmentItemList.Add(item.GetItemInfo(EItemDo.ItemSell));
            }
            return EquipmentItemList.ToArray();
        }
        public string[] GetConsumableItemList(Player player)
        {
            List<string> ConsumableItemList = new List<string>();
            foreach (var item in player.consumableInventory)
            {
                ConsumableItemList.Add(item.GetItemInfo(EItemDo.ItemSell));
            }
            return ConsumableItemList.ToArray();
        }
        public int EquipmentBuy(int inputNum, Player player, ItemManager itemManager)
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
                else
                {
                    Console.WriteLine("\n돈이 모자랍니다.\n아무 키나 입력해주세요");
                    Console.ReadKey();
                }
            }
            return 1;
        }
        public int EquipmentSell(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
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
            return 2;
        }
        public int ConsumptionBuy(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                ConsumableItem selectItem = itemManager.consumableItemList[inputNum - 1];
                if (selectItem.Price <= player.Gold)
                {
                    player.Buy(selectItem);
                    selectItem.AddCount();
                    player.Get(selectItem);
                    if (!player.consumableInventory.Contains(selectItem))
                    { player.consumableInventory.Add(selectItem); }
                }
                //if 샀는데 이미 같은 이름이 있다 -> 추가 안하고 해당아이템.Count +1
                else
                {
                    Console.WriteLine("\n돈이 모자랍니다.\n아무 키나 입력해주세요");
                    Console.ReadKey();
                }
            }
            return 3;
        }
        public int ConsumptionSell(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                ConsumableItem selectItem = player.consumableInventory[inputNum - 1];
                player.Sell(selectItem);
                selectItem.RemoveCount();
                if (selectItem.ItemCount == 0)
                    player.consumableInventory.Remove(selectItem);
            }
            return 4;
        }
    }
}
