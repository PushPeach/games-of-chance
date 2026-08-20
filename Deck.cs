namespace CasinoApp.Cards;

public class Deck
{
    private readonly List<Card> _cards = new();
    private readonly Random _random = new();

    private static readonly string[] Ranks =
        { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

    public Deck()
    {
        Reset();
    }

    // Baut ein volles 52-Karten-Deck neu auf und mischt es.
    public void Reset()
    {
        _cards.Clear();
        foreach (Suit suit in Enum.GetValues<Suit>())
        {
            foreach (var rank in Ranks)
            {
                _cards.Add(new Card(rank, suit));
            }
        }
        Shuffle();
    }

    private void Shuffle()
    {
        // Fisher-Yates Shuffle
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            (_cards[i], _cards[j]) = (_cards[j], _cards[i]);
        }
    }

    public Card Draw()
    {
        if (_cards.Count == 0)
            Reset(); // Falls das Deck leer wird, einfach neu mischen.

        var card = _cards[^1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }
}
