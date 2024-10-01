namespace _2GETHER
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();

            gameManager.GameStart();
        }
    }

    class GameManager
    {
        Quest quest;
        DataManager dataManager;

        Store store = new Store();
        Intro intro = new Intro();
        Status status = new Status();
        Monster monster = new Monster();
        Dungeon dungeon = new Dungeon();
        IOManager ioManager = new IOManager();
        Inventory inventory = new Inventory();

        public Player player = new Player();
        public ItemManager itemManager = new ItemManager();

        public GameManager ()
        {
            dataManager = new DataManager(this, ioManager);
            quest = new Quest(itemManager, player);
            ioManager.InitializeMessageColors();
        }

        public void GameStart()
        {
            Intro();
            Game();
        }

        public void Game()
        {
            int select;

            while (true)
            {
                select = ioManager.PrintMessageWithNumberForSelect(ioManager.MenuSceneMessage);

                Action(select);
            }
        }

        public void Action(int select)
        {
            switch (select)
            {
                //상태보기
                case 1:
                    Status();
                    break;

                //인벤토리
                case 2:
                    Inventory();
                    break;

                //상점
                case 3:
                    Store();
                    break;

                //던전 입장
                case 4:
                    Dungeon();
                    break;

                //퀘스트
                case 5:
                    Quest();
                    break;

                //저장 불러오기 제작중
                case 6:
                    SaveLoad();
                    break;

                case 7:
                    //테스트
                    break;
            }

            return;
        }
        public void Intro()
        {
            intro.SetPlayerName(player, ioManager);
            intro.SetPlayerJob(player, ioManager);
        }

        public void Status()
        {
            status.GetStatusInfo(player, ioManager);
        }

        public void Inventory()
        {
            inventory.EnterInventory(player, ioManager, itemManager);
        }

        public void Store()
        {
            store.EnterStore(player, ioManager, itemManager);
        }

        public void Dungeon()
        {
            dungeon.StartBattle(player, monster, ioManager);
        }

        public void Quest()
        {
            quest.QuestList(ioManager);
        }

        public void SaveLoad()
        {
            dataManager.SaveOrLoad();
        }
    }
}
