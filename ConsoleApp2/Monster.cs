using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    enum MonsterType
    {
        None,
        Slime,
        Orc,
        Skeleton
    }
    internal class Monster : Creature
    {
        protected MonsterType type = MonsterType.None;

        protected Monster(MonsterType type) : base(CreatureType.Monster)
        {
            this.type = type;
        }

    }
    class Slime : Monster
    {
        public Slime() :base(MonsterType.Slime)
        {
            SetInfo(20, 5);
        }
    }
    class Orc : Monster
    {
        public Orc() : base(MonsterType.Orc)
        {
            SetInfo(25, 8);
        }
    }
    class Skeleton : Monster
    {
        public Skeleton() : base(MonsterType.Skeleton)
        {
            SetInfo(22, 10);
        }
    }
}
