using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    enum GameMode
    {
        None,
        Lobby,
        Town,
        Field
    }
    internal class Game
    {
        public GameMode mode = GameMode.Lobby;
        public Player player = null;
        public Monster monster = null;
        Random random = new Random();

        public void Process()
        {
            switch (mode)
            {
                case GameMode.None:
                    break;
                case GameMode.Lobby:
                    LobbyProcess();
                    break;
                case GameMode.Town:
                    TownProcess();
                    break;
                case GameMode.Field:
                    FieldProcess();
                    break;
            }
        }

        public void LobbyProcess()
        {
            Console.WriteLine("게임이 실행됐습니다.\n당신의 캐릭터를 고르세요.");
            Console.WriteLine("[1] 기사");
            Console.WriteLine("[2] 궁수");
            Console.WriteLine("[3] 법사");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    player = new Knight();
                    Console.WriteLine("기사를 고르셨습니다.");
                    mode = GameMode.Town;
                    break;
                case "2":
                    player = new Archer();
                    Console.WriteLine("궁수를 고르셨습니다.");
                    mode = GameMode.Town;
                    break;
                case "3":
                    player = new Mage();
                    Console.WriteLine("법사를 고르셨습니다.");
                    mode = GameMode.Town;
                    break;
            }
        }

        public void TownProcess()
        {
            Console.WriteLine("마을에 접속했습니다.");
            Console.WriteLine("[1] 필드로 가기");
            Console.WriteLine("[2] 직업선택창으로 가기");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    mode = GameMode.Field;
                    break;
                case "2":
                    mode = GameMode.Lobby;
                    break;
            }
        }

        public void FieldProcess()
        {
            Console.WriteLine("필드에 접속했습니다.");
            CreateRandomMonster();
            Console.WriteLine("[1] 싸우기");
            Console.WriteLine("[2] 일정 확률로 도망가기");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    FightProcess();
                    break;
                case "2":
                    TryEscape();
                    break;
            }
        }

        public void CreateRandomMonster()
        {
            int RandomMonster = random.Next(1, 4);
            switch (RandomMonster)
            {
                case 1:
                    Console.WriteLine("슬라임이 스폰됐습니다.");
                    monster = new Slime();
                    break;
                case 2:
                    Console.WriteLine("오크가 스폰됐습니다.");
                    monster = new Orc();
                    break;
                case 3:
                    Console.WriteLine("스켈레톤이 스폰됐습니다.");
                    monster = new Skeleton();
                    break;
            }
        }

        public void FightProcess()
        {
            while (true)
            {
                int damage = player.GetAttack();
                monster.OnDamaged(damage);
                if(monster.IsDead())
                {
                    Console.WriteLine($"승리했습니다. 남은 체력 : {player.GetHp()}");
                    break;
                }

                damage = monster.GetAttack();
                player.OnDamaged(damage);
                if(player.IsDead())
                {
                    Console.WriteLine("패배했습니다.");
                    mode = GameMode.Lobby;
                    break;
                }
            }
        }

        public void TryEscape()
        {
            int RandValue = random.Next(1, 100);
            if (RandValue <= 33)
            {
                Console.WriteLine("도망 성공!");
                mode = GameMode.Town;
            }
            else
            {
                FightProcess();
            }
        }
    }
}
