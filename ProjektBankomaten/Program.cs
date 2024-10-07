﻿namespace ProjektBankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] users = { {1, 1337},
                             {2, 9988},
                             {3, 4550},
                             {4, 2003},
                             {5, 5858} };

            string[] userLegalName = { "Viktor",
                                       "Sven",
                                       "Erik",
                                       "Björn",
                                       "Anna" };

            string[][] userAccounts =
            {
                new string[] {"Lönekonto", "e-sparkonto", "MC-sparkonto"},
                new string[] {"Lönekonto"},
                new string[] {"Lönekonto", "e-sparkonto"},
                new string[] {"Lönekonto", "e-sparkonto", "Hus-sparkonto"},
                new string[] {"Lönekonto", "e-sparkonto"},
            };

            double[][] accountBalances =
            {
                new double[] {17349, 35900, 27500},
                new double[] {22539},
                new double[] {14500, 42350},
                new double[] {29559, 35000, 115000},
                new double[] {18953, 54250},
            };

            bool programActive = true;
            bool loggedIn = false;

            while (programActive)
            {
                Console.WriteLine("Hej! Välkommen till bankomaten!");
                Console.Write("Var god mata in ditt användar-ID: ");
                int userName = Int32.Parse(Console.ReadLine());
                int userIndex = FindUserIndex(users, userName);

                if (userIndex != 666)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Clear();
                        Console.Write("Var god mata in din pinkod: ");
                        int userPass = Int32.Parse(Console.ReadLine());
                        if (userPass == users[userIndex, 1])
                        {
                            loggedIn = true;
                            break;
                        }
                    }
                    if (loggedIn != true)
                    {
                        programActive = false;
                    }

                    while (loggedIn == true)
                    {
                        Console.Clear();
                        Console.WriteLine($"Välkommen {userLegalName[userIndex]}! Gör ditt menyval.");
                        switch (MainMenu())
                        {
                            case 1:
                                AccountOverview(userIndex, userAccounts, accountBalances);
                                break;
                            case 2:
                                Transfer(userIndex, userAccounts, accountBalances);
                                break;
                            case 3:
                                Withdraw();
                                break;
                            case 4:
                                LogOut();
                                break;
                        }
                        Console.WriteLine("Tryck på valfri tangent för att återgå...");
                        Console.ReadKey();
                    }
                }
            }
            Console.WriteLine("Du har ej behörighet, stänger ner...");
            Console.ReadKey();

        }

        static int FindUserIndex(int[,] users, int userName)
        {
            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (users[i, 0] == userName)
                {
                    return i;
                }
            }
            return 666;
        }

        static int MainMenu()
        {
            ConsoleColor recentForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("1. ");
            Console.ForegroundColor = recentForegroundColor;
            Console.WriteLine("Se dina konton och saldo");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("2. ");
            Console.ForegroundColor = recentForegroundColor;
            Console.WriteLine("Överföring mellan konton");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("3. ");
            Console.ForegroundColor = recentForegroundColor;
            Console.WriteLine("Ta ut pengar");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("4. ");
            Console.ForegroundColor = recentForegroundColor;
            Console.WriteLine("Logga ut");

            Console.Write("Ange ett menyval: ");
            int menuChoice = Int32.Parse(Console.ReadLine());
            return menuChoice;
        }

        static void AccountOverview(int userIndex, string[][] userAccounts, double[][] accountBalances)
        {
            Console.Clear();
            Console.WriteLine("Din kontoöversikt:");
            for (int i = 0; i < userAccounts[userIndex].Length; i++)
            {
                string accountName = userAccounts[userIndex][i];
                double accountBalance = accountBalances[userIndex][i];
                Console.WriteLine($"{accountName}: {accountBalance:C}");
            }
        }

        static void Transfer(int userIndex, string[][] userAccounts, double[][] accountBalances)
        {
            ConsoleColor recentForegroundColor = Console.ForegroundColor;
            Console.Clear();
            for (int i = 0; i < userAccounts[userIndex].Length; i++)
            {
                int accountNumber = i + 1;
                string accountName = userAccounts[userIndex][i];
                double accountBalance = accountBalances[userIndex][i];
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{accountNumber}. ");
                Console.ForegroundColor = recentForegroundColor;
                Console.WriteLine($"{accountName}: {accountBalance:C}");
            }
            Console.Write("Välj konto att överföra från: ");
            int accountFrom = Int32.Parse(Console.ReadLine());
            Console.Write("Välj konto att överföra till: ");
            int accountTo = Int32.Parse(Console.ReadLine());

            accountFrom -= 1;
            accountTo -= 1;

            bool transactionComplete = false;

            while (!transactionComplete)
            {

                Console.Write("Hur mycket vill du föra över? (kr): ");
                int transferAmount = Int32.Parse(Console.ReadLine());

                if (transferAmount <= accountBalances[userIndex][accountFrom] && transferAmount >= 0)
                {
                    accountBalances[userIndex][accountFrom] -= transferAmount;
                    accountBalances[userIndex][accountTo] += transferAmount;
                    transactionComplete = true;
                }
                else
                {
                    Console.WriteLine("Du har inte så mycket pengar. Försök igen.");
                }
            }
            Console.Clear();
            Console.WriteLine($"Dina nya saldon är: \n" +
                $"{userAccounts[userIndex][accountFrom]}: {accountBalances[userIndex][accountFrom]:C} \n" +
                $"{userAccounts[userIndex][accountTo]}: {accountBalances[userIndex][accountTo]:C}");
        }

        static void Withdraw()
        {

        }

        static void LogOut()
        {

        }
    }
}
