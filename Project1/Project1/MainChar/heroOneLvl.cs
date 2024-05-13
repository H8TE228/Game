using System;

namespace Project1.MainChar
{
    public class heroOneLvl
    {
        public int HealthPoints { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }

        public heroOneLvl(int healthPoints, int damage, int armor)
        {
            HealthPoints = healthPoints;
            Damage = damage;
            Armor = armor;
        }

        public heroOneLvl()
        {
            HealthPoints = 30;
            Damage = 10;
            Armor = 5;
        }

        public void Attack(heroOneLvl warriorOneLvl)
        {
            int actualDamage = Math.Max(0, Damage - warriorOneLvl.Armor);
            warriorOneLvl.HealthPoints -= actualDamage;
        }

        public bool IsAlive()
        {
            return HealthPoints > 0;
        }
    }
}
