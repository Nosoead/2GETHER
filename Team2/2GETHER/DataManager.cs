using System.Data.SqlTypes;
using System.Text.Json;

namespace _2GETHER
{
    class DataManager
    {
        GameManager currentGameManager;
        ItemManager? currentItemManager;
        Player? currentPlayer;

        public DataManager(GameManager gameManager)
        {
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
            saveData.EquippedWeapon = currentPlayer.EquippedWeapon;
            saveData.EquippedArmor = currentPlayer.EquippedArmor;
            saveData.Potions = currentPlayer.Potions;
            saveData.WeaponEquipment = currentPlayer.WeaponEquipment[0]?.eItem.ToString();
            saveData.ArmorEquipment = currentPlayer.ArmorEquipment[0]?.eItem.ToString();

            List<string> soldItems = new List<string>();
            foreach (Item item in currentItemManager?.items)
            {
                if (!item.isPlayerBuy)
                {
                    soldItems.Add(item.eItem.ToString());
                }
            }
            saveData.SoldItems = soldItems;

            List<string> inventoryItems = new List<string>();
            foreach (var item in currentPlayer.InventoryItems)
            {
                inventoryItems.Add(item.eItem.ToString());
            }
            saveData.InventoryItems = inventoryItems;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string jsonData = JsonSerializer.Serialize(saveData, options);
            File.WriteAllText("savegame.json", jsonData);
        }

        
    }

    

    struct Data
    {
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
