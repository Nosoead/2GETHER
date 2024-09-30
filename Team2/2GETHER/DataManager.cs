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

            string jsonData = JsonSerializer.Serialize(saveData);
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
        public int Gold { get; private set; }
        public int Exp { get; private set; }
        public int MaxExp { get; private set; }
        public EJob Job { get; private set; }
        public bool EquippedWeapon { get; set; }
        public bool EquippedArmor { get; set; }
        public int Potions { get; set; }
    }
}
