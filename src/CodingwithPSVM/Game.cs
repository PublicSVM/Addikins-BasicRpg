﻿using CodingwithPSVM.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingwithPSVM.Items;

namespace CodingwithPSVM
{
    public class Game
    {
        private Random Random = new Random();
        private UserInput UserInput = new UserInput();

        public void Start()
        {
            var hero = CreateHero();
            Console.WriteLine(hero.GetStatus());
            
            var enemy = new Enemy(EnemyClass.AllClasses[0]);
            Console.WriteLine($"{enemy.EnemyClass.Name} The wise\n Exp:{enemy.EnemyClass.Exp}");
            Fight(hero, enemy);
        }

        private void Fight(Hero hero, Enemy enemy)
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption("Attack", $"Use a melee attack against the {enemy.EnemyClass.Name}"),
                new MenuOption("Run", $"Run away from the {enemy.EnemyClass.Name}")
            };
            while(true) 
            {
                var choice = UserInput.ChooseOption(options);

                switch (choice.ToLower())
                {
                    case "attack":
                        enemy.TakeDamage(hero.GetAttack(), false);
                        break;
                    case "run":
                        Console.WriteLine("You get away!");
                        return;
                    default:
                        Console.WriteLine("Error");
                        break;
                }

                if (enemy.Health > 0)
                {
                    hero.TakeDamage(enemy.Attack, false);
                }
                if (hero.Health <= 0 || enemy.Health <= 0)
                {
                    break;
                }
            }
        }

        private Hero CreateHero()
        {
            Console.WriteLine("What is your name?");
            var name = UserInput.GetHeroName();

            Console.WriteLine("Which class do you want?");
            var heroClass = UserInput.ChooseHeroClass();

            var armor = new Armor("No Armor", 0, "Yer naked", 0, 0);
            var weapon = new Weapon("No Weapon", 0, "Just flail around a bit", 0, 0);

            return new Hero(name, heroClass, armor, weapon);
        }

        private IItem FindLoot()
        {
            var lootChance = Random.Next(4);
            switch (lootChance)
            {
                case 0:
                    return GetLootFrom(Food.AllFood);
                case 1:
                    return GetLootFrom(Potion.AllPots);
                case 2:
                    return GetLootFrom(Weapon.AllWeapons);
                case 3:
                default:
                    return GetLootFrom(Armor.AllArmorsets);
            }
        }

        private T GetLootFrom<T>(List<T> items)
        {
            var index = Random.Next(items.Count);
            return items[index];
        }
    }
}
