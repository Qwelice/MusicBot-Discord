namespace DiscordMusicBot.Services.Interfaces
{
    using DSharpPlus.CommandsNext;
    using System;

    public interface IDisconnectionService
    {
        public Task DisconnectFromVoice(CommandContext ctx, IServiceProvider services);
    }
}
