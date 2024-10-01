using System.Data.SqlTypes;
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
        List <string> FileLoadingMessage = new List<string>();

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
            //saveData.EquippedWeapon = currentPlayer.WeaponEquipment[0];
            //saveData.EquippedArmor = currentPlayer.EquippedArmor;
            saveData.Potions = currentPlayer.Potions;
            saveData.MonsterKills = currentPlayer.MonsterKills;
            saveData.WeaponEquipment = currentPlayer.WeaponEquipment[0]?.eItem.ToString();
            saveData.ArmorEquipment = currentPlayer.ArmorEquipment[0]?.eItem.ToString();

            List<string> soldItems = new List<string>();
            foreach (Item item in currentItemManager?.items)
            {
                /*if (item.isPlayerBuy)
                {
                    soldItems.Add(item.eItem.ToString());
                }*/
            }
            saveData.SoldItems = soldItems;

            List<string> inventoryItems = new List<string>();
            /*foreach (var item in currentPlayer.InventoryItems)
            {
                inventoryItems.Add(item.eItem.ToString());
            }*/
            saveData.InventoryItems = inventoryItems;

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

            ioManager.PrintDebugMessage("저장 완료");
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

            /*// 플레이어의 데이터를 불러와 게임에 반영
            currentPlayer.Name = loadedData.Name;
            currentPlayer.Level = loadedData.Level;
            currentPlayer.Attack = loadedData.Attack;
            currentPlayer.Defense = loadedData.Defense;
            currentPlayer.Hp = loadedData.Hp;
            currentPlayer.MaxHp = loadedData.MaxHp;
            currentPlayer.Mp = loadedData.Mp;
            currentPlayer.MaxMp = loadedData.MaxMp;
            currentPlayer.Gold = loadedData.Gold;
            currentPlayer.Exp = loadedData.Exp;
            currentPlayer.MaxExp = loadedData.MaxExp;
            currentPlayer.Job = Enum.Parse<EJob>(loadedData.Job); // 직업을 다시 enum 타입으로 변환
            currentPlayer.Potions = loadedData.Potions;
            currentPlayer.MonsterKills = loadedData.MonsterKills;*/

            EJob job = new EJob();
            job = Enum.Parse<EJob>(loadedData.Job);

            currentPlayer.SetPlayerData(loadedData.Name, loadedData.Level, loadedData.Attack, loadedData.Defense,
                loadedData.Hp, loadedData.MaxHp, loadedData.Mp, loadedData.MaxMp, loadedData.Gold, loadedData.Exp,
                loadedData.MaxExp, job, loadedData.Potions, loadedData.MonsterKills);

            //var butItems = currentItemManager.items.Where
                //(item => loadedData.SoldItems.Contains(item.eItem.ToString())).ToList();
            
            /*foreach (var item in currentItemManager.items)
            {
                if (loadedData.SoldItems.Contains (item.eItem.ToString()))
                {
                    item.isPlayerBuy = true;
                }
            }*/



            //currentItemManager.items
        }

        public void LoadingData()
        {
            int number = 0;
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

            fileSelect = ioManager.PrintMessageWithNumberForSelectZeroExit(FileLoadingMessage.ToArray(), true);

            /*if (fileSelect == 0)
            {
                return;
            }*/
            /*if (!File.Exists(saveFileName))
            {
                ioManager.PrintDebugMessage("저장된 게임 데이터가 없습니다.");
                return;
            }*/
        }

        public void SaveOrLoad()
        {
            LoadingData();

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
        public string DataSaveTime {  get; set; }

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

        public List<string> InventoryItems { get; set; }

        public List<string> SoldItems { get; set; }
        /*public List<string> inventoryItems = new List<string>();
        public List<string> InventoryItems
        {
            get
            {
                return inventoryItems;
            }
            set
            {
                inventoryItems = value;
            }
        }*/

    }
}
