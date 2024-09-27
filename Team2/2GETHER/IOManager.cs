namespace _2GETHER
{
    public class IOManager
    {
        public string inputString = "";

        public string[] GameStartSceneMessage =
        {
            "스파르타 던전에 오신 여러분 환영합니다.",
            "이제 전투를 시작할 수 있습니다."
        };

        public string[] MenuSceneMessage =
        {
            "상태 보기",
            "인벤토리",
            "상점",
            "던전 입장",
            "임시 테스트"
        };

        public void PrintDebugMessage()
        {
            Console.WriteLine("디버그용 출력 메세지입니다.");
            Console.ReadKey();
        }

        public void PlzInputAnyKey()
        {
            Console.WriteLine("\n아무 키나 입력해주세요");
            Console.ReadKey();
        }

        public void PrintMessage(string message, bool Clear = false)
        {
            if (Clear)
            {
                Console.Clear();
            }

            Console.WriteLine(message);
        }

        public void PrintMessage(string[] messages, bool Clear = false)
        {
            if (Clear)
            {
                Console.Clear();
            }

            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }

        public int PrintMessageWithNumberNoSelect(string[] messages, bool Clear = false)
        {
            if (Clear)
            {
                Console.Clear();
            }

            while (true)
            {
                for (int i = 0; i < messages.Length; i++)
                {
                    string printMessage = string.Format("{0}. {1}", i + 1, messages[i]);
                    Console.WriteLine(printMessage);
                }
            }
        }

        public int PrintMessageWithNumberForSelect(string[] messages, bool Clear = true)
        {
            int selectNumber = 0;

            if (Clear)
            {
                Console.Clear();
            }

            while (true)
            {
                for (int i = 0; i < messages.Length; i++)
                {
                    string printMessage = string.Format("{0}. {1}", i + 1, messages[i]);
                    Console.WriteLine(printMessage);
                }

                Console.WriteLine("\n원하는 행동을 입력해주세요 : \n");

                try
                {
                    selectNumber = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    //Console.Write(ex.ToString());
                    Console.WriteLine("잘못된 입력입니다\n");
                }

                if (!(0 < selectNumber && selectNumber <= messages.Length))
                {
                    selectNumber = -1;
                    Console.WriteLine("\n잘못된 숫자 입력입니다\n");
                }
                else
                {
                    break;
                }
            }

            return selectNumber;
        }
    }
}
