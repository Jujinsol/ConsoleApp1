using System;

namespace ConsoleApp1
{
    class Program
    {
        enum PlayerType
        {
            None = 0,
            Knight = 1,
            Archer = 2,
            Wizard = 3
        }
        struct Player
        {
            public int hp;
            public int attack;
        }
        enum MonsterType
        {
            None = 0,
            Slime = 1,
            Orc = 2,
            Skeleton = 3
        }
        struct Monster
        {
            public int hp;
            public int attack;
        }
        static PlayerType ChoosePlayer()
        {
            while (true)
            {
                PlayerType choice = PlayerType.None;
                Console.WriteLine("직업을 선택해주세요.");
                Console.WriteLine("[1] 기사");
                Console.WriteLine("[2] 궁수");
                Console.WriteLine("[3] 법사");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("기사를 선택하셨습니다.");
                        choice = PlayerType.Knight;
                        break;
                    case "2":
                        Console.WriteLine("궁수를 선택하셨습니다.");
                        choice= PlayerType.Archer;
                        break;
                    case "3":
                        Console.WriteLine("법사를 선택하셨습니다.");
                        choice = PlayerType.Wizard;
                        break;
                }
                return choice;
            }
        }
        static void CreatePlayer(PlayerType choice, out Player player)
        {
            switch (choice)
            {
                case PlayerType.Knight:
                    player.hp = 100;
                    player.attack = 10;
                    break;
                case PlayerType.Archer:
                    player.hp = 75;
                    player.attack = 12;
                    break;
                case PlayerType.Wizard:
                    player.hp = 50;
                    player.attack = 15;
                    break;
                default:
                    player.hp = 0;
                    player.attack = 0;
                    break;
            }
        }
        static void CreateMonster(out Monster monster)
        {
            Random rand = new Random();
            int randMonster = rand.Next(1, 4);

            switch (randMonster)
            {
                case (int)MonsterType.Slime:
                    Console.WriteLine("슬라임이 스폰됐습니다.");
                    monster.hp = 20;
                    monster.attack = 4;
                    break;
                case (int)MonsterType.Orc:
                    Console.WriteLine("오크가 스폰됐습니다.");
                    monster.hp = 15;
                    monster.attack = 5;
                    break;
                case (int)MonsterType.Skeleton:
                    Console.WriteLine("스켈레톤이 스폰됐습니다.");
                    monster.hp = 25;
                    monster.attack = 3;
                    break;
                default:
                    monster.hp = 0;
                    monster.attack = 0;
                    break;
            }
        }
        static void Fight(ref Monster monster,ref Player player)
        {
            while (true)
            {
                monster.hp -= player.attack;
                if (monster.hp <= 0)
                {
                    Console.WriteLine("승리!");
                    Console.WriteLine($"남은 체력 : {player.hp}");
                    break;
                }
                player.hp -= monster.attack;
                if (player.hp <= 0)
                {
                    Console.WriteLine("패배!");
                    break;
                }
            }
        }
        static void EnterField(ref Player player)
        {
            while (true)
            {

                Monster monster;
                Console.WriteLine("필드에 들어왔습니다.");
                CreateMonster(out monster);
                Console.WriteLine("[1] 전투모드 돌입");
                Console.WriteLine("[2] 일정 확률로 도망가기");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Fight(ref monster, ref player);
                }
                else if (input == "2")
                {
                    Random rand = new Random();
                    int randValue = rand.Next(0, 101);

                    if (randValue <= 33)
                    {
                        Console.WriteLine("도망 성공");
                        break;
                    }
                    else
                        Fight(ref monster,ref player);
                }
            }
        }
        static void EnterGame(ref Player player)
        {
            while (true)
            {
                Console.WriteLine("마을에 들어왔습니다.");
                Console.WriteLine("[1] 필드로 가기");
                Console.WriteLine("[2] 로비로 가기");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    EnterField(ref player);
                }
                else if (input == "2")
                {
                    break;
                }
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                PlayerType choice = ChoosePlayer();
                if (choice == PlayerType.None)
                    continue;
            
            Player player;
            CreatePlayer(choice, out player);
            EnterGame(ref player);
            }
        }
    }
}