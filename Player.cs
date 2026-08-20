namespace CasinoApp;

public class Player
{
    public string Name { get; }
    public decimal Balance { get; private set; }

    public Player(string name, decimal startingBalance)
    {
        Name = name;
        Balance = startingBalance;
    }

    public bool CanAfford(decimal amount) => amount > 0 && amount <= Balance;

    public void Deduct(decimal amount)
    {
        if (amount < 0 || amount > Balance)
            throw new InvalidOperationException("Ungültiger Einsatzbetrag.");
        Balance -= amount;
    }

    public void Add(decimal amount)
    {
        if (amount < 0)
            throw new InvalidOperationException("Gewinnbetrag kann nicht negativ sein.");
        Balance += amount;
    }

    public bool IsBroke() => Balance <= 0;
}
