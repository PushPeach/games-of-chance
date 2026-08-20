using CasinoApp.Games;

namespace CasinoApp;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Willkommen im Casino ===\n");

        decimal startingBalance = ReadStartingBalance();
        var player = new Player("Spieler", startingBalance);

        var games = new List<IGame>
        {
            new Blackjack(),
            new Roulette()
        };

        while (true)
        {
            if (player.IsBroke())
            {
                Console.WriteLine("\nDu hast kein Guthaben mehr. Spiel beendet.");
                break;
            }

            Console.WriteLine($"\n--- Hauptmenü (Guthaben: {player.Balance:F2}) ---");
            for (int i = 0; i < games.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {games[i].Name}");
            }
            Console.WriteLine($"{games.Count + 1}) Guthaben anzeigen");
            Console.WriteLine($"{games.Count + 2}) Beenden");

            int choice = InputHelper.ReadIntInRange("Auswahl: ", 1, games.Count + 2);

            if (choice == games.Count + 1)
            {
                Console.WriteLine($"Aktuelles Guthaben: {player.Balance:F2}");
                continue;
            }

            if (choice == games.Count + 2)
            {
                Console.WriteLine("Danke fürs Spielen. Auf Wiedersehen!");
                break;
            }

            games[choice - 1].Play(player);
        }
    }

    private static decimal ReadStartingBalance()
    {
        while (true)
        {
            Console.Write("Startguthaben festlegen (mind. 10): ");
            string? input = Console.ReadLine();

            if (!decimal.TryParse(input, out decimal balance))
            {
                Console.WriteLine("Bitte eine gültige Zahl eingeben.");
                continue;
            }

            if (balance < 10)
            {
                Console.WriteLine("Das Startguthaben muss mindestens 10 betragen.");
                continue;
            }

            return balance;
        }
    }
}
