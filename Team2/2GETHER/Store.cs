using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace _2GETHER
{
    public enum EItem
    {
        수련자_갑옷 = 1,
        무쇠갑옷,
        스파르타의_갑옷,
        낡은_검,
        청동_동끼,
        스파르타의_창
    }

    public enum EItemType
    {
        Armor = 1,
        Weapon
    }

    class Item
    {
        public EItem eItem;
        public EItemType eItemType;
        public int price;
        public int itemATK;
        public int itemDEF;
        public bool isArmor;
        public bool isPlayerBuy;
        public bool isPlayerEquip;
        public string itemDescription;

        public Item(EItem eItem, EItemType eItemType, int price, int itemATK, int itemDEF, bool isArmor, bool isPlayerBuy, bool isPlayerEquip, string itemDescription)
        {
            this.eItem = eItem;
            this.eItemType = eItemType;
            this.price = price;
            this.itemATK = itemATK;
            this.itemDEF = itemDEF;
            this.isArmor = isArmor;
            this.isPlayerBuy = isPlayerBuy;
            this.isPlayerEquip = isPlayerEquip;
            this.itemDescription = itemDescription;
        }

        public string GetitemInfo(bool isStore) //bool isItemTag store or inventory
        {
            string iteminfo = "";
            if (isStore)
            {
                iteminfo =
                      (isPlayerEquip ? "[E] " : "")
                    + ($"{eItem}")
                    + (isArmor ? $"| 방어력 + {itemDEF}" : $"| 공격력 + {itemATK}")
                    + $"| {itemDescription}"
                    + (isPlayerBuy ? "| 구매완료" : $"| {price} G");
            }
            else
            {
                iteminfo =
                      (isPlayerEquip ? "[E] " : "")
                    + ($"{eItem}")
                    + (isArmor ? $"| 방어력 + {itemDEF}" : $"| 공격력 + {itemATK}")
                    + $"| {itemDescription}";
            }
            return iteminfo;
        }
    }

    class ItemManager
    {
        public List<Item> items = new List<Item>();
        #region // 아이템 생성 리스트

        public ItemManager()
        {
            items.Add(new Item(
                eItem: (EItem)1,
                eItemType: EItemType.Armor,
                price: 1000,
                itemATK: 0,
                itemDEF: 5,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "수련에 도움을 주는 갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)2,
                eItemType: EItemType.Armor,
                price: 1750,
                itemATK: 0,
                itemDEF: 9,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "무쇠로 만들어져 튼튼한 갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)3,
                eItemType: EItemType.Armor,
                price: 3500,
                itemATK: 0,
                itemDEF: 15,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)4,
                eItemType: EItemType.Weapon,
                price: 600,
                itemATK: 2,
                itemDEF: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "쉽게 볼 수 있는 낡은 검 입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)5,
                eItemType: EItemType.Weapon,
                price: 1500,
                itemATK: 5,
                itemDEF: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "어디선가 사용됐던거 같은 도끼입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)6,
                eItemType: EItemType.Weapon,
                price: 2400,
                itemATK: 7,
                itemDEF: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "스파르타의 전사들이 사용했다는 전설의 창입니다."
             ));
        }
        #endregion
    }

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
                    ioManager.PrintMessageWithNumberNoSelect(GetStoreItemList(itemManager), false);
                    selectNumber = ioManager.PrintMessageWithNumberForSelect(ShowStoreMenu(), false);
                }
                //구매하기 상점창
                else if (selectNumber == 1)
                {
                    inputNum = ioManager.PrintMessageWithNumberForSelect(GetStoreItemList(itemManager), false);
                    selectNumber = ItemPurchase(inputNum, player, itemManager);
                }
                else if (selectNumber == 2)
                {
                    inputNum = ioManager.PrintMessageWithNumberForSelect(GetInventoryItemList(player), false);
                    selectNumber = ItemSale(inputNum, player, itemManager);
                }
            }
            while (selectNumber != 0);
        }

        public string[] ShowStoreWindow(Player player)
        {
            List<string> storeList = new List<string>()
            {
                "상점",
                "필요한 아이템을 얻을 수 있는 상점입니다.\n",
                "[보유 골드]",
                $"{player.gold} G\n",
                "[아이템 목록]"
            };
            return storeList.ToArray();
        }

        public string[] ShowStoreMenu()
        {
            List<string> menuList = new List<string>()
            {
                "아이템 구매",
                "아이템 판매",
                "나가기"
            };
            return menuList.ToArray();
        }

        public string[] GetStoreItemList(ItemManager itemManager)
        {
            List<string> storeItemList = new List<string>();
            foreach (var item in itemManager.items)
            {
                storeItemList.Add(item.GetitemInfo(true));
            }
            return storeItemList.ToArray();
        }

        public string[] GetInventoryItemList(Player player)
        {
            List<string> inventoryItemList = new List<string>();
            foreach (var item in player.InventoryItems)
            {
                inventoryItemList.Add(item.GetitemInfo(true));
            }
            return inventoryItemList.ToArray();
        }
        public int ItemPurchase(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                Item selectItem = itemManager.items[inputNum - 1];
                if (!selectItem.isPlayerBuy)
                {
                    selectItem.isPlayerBuy = true;
                    player.InventoryItems.Add(selectItem);
                }
                else
                {
                    Console.WriteLine("\n이미 구매한 항목입니다.\n아무 키나 입력해주세요");
                    Console.ReadKey();
                }
            }
            return 1;
        }

        public int ItemSale(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                Item selectItem = player.InventoryItems[inputNum - 1];
                for (int i = 0; i < itemManager.items.Count; i++)
                {
                    if (itemManager.items[i].eItem == selectItem.eItem)
                    { itemManager.items[i].isPlayerBuy = false; }
                }
                player.InventoryItems.Remove(selectItem);

            }
            return 2;
        }
    }

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
                    ioManager.PrintMessageWithNumberNoSelect(GetInventoryItemList(player), false);
                    selectNumber = ioManager.PrintMessageWithNumberForSelect(ShowInventoryMenu(), false);
                }
                //구매하기 상점창
                else if (selectNumber == 1)
                {
                    inputNum = ioManager.PrintMessageWithNumberForSelect(GetInventoryItemList(player), false);
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
                "장착 관리",
                "나가기"
            };
            return menuList.ToArray();
        }
        public string[] GetInventoryItemList(Player player)
        {
            List<string> inventoryItemList = new List<string>();
            foreach (var item in player.InventoryItems)
            {
                inventoryItemList.Add(item.GetitemInfo(false));
            }
            return inventoryItemList.ToArray();
        }

        public int ItemEquipment(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                Item selectItem = player.InventoryItems[inputNum - 1];
                if (selectItem.isArmor)
                {
                    EquipArmor(inputNum, player);
                }
                else
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
                player.InventoryItems[inputNum - 1].isPlayerEquip = true;
                player.ArmorEquipment[0] = player.InventoryItems[inputNum - 1];
            }
            else
            {

                if (player.ArmorEquipment[0].eItem == player.InventoryItems[inputNum - 1].eItem)
                {
                    player.ArmorEquipment[0] = null;
                    player.InventoryItems[inputNum-1].isPlayerEquip = false;
                }
                else
                {
                    for (int i = 0; i < player.InventoryItems.Count; i++)
                    {
                        if (player.ArmorEquipment[0].eItem == player.InventoryItems[i].eItem)
                        {
                            player.InventoryItems[i].isPlayerEquip = false;
                            player.InventoryItems[inputNum - 1].isPlayerEquip = true;
                            player.ArmorEquipment[0] = player.InventoryItems[inputNum - 1];
                        }
                    }
                }
            }
        }

        public void EquipWeapon(int inputNum, Player player)
        {
            if (player.WeaponEquipment[0] == null)
            {
                player.InventoryItems[inputNum - 1].isPlayerEquip = true;
                player.WeaponEquipment[0] = player.InventoryItems[inputNum - 1];
            }
            else
            {

                if (player.WeaponEquipment[0].eItem == player.InventoryItems[inputNum - 1].eItem)
                {
                    player.WeaponEquipment[0] = null;
                    player.InventoryItems[inputNum-1].isPlayerEquip = false;
                }
                else
                {
                    for (int i = 0; i < player.InventoryItems.Count; i++)
                    {
                        if (player.WeaponEquipment[0].eItem == player.InventoryItems[i].eItem)
                        {
                            player.InventoryItems[i].isPlayerEquip = false;
                            player.InventoryItems[inputNum - 1].isPlayerEquip = true;
                            player.WeaponEquipment[0] = player.InventoryItems[inputNum - 1];
                        }
                    }
                }
            }
        }

    }
}
