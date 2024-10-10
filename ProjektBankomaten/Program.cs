namespace ProjektBankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Setting all values for account names and pins, the account holders names, internal account names and their balances.
            int[,] users = { {1, 1337},
                             {2, 9988},
                             {3, 4550},
                             {4, 2003},
                             {5, 5858} };

            string[] userLegalName = { "Viktor",
                                       "Sven",
                                       "Erik",
                                       "Pelle",
                                       "Robin" };

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

            //Creates two booleans that handle how far the program can run, programActive is always true unless you fail your login attempts.
            bool programActive = true;
            bool loggedIn = false;

            while (programActive)
            {
                Console.Clear();
                DisplayLogo();
                Console.WriteLine("Hej! Välkommen till bankomaten!");
                Console.Write("Var god mata in ditt användar-ID: ");
                int userName = Int32.Parse(Console.ReadLine());
                int userIndex = FindUserIndex(users, userName);

                //If you input an invalid user ID the index gets set to 666 which this if-statement checks for.
                if (userIndex != 666)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Console.Clear();
                        DisplayLogo();
                        Console.Write("Var god mata in din pinkod: ");
                        int userPass = Int32.Parse(Console.ReadLine());
                        if (userPass == users[userIndex, 1])
                        {
                            loggedIn = true;
                            break;
                        }
                    }
                    //If you are not logged in after three attempts, the program shuts down.
                    if (loggedIn != true)
                    {
                        programActive = false;
                    }

                    //Adds the options to select what to do inside the bank and calls for the main menu method.
                    while (loggedIn == true)
                    {
                        Console.Clear();
                        DisplayLogo();
                        Console.WriteLine($"Välkommen {userLegalName[userIndex]}!");
                        switch (MainMenu())
                        {
                            case 1:
                                AccountOverview(userIndex, userAccounts, accountBalances);
                                break;
                            case 2:
                                Transfer(userIndex, userAccounts, accountBalances);
                                break;
                            case 3:
                                Withdraw(userIndex, userAccounts, accountBalances, users);
                                break;
                            case 4:
                                loggedIn = false;
                                break;
                        }
                        if (loggedIn == true)
                        {
                            Console.WriteLine("Tryck på valfri tangent för att återgå...");
                            Console.ReadKey();
                        }
                    }
                }
            }
            Console.WriteLine("Du har ej behörighet, stänger ner...");
            Console.ReadKey();

        }

        //Searches for the user ID and converts it to a correctly indexed number.
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

        //Displays the main menu and returns a menu choice.
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

        //Displays the balance of your accounts.
        static void AccountOverview(int userIndex, string[][] userAccounts, double[][] accountBalances)
        {
            Console.Clear();
            DisplayLogo();
            Console.WriteLine("Din kontoöversikt:");
            for (int i = 0; i < userAccounts[userIndex].Length; i++)
            {
                string accountName = userAccounts[userIndex][i];
                double accountBalance = accountBalances[userIndex][i];
                Console.WriteLine($"{accountName}: {accountBalance:C}");
            }
        }

        //Used for the transferring of money between internal accounts.
        static void Transfer(int userIndex, string[][] userAccounts, double[][] accountBalances)
        {
            ConsoleColor recentForegroundColor = Console.ForegroundColor;
            Console.Clear();
            DisplayLogo();
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

            //Lowers the number to account for indexing.
            accountFrom -= 1;
            accountTo -= 1;

            bool transactionComplete = false;

            while (!transactionComplete)
            {

                Console.Write("Hur mycket vill du föra över? (kr): ");
                int transferAmount = Int32.Parse(Console.ReadLine());

                //Checks if there is enough money to perform the transaction and that you dont add a negative amount of money.
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
            DisplayLogo();
            Console.WriteLine($"Dina nya saldon är: \n" +
                $"{userAccounts[userIndex][accountFrom]}: {accountBalances[userIndex][accountFrom]:C} \n" +
                $"{userAccounts[userIndex][accountTo]}: {accountBalances[userIndex][accountTo]:C}");
        }

        //Adds the method to withdraw money
        static void Withdraw(int userIndex, string[][] userAccounts, double[][] accountBalances, int[,] users)
        {
            ConsoleColor recentForegroundColor = Console.ForegroundColor;
            Console.Clear();
            DisplayLogo();
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
            Console.Write("Välj ett konto att ta ut pengar från: ");
            int accountChoice = Int32.Parse(Console.ReadLine());
            accountChoice -= 1;
            bool inputAccepted = false;
            int withdrawAmount = 0;

            while (!inputAccepted)
            {
                Console.Write("Hur mycket du vill ta ut: ");
                withdrawAmount = Int32.Parse(Console.ReadLine());
                if (withdrawAmount <= accountBalances[userIndex][accountChoice] && withdrawAmount >= 0)
                {
                    inputAccepted = true;
                }
                else
                {
                    Console.WriteLine("Du har inte så mycket pengar. Försök igen.");
                }
            }


            bool withdrawalComplete = false;

            Console.Write("Du måste bekräfta din identitet för att genomföra denna handling, mata in din pinkod: ");
            while (!withdrawalComplete)
            {
                int pinInput = Int32.Parse(Console.ReadLine());

                //Checks if the pin the user inputs is correct, withdraws the money if it is.
                if (pinInput == users[userIndex, 1])
                {
                    accountBalances[userIndex][accountChoice] -= withdrawAmount;
                    withdrawalComplete = true;
                }
                else
                {
                    Console.Write("Felaktig pin kod, försök igen: ");
                }
            }
            Console.Clear();
            DisplayLogo();
            Console.WriteLine($"Ditt nya saldo för {userAccounts[userIndex][accountChoice]} är: {accountBalances[userIndex][accountChoice]:C}");
        }

        //Displays the ASCII art logo.
        static void DisplayLogo()
        {
            ConsoleColor recentForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("   :::     ::: :::    ::: :::::::::      :::     ::::    ::: :::    ::: \r\n  :+:     :+: :+:    :+: :+:    :+:   :+: :+:   :+:+:   :+: :+:   :+:   \r\n +:+     +:+ +:+    +:+ +:+    +:+  +:+   +:+  :+:+:+  +:+ +:+  +:+     \r\n+#+     +:+ +#++:++#++ +#++:++#+  +#++:++#++: +#+ +:+ +#+ +#++:++       \r\n+#+   +#+  +#+    +#+ +#+    +#+ +#+     +#+ +#+  +#+#+# +#+  +#+       \r\n#+#+#+#   #+#    #+# #+#    #+# #+#     #+# #+#   #+#+# #+#   #+#       \r\n ###     ###    ### #########  ###     ### ###    #### ###    ###       ");
            Console.ForegroundColor = recentForegroundColor;
        }
    }
}
