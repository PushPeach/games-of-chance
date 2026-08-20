namespace CasinoApp.Games;

public interface IGame
{
    string Name { get; }
    void Play(Player player);
}
