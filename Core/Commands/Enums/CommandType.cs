namespace DiscordMusicBot.Commands.Enums
{
    public enum CommandType
    {
        None = 10,
        #region [User commands]
        Play = 20,
        Pause = 30,
        Stop = 40,
        Next = 50,
        Back = 60,
        Join = 70,
        Leave = 80,
        Help = 90,
        Menu = 100,
        #endregion

        #region [Admin commands]
        DefaultTest = 110,
        #endregion
    }
}
