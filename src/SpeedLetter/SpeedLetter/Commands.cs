using System;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace SpeedLetter
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Help()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("Commands");
            builder.AddField("!help", "Returns this message", false);
            builder.AddField("!start", "Starts the current game", false);
            builder.AddField("!setup", "Setups a new game", false);
            builder.AddField("!difficulty", "Sets the difficulty for current game", false);
            builder.AddField("For more documentation and source code visit", "https://espefalt.se/speedletter/documentation", false);

            builder.WithColor(Color.Orange);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("setup")]
        public async Task Setup()
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle("A new game is about to start, react to join!");
            builder.AddField("!start", "To start the game", false);
            builder.AddField("!difficulty", "To change the difficulty", false);
            builder.AddField("!rounds", "To change the amount of rounds", false);

            builder.WithColor(Color.Green);

            await ReplyAsync("", false, builder.Build());
        }

        [Command("start")]
        public async Task Start()
        {
            await ReplyAsync("10 seconds to start");
            await Task.Delay(7000);
            await ReplyAsync("3 seconds to start");
            await Task.Delay(1000);
            await ReplyAsync("2 seconds to start");
            await Task.Delay(1000);
            await ReplyAsync("1 seconds to start");
            await Task.Delay(1000);
            await ReplyAsync("The game has started!");
            Program.game.IsActive = true;
            Random random = new();
            Program.game.CurrentLetter = Char.ToString((char)random.Next(97, 122));
            await ReplyAsync(Program.game.CurrentLetter);
        }
    }
}
