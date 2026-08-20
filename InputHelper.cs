namespace CasinoApp;

public static class InputHelper
{
    // Liest einen Einsatzbetrag ein, der > 0 und <= aktuellem Guthaben ist.
    // Wiederholt die Abfrage bei ungültiger Eingabe, statt abzustürzen.
    public static decimal ReadBet(Player player)
    {
        while (true)
        {
            Console.Write($"Einsatz (Guthaben: {player.Balance:F2}): ");
            string? input = Console.ReadLine();

            if (!decimal.TryParse(input, out decimal bet))
            {
                Console.WriteLine("Bitte eine gültige Zahl eingeben.");
                continue;
            }

            if (bet <= 0)
            {
                Console.WriteLine("Der Einsatz muss grösser als 0 sein.");
                continue;
            }

            if (bet > player.Balance)
            {
                Console.WriteLine("Du hast nicht genug Guthaben für diesen Einsatz.");
                continue;
            }

            return bet;
        }
    }

    // Liest eine Ganzzahl im gegebenen Bereich [min, max] ein.
    public static int ReadIntInRange(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int value))
            {
                Console.WriteLine("Bitte eine gültige Zahl eingeben.");
                continue;
            }

            if (value < min || value > max)
            {
                Console.WriteLine($"Bitte eine Zahl zwischen {min} und {max} eingeben.");
                continue;
            }

            return value;
        }
    }

    // Liest ja/nein Eingaben (j/n) ein.
    public static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt + " (j/n): ");
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "j" || input == "ja") return true;
            if (input == "n" || input == "nein") return false;

            Console.WriteLine("Bitte mit 'j' oder 'n' antworten.");
        }
    }
}
