using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BossFightSim
{
    internal class GameCharacter
    {
        private string _name;
        private int _health;
        private int _strength;
        private int _stamina;

        private static bool isFightActive = false;
        private static int i = 0;

        private static GameCharacter _hero;
        private static GameCharacter _boss;


        private static List<GameCharacter> participants = new List<GameCharacter>();

        public GameCharacter(string name, int str, int stam, int hp = 400)
        {
            _name = name;
            _health = hp;
            _strength = str;
            _stamina = stam;

        }

        public static void SimulateFight()
        {
            Console.Clear();
            CreateChars();
            StartFight();



        }

        private static void StartFight()
        {
            isFightActive = true;

            while (isFightActive)
            {
                Thread.Sleep(1000);
                if (i == 0)
                {
                    Fight(i);
                    Thread.Sleep(1000);
                }
                else if (i == 1)
                {
                    Fight(i);
                    Thread.Sleep(1000);
                }
            }
        }
        public static void Fight(int character)
        {
            if (character == 0)
            {
                DidTheyDie();
                if (participants[character]._stamina == 0)
                {
                    Recharge(character);
                    StartFight();
                }
                else
                {
                    participants[character + 1]._health -= participants[character]._strength;
                    participants[character]._stamina -= 10;
                    PrintGameCharName(0, ConsoleColor.Blue);
                    Console.Write("(" + participants[character]._health + "HP)");
                    Console.Write(" took " + participants[character]._strength + "dmg from ");
                    PrintGameCharName(1, ConsoleColor.Red);
                    Console.Write("(" + participants[character + 1]._health + "HP)");
                    Console.WriteLine();
                    Console.WriteLine($"Remaining stamina: {participants[character]._stamina} \n");

                    i++;
                    Thread.Sleep(1000);
                    StartFight();

                }

            }
            else if (character == 1)
            {
                DidTheyDie();
                if (participants[character]._stamina == 0)
                {
                    Recharge(character);
                    StartFight();
                }
                else
                {
                    participants[character - 1]._health -= participants[character]._strength;
                    participants[character]._stamina -= 10;
                    PrintGameCharName(1, ConsoleColor.Red);
                    Console.Write("(" + participants[character]._health + "HP)");
                    Console.Write(" took " + participants[character]._strength + "dmg from ");
                    PrintGameCharName(0, ConsoleColor.Blue);
                    Console.Write("(" + participants[character - 1]._health + "HP)");
                    Console.WriteLine();
                    Console.WriteLine($"Remaining stamina: {participants[character]._stamina} \n");

                    i--;
                    Thread.Sleep(1000);
                    StartFight();


                }
            }

        }

        public static void Recharge(int index)
        {
            if (index == 0)
            {
                participants[index]._stamina = 40;
                i += 1;
                Console.WriteLine();
                PrintGameCharName(0, ConsoleColor.Blue);
                Console.Write("recharged his stamina and skipped this turn.\n\n\n");
                Thread.Sleep(2000);
            }
            else
            {
                participants[index]._stamina = 10;
                i -= 1;
                Console.WriteLine();
                PrintGameCharName(1, ConsoleColor.Red);
                Console.Write(" recharged his stamina and skipped this turn.\n\n\n");
                Thread.Sleep(2000);
            }
        }

        public static void CreateChars()
        {
            _hero = new GameCharacter("Hero", 20, 40, 100);
            participants.Add(_hero);
            var rand = new Random();
            var RandomStr = rand.Next(0, 30);
            _boss = new GameCharacter("Boss", RandomStr, 10);
            participants.Add(_boss);
        }

        public static void PrintGameCharName(int index, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(participants[index]._name);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void DidTheyDie()
        {
            if (participants[0]._health <= 0)
            {
                isFightActive = false;
                PrintGameCharName(1, ConsoleColor.Red);
                Console.Write($" has won the match with {participants[1]._health}HP remaining!\n\n");
                Console.WriteLine("Press any key to go back to the main menu...");
                Console.ReadKey();
                Program.Main();


            } else if (participants[1]._health <= 0)
            {
                isFightActive = false;
                PrintGameCharName(0, ConsoleColor.Blue);
                Console.Write($" has won the match with {participants[0]._health}HP remaining!\n\n");
                Console.WriteLine("Press any key to go back to the main menu...");
                Console.ReadKey();
                Program.Main();
            }
        }
    }
}
