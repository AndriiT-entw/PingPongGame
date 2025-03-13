
namespace pingPongV2
{
    class Program
    {

        static int gameHeight = 20;
        static int gameWidth = 80;
        static int gameYStart = 4;
        static int leftPaddleY, rightPaddleY;
        static int leftPaddleX = 1;
        static int rightPaddleX;
        static int ballX, ballY;
        static string gameName = "Ping pong";
        static int positionOfName;
        static int heightOfPlates = 4;
        static int scoreL = 0, scoreR = 0;
        static int positionOfCount;
        static string countDisplay;
        static bool isRunning = true;
        static char ball = 'O';
        static bool ballGoingRight, ballGoingDown;
        static int counter = 0;

        static void Main(string[] args)
        {

            InitializeGame();

            Task.Run(() => Insert());
            Task.Run(() => UpdateGame());

            while (isRunning)
            {
                drawScreen();
                Thread.Sleep(10);

                if (scoreL >= 10)
                {
                    Console.Clear();
                    Console.WriteLine("Game over");
                    Console.WriteLine("Left player won");
                    break;
                }

                if (scoreR >= 10)
                {
                    Console.Clear();
                    Console.WriteLine("Game over");
                    Console.WriteLine("Right player won");
                    break;
                }

            }
        }

        static void InitializeGame()
        {
            Random rnd = new Random();
            leftPaddleY = rightPaddleY = gameHeight / 2;
            ballX = gameWidth / 2;
            ballY = gameHeight / 2;
            positionOfName = gameWidth / 2 - gameName.Length / 2;
            rightPaddleX = gameWidth;
            

            if(rnd.Next(0, 2) == 1)
            {
                ballGoingRight = true;
            }
            else
            {
                ballGoingRight = false;
            }

            if(rnd.Next(0, 2) == 1)
            {
                ballGoingDown = true;
            }
            else
            {
                ballGoingDown = false;
            }
        }

        static void drawScreen()
        {
            countDisplay = $"{scoreL} | {scoreR}";
            positionOfCount = gameWidth / 2 - countDisplay.Length / 2;

            if (counter % 10 == 0)
            {
                Console.Clear(); 
            }
            counter++;
            for(int i = gameYStart; i <= gameHeight + gameYStart; i++)
            {
                Console.SetCursorPosition(leftPaddleX, i);
                Console.WriteLine(" ");
               

                Console.SetCursorPosition(rightPaddleX, i);
                Console.WriteLine(" ");
               
            }
           


            Console.SetCursorPosition(positionOfName, 1);
            Console.WriteLine("Ping pong");

            Console.SetCursorPosition(ballX, ballY);
            Console.WriteLine(ball);
            
            for(int i = 1; i < gameWidth; i++)
            {
                Console.SetCursorPosition(i, gameYStart);
                Console.WriteLine("_");

                Console.SetCursorPosition(i, gameHeight + gameYStart);
                Console.WriteLine("_");
            }

            for(int i = 0; i < heightOfPlates; i++)
            {
                Console.SetCursorPosition(leftPaddleX, leftPaddleY++);
                Console.WriteLine("|");

                Console.SetCursorPosition(rightPaddleX, rightPaddleY++);
                Console.WriteLine("|");


                if(i >= heightOfPlates - 1)
                {
                    leftPaddleY = leftPaddleY - heightOfPlates;
                    rightPaddleY = rightPaddleY - heightOfPlates;
                }
            }

            Console.SetCursorPosition(positionOfCount, 3);
            Console.WriteLine(countDisplay);

            
        }

        static void Insert()
        {
            while (isRunning)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    if(key == ConsoleKey.W && leftPaddleY >= gameYStart + 2)
                    {
                        leftPaddleY--;
                    }
                    else if(key == ConsoleKey.S && leftPaddleY <= gameHeight + gameYStart - heightOfPlates)
                    {
                        leftPaddleY++;
                    }

                    if(key == ConsoleKey.E && rightPaddleY >= gameYStart + 2)
                    {
                        rightPaddleY--;
                    }
                    else if(key == ConsoleKey.D && rightPaddleY <= gameHeight + gameYStart - heightOfPlates)
                    {
                        rightPaddleY++;
                    }
                }
                Thread.Sleep(10);
            }

        }

        static void UpdateGame()
        {
            while(isRunning)
            {
                if (ballGoingRight == true)
                {
                    ballX++;
                }
                else if(ballGoingRight == false)
                {
                    ballX--;
                }

                if(ballGoingDown == true)
                {
                    ballY++;
                }
                else if(ballGoingDown == false)
                {
                    ballY--;
                }

                if (ballY <= gameYStart + 1 || ballY >= gameHeight + gameYStart)
                {
                    ballGoingDown = !ballGoingDown;
                }

                if (ballX <= leftPaddleX && ballY >= leftPaddleY && ballY <= leftPaddleY + heightOfPlates)
                {
                    ballGoingRight = !ballGoingRight;
                }
                else if (ballX < leftPaddleX)
                {
                    ResetBall();
                    scoreR++;
                   
                }

                if(ballX >= rightPaddleX && ballY >= rightPaddleY && ballY <= rightPaddleY + heightOfPlates)
                {
                    ballGoingRight = !ballGoingRight;
                }
                else if(ballX > rightPaddleX)
                {
                    ResetBall();
                    scoreL++;
                    
                }
                Thread.Sleep(50);
                ClearBall();
            }
        }
        static void ResetBall()
        {
            ballX = gameWidth / 2;
            ballY = gameHeight / 2;
            Console.SetCursorPosition(ballX, ballY);
            Console.WriteLine(ball);
        }

        static void ClearBall()
        {
            Console.SetCursorPosition(ballX, ballY);
            Console.WriteLine(" ");
        }
    }

}