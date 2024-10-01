using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace _2GETHER
{
    public enum EItem
    {
        누더기_갑옷 = 1,
        구리갑옷,
        르탄의_갑옷,
        나뭇가지,
        청동_거울,
        르탄의_창,
        빨간_포션,

        낡은_검 = 101,
        강철_검,
        청동_도끼,
        엑스칼리버,
        수련자_갑옷,
        무쇠_갑옷,
        스파르타_갑옷,

        낡은_석궁 = 201,
        강철_활,
        다이아몬드_활,
        아스트라,
        수련자_가죽갑옷,
        무쇠_가죽갑옷,
        스파르타_가죽갑옷,

        낡은_지팡이 = 301,
        강철_지팡이,
        마법_지팡이,
        아티쉬,
        수련자_로브,
        무쇠_로브,
        스파르타_로브
    }

    public enum EItemType
    {
        Armor = 1,
        Weapon,
        Potion
    }

    public enum EItemDo
    {
        ItemBuy,
        ItemSell,
        ItemView
    }
    class Item
    {
        public EItem eItem;
        public EItemType eItemType;
        public EJob eJob;
        public int price;
        public int itemATK;
        public int itemDEF;
        public int itemHeal;
        public bool isArmor;
        public bool isPlayerBuy;
        public bool isPlayerEquip;
        public string itemDescription;

        public Item(EItem eItem, EItemType eItemType, EJob eJob, int price, int itemATK, int itemDEF, int itemHeal, bool isArmor, bool isPlayerBuy, bool isPlayerEquip, string itemDescription)
        {
            this.eItem = eItem;
            this.eItemType = eItemType;
            this.eJob = eJob;
            this.price = price;
            this.itemATK = itemATK;
            this.itemDEF = itemDEF;
            this.itemHeal = itemHeal;
            this.isArmor = isArmor;
            this.isPlayerBuy = isPlayerBuy;
            this.isPlayerEquip = isPlayerEquip;
            this.itemDescription = itemDescription;
        }

        public string GetitemInfo(EItemDo eItemDo) //bool isItemTag store or inventory
        {
            string iteminfo = "";

            switch (eItemDo)
            {
                case EItemDo.ItemBuy:
                    iteminfo =
                          (isPlayerEquip ? "[E] " : "")
                        + ($"{eItem}")
                        + (isArmor ? $"| 방어력 + {itemDEF}" : $"| 공격력 + {itemATK}")
                        + $"| {eJob}"
                        + $"| {itemDescription}"
                        + (isPlayerBuy ? "| 구매완료" : $"| {price} G");
                    break;
                case EItemDo.ItemSell:
                    iteminfo =
                          (isPlayerEquip ? "[E] " : "")
                        + ($"{eItem}")
                        + (isArmor ? $"| 방어력 + {itemDEF}" : $"| 공격력 + {itemATK}")
                        + $"| {eJob}"
                        + $"| {itemDescription}"
                        + (isPlayerBuy ? $"| {price * 85 / 100} G" : "");
                    break;
                case EItemDo.ItemView:
                    iteminfo =
                          (isPlayerEquip ? "[E] " : "")
                        + ($"{eItem}")
                        + (isArmor ? $"| 방어력 + {itemDEF}" : $"| 공격력 + {itemATK}")
                        + $"| {eJob}"
                        + $"| {itemDescription}";
                    break;
            }
            return iteminfo;
        }
    }

    class ItemManager
    {
        public List<Item> items = new List<Item>();
        //public List<Item> jobItems = new List<Item>();
        
        public ItemManager()
        {
            #region // 공용아이템 생성 리스트
            items.Add(new Item(
                eItem: (EItem)1,
                eItemType: EItemType.Armor,
                eJob: EJob.공용,
                price: 1000,
                itemATK: 0,
                itemDEF: 5,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "수련에 도움을 주는 갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)2,
                eItemType: EItemType.Armor,
                eJob: EJob.공용,
                price: 1750,
                itemATK: 0,
                itemDEF: 9,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "무쇠로 만들어져 튼튼한 갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)3,
                eItemType: EItemType.Armor,
                eJob: EJob.공용,
                price: 3500,
                itemATK: 0,
                itemDEF: 15,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)4,
                eItemType: EItemType.Weapon,
                eJob: EJob.공용,
                price: 600,
                itemATK: 2,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "쉽게 볼 수 있는 낡은 검 입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)5,
                eItemType: EItemType.Weapon,
                eJob: EJob.공용,
                price: 1500,
                itemATK: 5,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "어디선가 사용됐던거 같은 도끼입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)6,
                eItemType: EItemType.Weapon,
                eJob: EJob.공용,
                price: 2400,
                itemATK: 7,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "스파르타의 전사들이 사용했다는 전설의 창입니다."
             ));
            items.Add(new Item(
                eItem: (EItem)7,
                eItemType: EItemType.Potion,
                eJob: EJob.공용,
                price: 500,
                itemATK: 0,
                itemDEF: 0,
                itemHeal: 30,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "마시면 기분이 좋아지는 약입니다."
             ));
            #endregion
            #region // 전사아이템 생성 리스트
            items.Add(new Item(
                eItem: (EItem)101,
                eItemType: EItemType.Weapon,
                eJob: EJob.전사,
                price: 600,
                itemATK: 2,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "쉽게 볼 수 있는 낡은 검입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)102,
                eItemType: EItemType.Weapon,
                eJob: EJob.전사,
                price: 1000,
                itemATK: 5,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "강철로 만든 검입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)103,
                eItemType: EItemType.Weapon,
                eJob: EJob.전사,
                price: 1500,
                itemATK: 10,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "어디선가 사용됐던 도끼입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)104,
                eItemType: EItemType.Weapon,
                eJob: EJob.전사,
                price: 4000,
                itemATK: 20,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "전설적인 영웅 아서가 썼다고 전해지는 검입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)105,
                eItemType: EItemType.Armor,
                eJob: EJob.전사,
                price: 1000,
                itemATK: 0,
                itemDEF: 5,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "수련에 도움을 주는 갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)106,
                eItemType: EItemType.Armor,
                eJob: EJob.전사,
                price: 2000,
                itemATK: 0,
                itemDEF: 9,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "무쇠로 만들어져 튼튼한 갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)107,
                eItemType: EItemType.Armor,
                eJob: EJob.전사,
                price: 3000,
                itemATK: 0,
                itemDEF: 15,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "스파르타의 전사들이 사용한 전설의 갑옷입니다."
             ));
            #endregion
            #region // 궁수아이템 생성 리스트
            items.Add(new Item(
               eItem: (EItem)201,
               eItemType: EItemType.Weapon,
               eJob: EJob.궁수,
               price: 600,
               itemATK: 2,
               itemDEF: 0,
               itemHeal: 0,
               isArmor: false,
               isPlayerBuy: false,
               isPlayerEquip: false,
               itemDescription: "쉽게 볼 수 있는 낡은 석궁입니다."
            ));

            items.Add(new Item(
                eItem: (EItem)202,
                eItemType: EItemType.Weapon,
                eJob: EJob.궁수,
                price: 1000,
                itemATK: 5,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "강철로 만든 활입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)203,
                eItemType: EItemType.Weapon,
                eJob: EJob.궁수,
                price: 1500,
                itemATK: 10,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "강력한 다이아몬드 활입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)204,
                eItemType: EItemType.Weapon,
                eJob: EJob.궁수,
                price: 4000,
                itemATK: 20,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "고대 엘프 족장 아스트라가 썼다고 전해지는 활입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)205,
                eItemType: EItemType.Armor,
                eJob: EJob.궁수,
                price: 1000,
                itemATK: 0,
                itemDEF: 5,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "수련에 도움을 주는 가죽갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)206,
                eItemType: EItemType.Armor,
                eJob: EJob.궁수,
                price: 2000,
                itemATK: 0,
                itemDEF: 9,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "무쇠로 만들어져 튼튼한 가죽갑옷입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)207,
                eItemType: EItemType.Armor,
                eJob: EJob.궁수,
                price: 3000,
                itemATK: 0,
                itemDEF: 15,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "스파르타의 전사들이 사용한 전설의 가죽갑옷입니다."
             ));
            #endregion
            #region // 마법사아이템 생성 리스트
            items.Add(new Item(
               eItem: (EItem)301,
               eItemType: EItemType.Weapon,
               eJob: EJob.마법사,
               price: 600,
               itemATK: 2,
               itemDEF: 0,
               itemHeal: 0,
               isArmor: false,
               isPlayerBuy: false,
               isPlayerEquip: false,
               itemDescription: "쉽게 볼 수 있는 낡은 지팡이입니다."
            ));

            items.Add(new Item(
                eItem: (EItem)302,
                eItemType: EItemType.Weapon,
                eJob: EJob.마법사,
                price: 1000,
                itemATK: 5,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "강철로 만든 지팡이입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)303,
                eItemType: EItemType.Weapon,
                eJob: EJob.마법사,
                price: 1500,
                itemATK: 10,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "어디선가 사용됐던 마법 지팡이입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)304,
                eItemType: EItemType.Weapon,
                eJob: EJob.마법사,
                price: 4000,
                itemATK: 20,
                itemDEF: 0,
                itemHeal: 0,
                isArmor: false,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "9서클 대마법사 랑그란트가 사용한 지팡이입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)305,
                eItemType: EItemType.Armor,
                eJob: EJob.마법사,
                price: 1000,
                itemATK: 0,
                itemDEF: 5,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "수련에 도움을 주는 로브입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)306,
                eItemType: EItemType.Armor,
                eJob: EJob.마법사,
                price: 2000,
                itemATK: 0,
                itemDEF: 9,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "무쇠로 만들어져 튼튼한 로브입니다."
             ));

            items.Add(new Item(
                eItem: (EItem)307,
                eItemType: EItemType.Armor,
                eJob: EJob.마법사,
                price: 3000,
                itemATK: 0,
                itemDEF: 15,
                itemHeal: 0,
                isArmor: true,
                isPlayerBuy: false,
                isPlayerEquip: false,
                itemDescription: "스파르타의 전사들이 사용한 전설의 로브입니다."
             ));
            #endregion
        }
        public List<Item> GetItemsByJob(EJob eJob)
        {
            return items.Where(item => item.eJob == eJob).ToList();
        }
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
                    ioManager.PrintMessage(GetStoreItemList(player, itemManager), false);
                    selectNumber = ioManager.PrintMessageWithNumberForSelectZeroExit(ShowStoreMenu(), false);
                }
                //구매하기 상점창
                else if (selectNumber == 1)
                {
                    inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetStoreItemList(player, itemManager), false);
                    selectNumber = ItemBuy(inputNum, player, itemManager);
                }
                else if (selectNumber == 2)
                {
                    inputNum = ioManager.PrintMessageWithNumberForSelectZeroExit(GetInventoryItemList(player), false);
                    selectNumber = ItemSell(inputNum, player, itemManager);
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

        public string[] GetStoreItemList(Player player, ItemManager itemManager)
        {
            //List<Item> jobItemList = new List<Item>();
            //for (int itemIndex = 0; itemIndex < itemManager.items.Count; itemIndex++)
            //{
            //    if (player.Job == itemManager.items[itemIndex].eJob)
            //        jobItemList.Add(itemManager.items[itemIndex]);
            //}
            List<string> storeItemList = new List<string>();
            foreach (var item in itemManager.GetItemsByJob(player.Job))
            {
                storeItemList.Add(item.GetitemInfo(EItemDo.ItemBuy));
            }
            return storeItemList.ToArray();
        }

        public string[] GetInventoryItemList(Player player)
        {
            List<string> inventoryItemList = new List<string>();
            foreach (var item in player.InventoryItems)
            {
                inventoryItemList.Add(item.GetitemInfo(EItemDo.ItemSell));
            }
            return inventoryItemList.ToArray();
        }
        public int ItemBuy(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                Item selectItem = itemManager.GetItemsByJob(player.Job)[inputNum - 1];
                if (!selectItem.isPlayerBuy && selectItem.price <= player.Gold)
                {
                    selectItem.isPlayerBuy = true;
                    player.Buy(selectItem);
                    player.InventoryItems.Add(selectItem);
                }
                else if (!selectItem.isPlayerBuy && selectItem.price > player.Gold)
                {
                    Console.WriteLine("\n돈이 모자랍니다.\n아무 키나 입력해주세요");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\n이미 구매한 항목입니다.\n아무 키나 입력해주세요");
                    Console.ReadKey();
                }
            }
            return 1;
        }

        public int ItemSell(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                Item selectItem = player.InventoryItems[inputNum - 1];
                if (!selectItem.isPlayerEquip)
                {
                    for (int i = 0; i < itemManager.GetItemsByJob(player.Job).Count; i++)
                    {
                        if (itemManager.GetItemsByJob(player.Job)[i].eItem == selectItem.eItem)
                        { itemManager.items[i].isPlayerBuy = false; }
                    }
                    player.Sell(selectItem);
                    player.InventoryItems.Remove(selectItem);
                }
                else
                {
                    Console.WriteLine("\n장착 아이템은 판매할 수 없습니다.\n아무 키나 입력해주세요");
                    Console.ReadKey();
                }
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
            foreach (var item in player.InventoryItems)
            {
                inventoryItemList.Add(item.GetitemInfo(EItemDo.ItemView));
            }
            return inventoryItemList.ToArray();
        }

        public int ItemEquipment(int inputNum, Player player, ItemManager itemManager)
        {
            if (inputNum == 0) return -1;
            else
            {
                // EquipItem(inputNum, player);
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
        public void EquipItem(int inputNum, Player player)
        {
            Item seclectedItem = player.InventoryItems[inputNum - 1].isArmor ?
                                        player.ArmorEquipment[0] : player.WeaponEquipment[0];
            if (seclectedItem == null)
            {
                player.InventoryItems[inputNum - 1].isPlayerEquip = true;
                seclectedItem = player.InventoryItems[inputNum - 1];
            }
            else
            {

                if (seclectedItem.eItem == player.InventoryItems[inputNum - 1].eItem)
                {
                    seclectedItem = null;
                    player.InventoryItems[inputNum - 1].isPlayerEquip = false;
                }
                else
                {
                    for (int i = 0; i < player.InventoryItems.Count; i++)
                    {
                        if (seclectedItem.eItem == player.InventoryItems[i].eItem)
                        {
                            player.InventoryItems[i].isPlayerEquip = false;
                            player.InventoryItems[inputNum - 1].isPlayerEquip = true;
                            seclectedItem = player.InventoryItems[inputNum - 1];
                        }
                    }
                }
            }
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
                    player.InventoryItems[inputNum - 1].isPlayerEquip = false;
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
                    player.InventoryItems[inputNum - 1].isPlayerEquip = false;
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
