namespace _2GETHER
{
    class ItemManager
    {
        public List<Item> items = new List<Item>();
        //public List<Item> jobItems = new List<Item>();
        public List<EquipmentItem> equipmentItemList = new List<EquipmentItem>();
        public List<ConsumableItem> consumableItemList = new List<ConsumableItem>();
        public ItemManager()
        {
            #region // 공용아이템 생성 리스트
            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)1,
                eItemType: EItemType.Armor,
                eJob: EJob.공용,
                Price: 1000,
                ItemATK: 0,
                ItemDEF: 5,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "수련에 도움을 주는 갑옷입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)2,
                eItemType: EItemType.Armor,
                eJob: EJob.공용,
                Price: 1750,
                ItemATK: 0,
                ItemDEF: 9,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "무쇠로 만들어져 튼튼한 갑옷입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)3,
                eItemType: EItemType.Armor,
                eJob: EJob.공용,
                Price: 3500,
                ItemATK: 0,
                ItemDEF: 15,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)4,
                eItemType: EItemType.Weapon,
                eJob: EJob.공용,
                Price: 600,
                ItemATK: 2,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "쉽게 볼 수 있는 낡은 검 입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)5,
                eItemType: EItemType.Weapon,
                eJob: EJob.공용,
                Price: 1500,
                ItemATK: 5,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "어디선가 사용됐던거 같은 도끼입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)6,
                eItemType: EItemType.Weapon,
                eJob: EJob.공용,
                Price: 2400,
                ItemATK: 7,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "스파르타의 전사들이 사용했다는 전설의 창입니다."
             ));
            #endregion
            #region // 전사아이템 생성 리스트
            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)101,
                eItemType: EItemType.Weapon,
                eJob: EJob.전사,
                Price: 600,
                ItemATK: 2,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "쉽게 볼 수 있는 낡은 검입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)102,
                eItemType: EItemType.Weapon,
                eJob: EJob.전사,
                Price: 1000,
                ItemATK: 5,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "강철로 만든 검입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)103,
                eItemType: EItemType.Weapon,
                eJob: EJob.전사,
                Price: 1500,
                ItemATK: 10,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "어디선가 사용됐던 도끼입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)104,
                eItemType: EItemType.Weapon,
                eJob: EJob.전사,
                Price: 4000,
                ItemATK: 20,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "전설적인 영웅 아서가 썼다고 전해지는 검입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)105,
                eItemType: EItemType.Armor,
                eJob: EJob.전사,
                Price: 1000,
                ItemATK: 0,
                ItemDEF: 5,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "수련에 도움을 주는 갑옷입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)106,
                eItemType: EItemType.Armor,
                eJob: EJob.전사,
                Price: 2000,
                ItemATK: 0,
                ItemDEF: 9,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "무쇠로 만들어져 튼튼한 갑옷입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)107,
                eItemType: EItemType.Armor,
                eJob: EJob.전사,
                Price: 3000,
                ItemATK: 0,
                ItemDEF: 15,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "스파르타의 전사들이 사용한 전설의 갑옷입니다."
             ));
            #endregion
            #region // 궁수아이템 생성 리스트
            equipmentItemList.Add(new EquipmentItem(
               eItem: (EItem)201,
               eItemType: EItemType.Weapon,
               eJob: EJob.궁수,
               Price: 600,
               ItemATK: 2,
               ItemDEF: 0,
               ItemHeal: 0,
               ItemCount: 0,
               IsArmor: false,
               IsPlayerEquip: false,
               ItemDescription: "쉽게 볼 수 있는 낡은 석궁입니다."
            ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)202,
                eItemType: EItemType.Weapon,
                eJob: EJob.궁수,
                Price: 1000,
                ItemATK: 5,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "강철로 만든 활입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)203,
                eItemType: EItemType.Weapon,
                eJob: EJob.궁수,
                Price: 1500,
                ItemATK: 10,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "강력한 다이아몬드 활입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)204,
                eItemType: EItemType.Weapon,
                eJob: EJob.궁수,
                Price: 4000,
                ItemATK: 20,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "고대 엘프 족장 아스트라가 썼다고 전해지는 활입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)205,
                eItemType: EItemType.Armor,
                eJob: EJob.궁수,
                Price: 1000,
                ItemATK: 0,
                ItemDEF: 5,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "수련에 도움을 주는 가죽갑옷입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)206,
                eItemType: EItemType.Armor,
                eJob: EJob.궁수,
                Price: 2000,
                ItemATK: 0,
                ItemDEF: 9,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "무쇠로 만들어져 튼튼한 가죽갑옷입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)207,
                eItemType: EItemType.Armor,
                eJob: EJob.궁수,
                Price: 3000,
                ItemATK: 0,
                ItemDEF: 15,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "스파르타의 궁수들이 사용한 전설의 가죽갑옷입니다."
             ));
            #endregion
            #region // 마법사아이템 생성 리스트
            equipmentItemList.Add(new EquipmentItem(
               eItem: (EItem)301,
               eItemType: EItemType.Weapon,
               eJob: EJob.마법사,
               Price: 600,
               ItemATK: 2,
               ItemDEF: 0,
               ItemHeal: 0,
               ItemCount: 0,
               IsArmor: false,
               IsPlayerEquip: false,
               ItemDescription: "쉽게 볼 수 있는 낡은 지팡이입니다."
            ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)302,
                eItemType: EItemType.Weapon,
                eJob: EJob.마법사,
                Price: 1000,
                ItemATK: 5,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "강철로 만든 지팡이입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)303,
                eItemType: EItemType.Weapon,
                eJob: EJob.마법사,
                Price: 1500,
                ItemATK: 10,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "어디선가 사용됐던 마법 지팡이입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)304,
                eItemType: EItemType.Weapon,
                eJob: EJob.마법사,
                Price: 4000,
                ItemATK: 20,
                ItemDEF: 0,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: false,
                IsPlayerEquip: false,
                ItemDescription: "9서클 대마법사 랑그란트가 사용한 지팡이입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)305,
                eItemType: EItemType.Armor,
                eJob: EJob.마법사,
                Price: 1000,
                ItemATK: 0,
                ItemDEF: 5,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "수련에 도움을 주는 로브입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)306,
                eItemType: EItemType.Armor,
                eJob: EJob.마법사,
                Price: 2000,
                ItemATK: 0,
                ItemDEF: 9,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "무쇠로 만들어져 튼튼한 로브입니다."
             ));

            equipmentItemList.Add(new EquipmentItem(
                eItem: (EItem)307,
                eItemType: EItemType.Armor,
                eJob: EJob.마법사,
                Price: 3000,
                ItemATK: 0,
                ItemDEF: 15,
                ItemHeal: 0,
                ItemCount: 0,
                IsArmor: true,
                IsPlayerEquip: false,
                ItemDescription: "스파르타의 전사들이 사용한 전설의 로브입니다."
             ));
            #endregion

            #region // 소비아이템 생성 리스트
            consumableItemList.Add(new ConsumableItem(
                eItem: (EItem)7,
                eItemType: EItemType.Potion,
                Price: 500,
                ItemATK: 0,
                ItemDEF: 0,
                ItemHeal: 30,
                ItemCount: 0,
                ItemDescription: "마시면 기분이 좋아지는 약입니다."
            ));
            #endregion
        }
        public List<EquipmentItem> GetItemsByJob(EJob eJob)
        {
            return equipmentItemList.Where(equipmentItem => equipmentItem.eJob == eJob).ToList();
        }
    }
}
