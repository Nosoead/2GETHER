using System.Drawing;

namespace _2GETHER
{
    class IOManager
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
            "퀘스트",
            "저장/불러오기"
        };

        public Dictionary<string, ConsoleColor> MessageColor = new Dictionary<string, ConsoleColor>();

        public void InitializeMessageColors(Player player)
        {
            switch(player.Job)
            {
                case EJob.전사:
                    MessageColor["전사"] = ConsoleColor.Green;
                    MessageColor["궁수"] = ConsoleColor.DarkRed;
                    MessageColor["마법사"] = ConsoleColor.DarkRed;
                    break;

                case EJob.궁수:
                    MessageColor["전사"] = ConsoleColor.DarkRed;
                    MessageColor["궁수"] = ConsoleColor.Green;
                    MessageColor["마법사"] = ConsoleColor.DarkRed;
                    break;

                case EJob.마법사:
                    MessageColor["전사"] = ConsoleColor.DarkRed;
                    MessageColor["궁수"] = ConsoleColor.DarkRed;
                    MessageColor["마법사"] = ConsoleColor.Green;
                    break;
            }

            MessageColor["상태 보기"] = ConsoleColor.DarkYellow;
            MessageColor["Battle"] = ConsoleColor.Red;
            MessageColor["Dead"] = ConsoleColor.DarkGray;
        }

        public void PrintDebugMessage(string message = "디버그용 출력 메세지입니다.", bool applyColor = false)
        {
            if (applyColor)
            {
                PrintWithColor(message);
            }
            else
            {
                Console.WriteLine(message);
            }
            Console.ReadKey();
        }

        public void PlzInputAnyKey()
        {
            Console.WriteLine("\n아무 키나 입력해주세요");
            Console.ReadKey();
        }

        public void PrintConsoleLine()
        {
            Console.WriteLine();
        }

        public int PrintMessageAndSelectNum(string message, int maxNumber, bool applyColor = false)
        {
            int select = -1;

            while (true)
            {
                if (applyColor)
                {
                    PrintWithColor(message);
                }
                else
                {
                    Console.WriteLine(message);
                }

                try
                {
                    select = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    //Console.WriteLine("{0}", message);
                    continue;
                }

                if ((0 <= select && select <= maxNumber))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            return select;
        }

        public void PrintMessage(string message, bool Clear = false, bool applyColor = false)
        {
            if (Clear)
            {
                Console.Clear();
            }

            if (applyColor)
            {
                PrintWithColor(message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }

        public void PrintMessage(string[] messages, bool Clear = false, bool applyColor = false)
        {
            if (Clear)
            {
                Console.Clear();
            }

            foreach (var message in messages)
            {
                if (applyColor)
                {
                    PrintWithColor(message);
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
        }

        public void PrintMessageWithNumberNoSelect(string[] messages, bool Clear = false, bool applyColor = false)
        {
            if (Clear)
            {
                Console.Clear();
            }

            for (int i = 0; i < messages.Length; i++)
            {
                string printMessage = string.Format("{0}. {1}", i + 1, messages[i]);

                if (applyColor)
                    PrintWithColor(printMessage);
                else
                    Console.WriteLine(printMessage);
            }
        }

        public int PrintMessageWithNumberForSelect(string[] messages, bool Clear = true, bool applyColor = false)
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

                    if (applyColor)
                        PrintWithColor(printMessage);
                    else 
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
                    continue;
                }

                if (!(0 <= selectNumber && selectNumber <= messages.Length))
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

        public int PrintMessageWithNumberForSelectZeroExit(string[] messages, bool Clear = true, bool applyColor = false)
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

                    if (applyColor)
                        PrintWithColor(printMessage);
                    else
                        Console.WriteLine(printMessage);
                }

                Console.WriteLine("\n0. 취소/나가기");

                Console.WriteLine("\n원하는 행동을 입력해주세요 : \n");

                try
                {
                    selectNumber = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    //Console.Write(ex.ToString());
                    Console.WriteLine("잘못된 입력입니다\n");
                    continue;
                }

                if (!(0 <= selectNumber && selectNumber <= messages.Length))
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

        public int PrintMessageWithNumberForSelectZeroExit(string[] messages, string[] centerMessage ,bool Clear = true, bool applyColor = false)
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

                    if (applyColor)
                        PrintWithColor(printMessage);
                    else
                        Console.WriteLine(printMessage);

                    /*//죽은 몬스터일 경우
                    if (printMessage.Contains("Dead"))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    Console.WriteLine(printMessage);

                    Console.ResetColor();*/
                }

                PrintConsoleLine();
                PrintMessage(centerMessage);

                Console.WriteLine("\n0. 취소/나가기");

                Console.WriteLine("\n원하는 행동을 입력해주세요 : \n");

                try
                {
                    selectNumber = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    //Console.Write(ex.ToString());
                    Console.WriteLine("잘못된 입력입니다\n");
                    continue;
                }

                if (!(0 <= selectNumber && selectNumber <= messages.Length))
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

        
        public void PrintWithColor(string message)
        {
            ConsoleColor color = CheckMessageReturnColor(message);

            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ResetColor();
        }

        public ConsoleColor CheckMessageReturnColor(string message)
        {
            foreach (var key in MessageColor.Keys)
            {
                if (message.ToLower().Contains(key.ToLower()))
                {
                    return MessageColor[key];
                }
            }

            return ConsoleColor.White;
        }
    }
}
