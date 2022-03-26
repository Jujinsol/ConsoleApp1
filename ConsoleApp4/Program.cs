using System;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(); 
            Board board = new Board();
            board.Initialize(25, player);
            player.Initialize(1, 1, board);

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;

            int lastTick = 0;
            while (true)
            {
                #region 프레임 관리
                // 만약 경과한 시간이 1/30보다 작다면
                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < WAIT_TICK)
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion

                Console.SetCursorPosition(0, 0);
                player.Update(deltaTick);
                board.Render();
            }
        }
    }
}