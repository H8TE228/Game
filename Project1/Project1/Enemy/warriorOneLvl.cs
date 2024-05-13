using System;
using Project1.MainChar;

namespace Project1.Enemy
{
    public class warriorOneLvl
    {
        public int HealthPoints { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }

        public warriorOneLvl(int healthPoints, int damage, int armor)
        {
            HealthPoints = healthPoints;
            Damage = damage;
            Armor = armor;
        }
        public warriorOneLvl()
        {
            HealthPoints = 20;
            Damage = 5;
            Armor = 2;
        }
    }
}
