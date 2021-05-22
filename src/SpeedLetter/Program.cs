using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace SpeedLetter
{
    class Program
    {
        private DiscordSocketClient client;
        private CommandService commands;
        private IServiceProvider services;

        private Settings settings;

        public static string rootPath;
        private static string settingsPath;

        public Game game = new();

        static void Main(string[] args)
        {
            rootPath = @"C:\Users\alfre\Desktop\SpeedLetterOld\build\";
            settingsPath = Path.Combine(rootPath, "settings.json");
            new Program().RunBotAsync().GetAwaiter().GetResult();
        }

        public async Task RunBotAsync()
        {
            this.settings = Settings.LoadFromJson(settingsPath);

            client = new DiscordSocketClient();
            commands = new CommandService();
            services = new ServiceCollection()
                .AddSingleton(client)
                .AddSingleton(commands)
                .BuildServiceProvider();

            client.Log += Log;

            await client.SetGameAsync($"{this.settings.CommandPrefix}help");
            await client.SetStatusAsync(UserStatus.Online);

            await RegisterCommandsAsync();

            await client.LoginAsync(TokenType.Bot, this.settings.Token);

            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
        private static void Log(string arg)
        {
            Console.WriteLine(arg);
        }

        public async Task RegisterCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            SocketUserMessage message = arg as SocketUserMessage;
            SocketCommandContext context = new SocketCommandContext(client, message);
            if (message.Author.IsBot) return;
            int argPos = 0;
            if (message.HasStringPrefix(this.settings.CommandPrefix, ref argPos))
            {
                Log($"Command: {message} Executed by {message.Author}");
                IResult result = await commands.ExecuteAsync(context, argPos, services);
                if (!result.IsSuccess)
                {
                    Log($"Error: {result.Error} Reason: {result.ErrorReason}");
                }
            }
            else if (game.IsActive && message.ToString().Length == 1)
            {
                if(message.ToString() == game.CurrentLetter)
                {
                    Log($"Letter: {message} From: {message.Author} Correct: True");
                    Random random = new();
                    this.game.CurrentLetter = Char.ToString((char)random.Next(97, 122));
                    await Task.Delay(2000);
                    await message.Channel.SendMessageAsync(this.game.CurrentLetter);
                }
                else
                {
                    Log($"Letter: {message} From: {message.Author} Correct: False");
                }
            }
        }
    }
}
