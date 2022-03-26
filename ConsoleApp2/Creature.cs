using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    enum CreatureType
    {
        None,
        Player,
        Monster
    }
    class Creature
    {
        CreatureType type = CreatureType.None;

        protected int hp = 0;
        protected int attack = 0;

        public Creature(CreatureType type)
        {
            this.type = type;
        }

        public int GetHp() { return hp; }
        public int GetAttack() { return attack; }
        public void SetInfo(int hp, int attack)
        {
            this.hp = hp;
            this.attack = attack;
        }
        public bool IsDead() { return hp <= 0; }
        public void Ondamage(int damage)
        {
            hp -= damage;
            if (hp < 0)
                hp = 0;
        }

    }
}
