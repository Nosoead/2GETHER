using System.Diagnostics;

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
        public EItem eItem { get; protected set; }
        public EItemType eItemType { get; protected set; }
        public int Price { get; protected set; }
        public int ItemATK { get; protected set; }
        public int ItemDEF { get; protected set; }
        public int ItemHeal { get; protected set; }
        public int ItemCount { get; protected set; }
        protected string? ItemDescription;

        public void SetCount(int count)
        {
            ItemCount = count;
        }
        public void AddCount()
        {
            ItemCount++;
        }
        public void RemoveCount()
        {
            if (ItemCount == 0) return;
            ItemCount--;
        }
        public virtual string GetItemInfo(EItemDo eItemDo)
        {
            string itemInfo = "";

            return itemInfo;
        }
    }

    class EquipmentItem : Item
    {

        public EJob eJob { get; private set; }
        protected bool IsArmor;
        public bool IsPlayerEquip { get; set; }
        public EquipmentItem(EItem eItem, EItemType eItemType, EJob eJob, int Price, int ItemATK, int ItemDEF, int ItemHeal, int ItemCount, bool IsArmor, bool IsPlayerEquip, string ItemDescription)
        {
            this.eItem = eItem;
            this.eItemType = eItemType;
            this.eJob = eJob;
            this.Price = Price;
            this.ItemATK = ItemATK;
            this.ItemDEF = ItemDEF;
            this.ItemHeal = ItemHeal;
            this.ItemCount = ItemCount;
            this.IsArmor = IsArmor;
            this.IsPlayerEquip = IsPlayerEquip;
            this.ItemDescription = ItemDescription;
        }

        public override string GetItemInfo(EItemDo eItemDo)
        {
            string iteminfo = "";

            switch (eItemDo)
            {
                case EItemDo.ItemBuy:
                    iteminfo =
                          (IsPlayerEquip ? "[E] " : "")
                        + ($"{eItem}")
                        + (ItemATK != 0 ? $"| 공격력 + {ItemATK}" : "")
                        + (ItemDEF != 0 ? $"| 방어력 + {ItemDEF}" : "")
                        + (ItemHeal != 0 ? $"| 회복력 + {ItemHeal}" : "")
                        + $"| {eJob}"
                        + $"| {ItemDescription}"
                        + $"| {Price} G";
                    break;
                case EItemDo.ItemSell:
                    iteminfo =
                          (IsPlayerEquip ? "[E] " : "")
                        + ($"{eItem}")
                        + (ItemATK != 0 ? $"| 공격력 + {ItemATK}" : "")
                        + (ItemDEF != 0 ? $"| 방어력 + {ItemDEF}" : "")
                        + (ItemHeal != 0 ? $"| 회복력 + {ItemHeal}" : "")
                        + $"| {eJob}"
                        + $"| {ItemDescription}"
                        + $"| {Price * 85 / 100} G"
                        + $"| x {ItemCount}";
                    break;
                case EItemDo.ItemView:
                    iteminfo =
                          (IsPlayerEquip ? "[E] " : "")
                        + ($"{eItem}")
                        + (ItemATK != 0 ? $"| 공격력 + {ItemATK}" : "")
                        + (ItemDEF != 0 ? $"| 방어력 + {ItemDEF}" : "")
                        + (ItemHeal != 0 ? $"| 회복력 + {ItemHeal}" : "")
                        + $"| {eJob}"
                        + $"| {ItemDescription}"
                        + $"| x {ItemCount}";
                    break;
            }

            return iteminfo;
        }
    }

    class ConsumableItem : Item
    {
        public ConsumableItem(EItem eItem, EItemType eItemType, int Price, int ItemATK, int ItemDEF, int ItemHeal, int ItemCount, string ItemDescription)
        {
            this.eItem = eItem;
            this.eItemType = eItemType;
            this.Price = Price;
            this.ItemATK = ItemATK;
            this.ItemDEF = ItemDEF;
            this.ItemHeal = ItemHeal;
            this.ItemCount = ItemCount;
            this.ItemDescription = ItemDescription;
        }

        public override string GetItemInfo(EItemDo eItemDo)
        {
            string iteminfo = "";

            switch (eItemDo)
            {
                case EItemDo.ItemBuy:
                    iteminfo =
                        ($"{eItem}")
                        + (ItemATK != 0 ? $"| 공격력 + {ItemATK}" : "")
                        + (ItemDEF != 0 ? $"| 방어력 + {ItemDEF}" : "")
                        + (ItemHeal != 0 ? $"| 회복력 + {ItemHeal}" : "")
                        + $"| {ItemDescription}"
                        + $"| {Price} G";
                    break;
                case EItemDo.ItemSell:
                    iteminfo =
                        ($"{eItem}")
                        + (ItemATK != 0 ? $"| 공격력 + {ItemATK}" : "")
                        + (ItemDEF != 0 ? $"| 방어력 + {ItemDEF}" : "")
                        + (ItemHeal != 0 ? $"| 회복력 + {ItemHeal}" : "")
                        + $"| {ItemDescription}"
                        + $"| {Price * 85 / 100} G"
                        + $"| x {ItemCount}";
                    break;
                case EItemDo.ItemView:
                    iteminfo =
                        ($"{eItem}")
                        + (ItemATK != 0 ? $"| 공격력 + {ItemATK}" : "")
                        + (ItemDEF != 0 ? $"| 방어력 + {ItemDEF}" : "")
                        + (ItemHeal != 0 ? $"| 회복력 + {ItemHeal}" : "")
                        + $"| {ItemDescription}"
                        + $"| x {ItemCount}";
                    break;
            }

            return iteminfo;
        }
    }
}
