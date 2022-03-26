using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Board
    {
        public enum TileType
        {
            Empty,
            Wall
        }

        public int Size { get; private set; }
        public int DestY { get; private set; }
        public int DestX { get; private set; }
        public TileType[,] Tile { get; private set; }
        const char Circle = '\u25cf';
        Player _player = new Player();
        

        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0)
                return;

            Tile = new TileType[size, size];

            DestY = size - 2;
            DestX = size - 2;

            _player = player;
            Size = size;

            GenerateSideWind();
        }

        public void GenerateSideWind()
        {

            // 격자 무늬로 벽 만들기
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (y % 2 == 0 || x % 2 == 0) // 짝수라면
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }

            // 초록점 기준, 랜덤하게 오른쪽 혹은 아래 뚫기
            Random random = new Random();
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

                    int randRoad = random.Next(0, 2);
                    if (randRoad == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;
                    }
                    else
                    {
                        int randIndex = random.Next(0, count);
                        Tile[y + 1, x - randIndex * 2] = TileType.Empty;
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
                    if (y == DestY && x == DestX)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);

                    Console.Write(Circle);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = PrevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Wall:
                    return ConsoleColor.Red;
                case TileType.Empty:
                    return ConsoleColor.Green;
                default:
                    return ConsoleColor.Green;
            }
        }
    }
}
