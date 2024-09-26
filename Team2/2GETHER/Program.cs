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

    public class GameManager
    {
        IOManager ioManager = new IOManager();

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

                //임시 테스트용
                case 5:
                    ioManager.PrintDebugMessage();
                    break;
            }

            return;
        }

        public void Status()
        {

        }

        public void Inventory()
        {

        }

        public void Store()
        {

        }

        public void Dungeon()
        {

        }
    }
}
