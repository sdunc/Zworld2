using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zworld
{
    public partial class GameWorld
    {
        public List<ICharacter> Enemies { get; set; }
        public List<ICharacter> Allies { get; set; }
        public int Turn { get; set; }

        private Random random = new Random();

        public GameWorld()
        {
            Reset();
        }

        public void NextTurn()
        {
            if (Allies.Count == 0 || Enemies.Count == 0)
            {
                Reset();
                return;
            }

            Turn++;


            foreach (ICharacter enemy in Enemies)
            {
                ICharacter Ally = Allies.FirstOrDefault();

                if (Ally == null)
                {
                    Console.WriteLine($"[{Turn}]\tEnemies win!");
                    return;
                }

                Ally.Health -= enemy.AttackPower;
                if (Ally.Health <= 0)
                {
                    Allies.Remove(Ally);
                    Console.WriteLine($"[{Turn}]\tEnemy {enemy.Name} deals {enemy.AttackPower} and kills {Ally.Name}!");
                }
                else
                {
                    Console.WriteLine($"[{Turn}]\tEnemy {enemy.Name} deals {enemy.AttackPower} to {Ally.Name}.");
                }
            }

            foreach (ICharacter ally in Allies)
            {
                ICharacter enemy = Enemies.First();

                if (enemy == null)
                {
                    Console.WriteLine($"[{Turn}]\tAllies win!");
                    return;
                }

                enemy.Health -= ally.AttackPower;
                if (enemy.Health <= 0)
                {
                    Enemies.Remove(enemy);
                    Console.WriteLine($"[{Turn}]\tAlly {ally.Name} deals {ally.AttackPower} and kills {enemy.Name}!");
                }
                else
                {
                    Console.WriteLine($"[{Turn}]\tAlly {ally.Name} deals {ally.AttackPower} to {enemy.Name}!");
                }
            }


        }

        void Reset()
        {
            Console.WriteLine("Resetting...");

            Allies = new List<ICharacter>();
            Allies.Add(new Hero(random.Next(50, 100), random.Next(10, 20), "Zach"));
            Console.WriteLine("Added Hero Zach to Allies team.");

            Enemies = new List<ICharacter>();
            
            for (int i = 0; i < random.Next(3,6); i++)
            {
                int mob = random.Next(0, 3);
                if (mob == 0)
                {
                    Enemies.Add(new Orc()); // orc with random name
                    Console.WriteLine($"Added Orc {Enemies.Last().Name} to Enemies team.");
                }
                else
                {
                    Enemies.Add(new Goblin(10, 4));
                    Console.WriteLine($"Added Goblin {Enemies.Last().Name} to Enemies team.");
                }
            }

            Turn = 0;
        }
    }

    public interface ICharacter
    {
        string Name { get; set; }
        int Health { get; set; }
        int AttackPower { get; set; }
    }

    public class Hero : ICharacter
    {
        public Hero(int health, int attackPower, string name)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
        }

        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set;}
    }

    public class Goblin : ICharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }

        public Goblin(int health, int attackPower, string? name = null)
        {
            Health = health;
            AttackPower = attackPower;

            if (name == null)
            {
                Name = GoblinNameGenerate();
            }
            else
            {
                Name = name;
            }
        }

        public void Attack(ICharacter defender)
        {
            // Implement the attack logic
        }

        private string GoblinNameGenerate()
        {
            string name = "General Bentnose";
            // TODO: Implement name generator.
            return name;
        }
    }

    public class Orc : ICharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }

        public Orc(string? name = null)
        {
            Random random = new Random();
            Health = random.Next(2, 4);
            AttackPower = random.Next(2, 12);

            if (name == null)
            {
                Name = OrcNameGenerate();
            }
            else
            {
                Name = name;
            }
        }

        private string OrcNameGenerate()
        {
            string name = "ORC";
            // TODO: Implement name generator.
            return name;
        }
    }

}
