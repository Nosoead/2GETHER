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
        public Player player = new Player();
        public ItemManager itemManager = new ItemManager();

        Quest quest;
        Intro intro = new Intro();
        Status status = new Status();
        Monster monster = new Monster();
        Dungeon dungeon = new Dungeon();
        IOManager ioManager = new IOManager();
        Store store = new Store();
        Inventory inventory = new Inventory();
        DataManager dataManager;

        public GameManager ()
        {
            dataManager = new DataManager(this);
            quest = new Quest(itemManager, player);
        }

        public void GameStart()
        {
            ioManager.PrintMessage(ioManager.GameStartSceneMessage);
            ioManager.PlzInputAnyKey();
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

                case 5:
                    Quest();
                    break;

                case 6:
                    //저장하기 or 불러오기
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
    }
}
