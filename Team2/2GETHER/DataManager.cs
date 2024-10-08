﻿using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Data;

namespace _2GETHER
{
    using System;
    class DataManager
    {
        readonly int FileMaxCount = 5;
        string saveFileName = "savegame.json";
        List<string> FileLoadingMessage = new List<string>();

        IOManager ioManager;
        Player? currentPlayer;
        ItemManager? currentItemManager;
        GameManager currentGameManager;

        Data[] SavedData = new Data[5];

        string[] SaveOrLoadMessage =
        {
            "저장",
            "불러오기"
        };

        public DataManager(GameManager gameManager, IOManager ioManager)
        {
            this.ioManager = ioManager;
            currentGameManager = gameManager;
            currentItemManager = gameManager.itemManager;
            currentPlayer = gameManager.player;
        }

        public void SaveData()
        {
            Data saveData = new Data();

            saveData.Name = currentPlayer.Name;
            saveData.Level = currentPlayer.Level;
            saveData.Attack = currentPlayer.Attack;
            saveData.Defense = currentPlayer.Defense;
            saveData.Hp = currentPlayer.Hp;
            saveData.MaxHp = currentPlayer.MaxHp;
            saveData.Mp = currentPlayer.Mp;
            saveData.MaxMp = currentPlayer.MaxMp;
            saveData.Gold = currentPlayer.Gold;
            saveData.Exp = currentPlayer.Exp;
            saveData.MaxExp = currentPlayer.MaxExp;
            saveData.Job = currentPlayer.Job.ToString();
            saveData.Potions = currentPlayer.Potions;
            saveData.MonsterKills = currentPlayer.MonsterKills;
            saveData.WeaponEquipment = currentPlayer.weaponEquipment[0]?.eItem.ToString();
            saveData.ArmorEquipment = currentPlayer.armorEquipment[0]?.eItem.ToString();

            saveData.EquipmentInventory = new Dictionary<string, int>();
            saveData.ConsumableInventory = new Dictionary<string, int>();

            foreach (var item in currentPlayer.equipmentInventory)
            {
                saveData.EquipmentInventory.Add(item.eItem.ToString(), item.ItemCount);
            }

            foreach (var item in currentPlayer.consumableInventory)
            {
                saveData.ConsumableInventory.Add(item.eItem.ToString(), item.ItemCount);
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            DateTime now = DateTime.Now;
            string saveMessage = $" 마지막 저장 시점 : {now.ToString("f")}";
            saveData.DataSaveTime = saveMessage;

            string jsonData = JsonSerializer.Serialize(saveData, options);
            File.WriteAllText(saveFileName, jsonData);

            ioManager.PrintDebugMessage("저장 완료", true);
        }

        public void LoadData()
        {
            if (!File.Exists(saveFileName))
            {
                ioManager.PrintDebugMessage("저장된 게임 데이터가 없습니다.");
                return;
            }

            string jsonData = File.ReadAllText(saveFileName);
            Data loadedData = JsonSerializer.Deserialize<Data>(jsonData);

            EJob job = new EJob();
            job = Enum.Parse<EJob>(loadedData.Job);

            currentPlayer.SetPlayerData(loadedData.Name, loadedData.Level, loadedData.Attack, loadedData.Defense,
                loadedData.Hp, loadedData.MaxHp, loadedData.Mp, loadedData.MaxMp, loadedData.Gold, loadedData.Exp,
                loadedData.MaxExp, job, loadedData.Potions, loadedData.MonsterKills);

            currentPlayer.equipmentInventory.Clear();
            currentPlayer.consumableInventory.Clear();

            foreach (var loadItemName in loadedData.EquipmentInventory.Keys)
            {
                var existingItem = currentPlayer.equipmentInventory
                    .FirstOrDefault(x => x.eItem.ToString() == loadItemName);

                if (existingItem == null)
                {
                    var item = currentItemManager.equipmentItemList.Find(x => x.eItem.ToString() == loadItemName);
                    currentPlayer.equipmentInventory.Add(item);

                    item.SetCount(loadedData.EquipmentInventory[loadItemName]);
                }
            }

            bool isWeaponSet = false;
            bool isArmorSet = false;

            var armorItems = currentPlayer.equipmentInventory.Where(x => x.eItem.ToString() == loadedData.ArmorEquipment).ToList();
            var weaponItems = currentPlayer.equipmentInventory.Where(x => x.eItem.ToString() == loadedData.WeaponEquipment).ToList();

            foreach (var equipItem in weaponItems)
            {
                equipItem.IsPlayerEquip = true;
                if (!isWeaponSet && equipItem.eItemType == EItemType.Weapon)
                {
                    currentPlayer.weaponEquipment[0] = equipItem;
                    isWeaponSet = true;
                }
            }

            foreach (var equipItem in armorItems)
            {
                equipItem.IsPlayerEquip = true;
                if (!isArmorSet && equipItem.eItemType == EItemType.Armor)
                {
                    currentPlayer.armorEquipment[0] = equipItem;
                    isArmorSet = true;
                }
            }

            foreach (var loadItemName in loadedData.ConsumableInventory.Keys)
            {
                var item = currentItemManager.consumableItemList.Find(x => x.eItem.ToString() == loadItemName);
                currentPlayer.consumableInventory.Add(item);

                item.SetCount(loadedData.ConsumableInventory[loadItemName]);
            }

            ioManager.PrintDebugMessage("불러오기 완료", true);
        }

        //선택창 구현
        public void LoadingData()
        {
            /*int number = 0;
            int fileSelect = -1;

            for (number = 0; number < FileMaxCount; number++)
            {
                string FileName = $"myJson{number + 1}.json";

                if (!File.Exists(FileName))
                {
                    FileLoadingMessage.Add("빈 슬롯");
                    continue;
                }
                else
                {
                    //string 
                }
            }

            fileSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(FileLoadingMessage.ToArray(), true);*/

            /*if (!File.Exists(saveFileName))
            {
                ioManager.PrintDebugMessage("저장된 게임 데이터가 없습니다.");
                return;
            }*/


        }

        public void SaveOrLoad()
        {
            //LoadingData();

            int select = -1;
            while (true)
            {
                select = ioManager.PrintMessageWithNumberForSelectZeroExit(SaveOrLoadMessage, true);

                switch (select)
                {
                    //저장
                    case 1:
                        SaveData();
                        break;

                    //불러오기
                    case 2:
                        LoadData();
                        break;

                    //나가기
                    case 0:
                        return;

                    //잘못입력
                    default:
                        break;
                }
            }
        }
    }



    struct Data
    {
        public string DataSaveTime { get; set; }

        public string Name { get; set; }
        public int Level { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public double Hp { get; set; }
        public double MaxHp { get; set; }
        public double Mp { get; set; }
        public double MaxMp { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public int MaxExp { get; set; }
        public string Job { get; set; }
        public bool EquippedWeapon { get; set; }
        public bool EquippedArmor { get; set; }
        public int Potions { get; set; }
        public int MonsterKills { get; set; }
        public string WeaponEquipment { get; set; }
        public string ArmorEquipment { get; set; }

        public Dictionary<string, int> EquipmentInventory { get; set; }
        public Dictionary<string, int> ConsumableInventory { get; set; }

    }
}
