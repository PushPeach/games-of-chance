namespace CasinoApp.Games;

public class Roulette : IGame
{
    public string Name => "Roulette";

    private static readonly HashSet<int> RedNumbers = new()
    {
        1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36
    };

    private readonly Random _random = new();

    public void Play(Player player)
    {
        Console.WriteLine("\n=== Roulette ===");
        Console.WriteLine("Einsatzarten: 1) Zahl (0-36, zahlt 35:1)  2) Farbe Rot/Schwarz (zahlt 1:1)  3) Gerade/Ungerade (zahlt 1:1)");

        int betType = InputHelper.ReadIntInRange("Einsatzart wählen (1-3): ", 1, 3);
        decimal bet = InputHelper.ReadBet(player);

        int chosenNumber = 0;
        string chosenColor = "";
        string chosenParity = "";

        switch (betType)
        {
            case 1:
                chosenNumber = InputHelper.ReadIntInRange("Zahl wählen (0-36): ", 0, 36);
                break;
            case 2:
                chosenColor = ReadColor();
                break;
            case 3:
                chosenParity = ReadParity();
                break;
        }

        int result = _random.Next(0, 37); // 0-36
        string resultColor = GetColor(result);
        Console.WriteLine($"\nDie Kugel landet auf: {result} ({resultColor})");

        bool won = betType switch
        {
            1 => result == chosenNumber,
            2 => resultColor.Equals(chosenColor, StringComparison.OrdinalIgnoreCase),
            3 => GetParity(result) == chosenParity,
            _ => false
        };

        if (won)
        {
            decimal multiplier = betType == 1 ? 35m : 1m;
            decimal winnings = bet * multiplier;
            player.Add(winnings);
            Console.WriteLine($"Gewonnen! Du erhältst {winnings:F2}.");
        }
        else
        {
            player.Deduct(bet);
            Console.WriteLine("Verloren.");
        }

        Console.WriteLine($"Aktuelles Guthaben: {player.Balance:F2}");
    }

    private static string ReadColor()
    {
        while (true)
        {
            Console.Write("Farbe wählen (rot/schwarz): ");
            string? input = Console.ReadLine()?.Trim().ToLower();
            if (input == "rot" || input == "schwarz") return input;
            Console.WriteLine("Bitte 'rot' oder 'schwarz' eingeben.");
        }
    }

    private static string ReadParity()
    {
        while (true)
        {
            Console.Write("Gerade oder ungerade wählen (gerade/ungerade): ");
            string? input = Console.ReadLine()?.Trim().ToLower();
            if (input == "gerade" || input == "ungerade") return input;
            Console.WriteLine("Bitte 'gerade' oder 'ungerade' eingeben.");
        }
    }

    private static string GetColor(int number)
    {
        if (number == 0) return "grün";
        return RedNumbers.Contains(number) ? "rot" : "schwarz";
    }

    private static string GetParity(int number)
    {
        if (number == 0) return "keins"; // 0 zählt bei Gerade/Ungerade-Wetten nicht
        return number % 2 == 0 ? "gerade" : "ungerade";
    }
}
