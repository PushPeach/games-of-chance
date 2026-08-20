namespace CasinoApp.Cards;

public enum Suit
{
    Herz,
    Karo,
    Pik,
    Kreuz
}

public class Card
{
    public string Rank { get; }
    public Suit Suit { get; }

    public Card(string rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    // Blackjack-Wert der Karte. Ass wird hier als 11 gezählt;
    // die Anpassung auf 1 passiert in der Hand-Berechnung (siehe Blackjack.cs).
    public int Value => Rank switch
    {
        "A" => 11,
        "K" or "Q" or "J" => 10,
        _ => int.Parse(Rank)
    };

    public override string ToString() => $"{Rank}{SuitSymbol()}";

    private string SuitSymbol() => Suit switch
    {
        Suit.Herz => "♥",
        Suit.Karo => "♦",
        Suit.Pik => "♠",
        Suit.Kreuz => "♣",
        _ => "?"
    };
}
