using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }
    enum Dir
    {
        Up,
        Left,
        Down,
        Right
    }
    class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }

        Board _board; 
        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void Initialize(int posY, int posX, Board board)
        {
            _board = board;

            PosY = posY;
            PosX = posX;


            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };


            _points.Add(new Pos(posY, posX));
            while (PosY != _board.DestY || PosX != _board.DestX)
            {
                // 1. 보는 방향 기준 오른쪽으로 갈 수 있나 확인 후 
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    // 1-1. 오른쪽회전
                    _dir = (_dir + 3) % 4;
                    // 1-2. 전진 한 보
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];

                    _points.Add(new Pos(PosY, PosX));
                }
                // 2. 안되면 보는 방향 기준, 전진할 수 있나 확인 후
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    // 2-1. 전진 한 보
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];

                    _points.Add(new Pos(PosY, PosX));
                }
                // 3. 안되면 왼쪽으로 90도 회전
                else
                {
                    _dir = (_dir + 5) % 4;
                }
            }
        }

        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;

        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
            }
        }
    }
}
