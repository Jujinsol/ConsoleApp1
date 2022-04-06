using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Board
    {
        public enum TileType
        {
            Wall,
            Empty
        }

        public int Size { get; private set; }
        public int DestY { get; private set; }
        public int DestX { get; private set; }

        public TileType[,] Tile { get; private set; }
        const char Circle = '\u25cf';
        Player _player;

        public void Initialize(int size, Player player)
        {
            // size는 홀수여야한다.
            if (size % 2 == 0)
                return;

            Tile = new TileType[size, size];

            Size = size;
            _player = player;

            DestY = size - 2;
            DestX = size - 2;

            GenerateSideWinder();
        }

        void GenerateSideWinder()
        {
            // 격자로 벽 만들기
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (y % 2 == 0 || x % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (y == Size - 1 || x == Size - 1)
                    {
                        Tile[y, x] = TileType.Wall;
                        continue;
                    }
                    if (y % 2 == 0 || x % 2 == 0)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }


                    if (y == Size - 2 && x == Size - 2)
                        Tile[y, x] = TileType.Empty;


                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randNum = rand.Next(0, count);
                        Tile[y + 1, x - randNum * 2] = TileType.Empty;
                        count = 1;
                    }

                }
            }

        }

        public void Render()
        {
            ConsoleColor PrevColor = Console.ForegroundColor;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);

                    Console.Write(Circle);
                }
                Console.WriteLine();
            }
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
