using System;
using BossFightSim;

public class Program
{
    public static void Main()
    {
        Console.Clear();
        Console.WriteLine("Welcome to Boss Fight Simulator!\n");
        Console.WriteLine("Menu");
        Console.WriteLine("1) Simulate Fight");
        Console.WriteLine("2) Exit App");

        char menuChoice = Console.ReadKey(true).KeyChar;
        switch (menuChoice)
        {
            case '1':
                GameCharacter.SimulateFight();
                break;
            case '2':
                Environment.Exit(404);
                break;
        }


    }
}



                