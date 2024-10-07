namespace ProjektBankomaten
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] users = { {1, 1234},
                             {2, 1234},
                             {3, 1234},
                             {4, 1234},
                             {5, 1234} };

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
                new double[] {},
                new double[] {},
                new double[] {},
                new double[] {},
                new double[] {},
            };

            Console.WriteLine("Hej! Välkommen till bankomaten!");
        }

        static void UserLogin()
        {
            Console.Write("Var god mata in ditt användarnamn: ");
            string userName = Console.ReadLine();
        }
    }
}
