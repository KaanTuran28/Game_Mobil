namespace GameMobil.Core
{
    /// <summary>
    /// Oyunun üst seviye akış durumları. GameManager tarafından tutulur ve değiştirilir.
    /// </summary>
    public enum GameState
    {
        Bootstrap,
        Loading,
        MainMenu,
        Playing,
        Paused,
        Win,
        Lose
    }
}
