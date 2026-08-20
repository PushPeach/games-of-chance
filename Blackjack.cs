using CasinoApp.Cards;

namespace CasinoApp.Games;

public class Blackjack : IGame
{
    public string Name => "Blackjack";

    public void Play(Player player)
    {
        Console.WriteLine("\n=== Blackjack ===");
        decimal bet = InputHelper.ReadBet(player);

        var deck = new Deck();
        var playerHand = new List<Card> { deck.Draw(), deck.Draw() };
        var dealerHand = new List<Card> { deck.Draw(), deck.Draw() };

        Console.WriteLine($"\nDeine Karten: {HandToString(playerHand)} (Wert: {HandValue(playerHand)})");
        Console.WriteLine($"Dealer zeigt: {dealerHand[0]}");

        // Spieler-Runde: Hit oder Stand
        bool playerBust = false;
        while (true)
        {
            if (HandValue(playerHand) == 21)
            {
                Console.WriteLine("Blackjack!");
                break;
            }

            bool hit = InputHelper.ReadYesNo("Karte ziehen (Hit)?");
            if (!hit) break;

            playerHand.Add(deck.Draw());
            Console.WriteLine($"Deine Karten: {HandToString(playerHand)} (Wert: {HandValue(playerHand)})");

            if (HandValue(playerHand) > 21)
            {
                playerBust = true;
                Console.WriteLine("Bust! Du hast über 21.");
                break;
            }
        }

        if (playerBust)
        {
            player.Deduct(bet);
            Console.WriteLine($"Verloren. Neues Guthaben: {player.Balance:F2}");
            return;
        }

        // Dealer-Runde: zieht automatisch bis mindestens 17
        Console.WriteLine($"\nDealer deckt auf: {HandToString(dealerHand)} (Wert: {HandValue(dealerHand)})");
        while (HandValue(dealerHand) < 17)
        {
            dealerHand.Add(deck.Draw());
            Console.WriteLine($"Dealer zieht: {HandToString(dealerHand)} (Wert: {HandValue(dealerHand)})");
        }

        int playerValue = HandValue(playerHand);
        int dealerValue = HandValue(dealerHand);
        bool dealerBust = dealerValue > 21;
        bool playerHasBlackjack = playerValue == 21 && playerHand.Count == 2;

        if (dealerBust || playerValue > dealerValue)
        {
            decimal winnings = playerHasBlackjack ? bet * 1.5m : bet;
            player.Add(winnings);
            Console.WriteLine(dealerBust
                ? $"Dealer hat sich überkauft! Du gewinnst {winnings:F2}."
                : $"Du gewinnst {winnings:F2}!");
        }
        else if (playerValue == dealerValue)
        {
            Console.WriteLine("Unentschieden — Einsatz wird zurückerstattet.");
        }
        else
        {
            player.Deduct(bet);
            Console.WriteLine($"Verloren. Neues Guthaben: {player.Balance:F2}");
        }

        Console.WriteLine($"Aktuelles Guthaben: {player.Balance:F2}");
    }

    // Berechnet den Handwert und behandelt Asse flexibel (11 oder 1),
    // damit man nicht unnötig bustet.
    private static int HandValue(List<Card> hand)
    {
        int total = hand.Sum(c => c.Value);
        int aceCount = hand.Count(c => c.Rank == "A");

        while (total > 21 && aceCount > 0)
        {
            total -= 10; // ein Ass zählt dann als 1 statt 11
            aceCount--;
        }

        return total;
    }

    private static string HandToString(List<Card> hand) => string.Join(" ", hand);
}
