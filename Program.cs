using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DFMBot
{
   public class Program
    {
        public readonly EventId BotEventId = new EventId(42, "Бот-Ех01");

        public DiscordClient Client { get; set; }
        public CommandsNextExtension Commands { get; set; }

        public static void Main(string[] args)
        {
            var prog = new Program();
            prog.RunBotAsync().GetAwaiter().GetResult();
        }

        public async Task RunBotAsync()
        {
            var cfg = new DiscordConfiguration
            {
                Token = "ODAxMTI0NTU3OTk3OTk4MTAw.YAcHYg.DM6vVSPvCZWaYp36TKdTQ7yLXak",
                TokenType = TokenType.Bot,

                AutoReconnect = true,
            };

            var ccfg = new CommandsNextConfiguration
            {
                StringPrefixes = new[] { "<" },
                EnableMentionPrefix = false,
                EnableDms = true,
            };

            this.Client = new DiscordClient(cfg);
            this.Client.Ready += this.Client_Ready;

            this.Commands = this.Client.UseCommandsNext(ccfg);

            this.Commands.CommandErrored += this.Commands_CommandErrored;

            this.Commands.RegisterCommands<PingModule>();

            this.Commands.SetHelpFormatter<HelpFormatter>();

            await this.Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private async Task Commands_CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            e.Context.Client.Logger.LogError(BotEventId, $"{e.Context.User.Username} попытался выполнить команду '{e.Command?.QualifiedName ?? "<неизвестная команда>"}', но она выдала ошибку: {e.Exception.GetType()}: {e.Exception.Message ?? "<нет сообщения>"}", DateTime.Now);

            if (e.Exception is ArgumentException ex)
            {
                if (e.Command?.QualifiedName == "рандом")
                {
                    await e.Context.Message.RespondAsync("Вы ввели минимальное число больше максимального, или вообще не ввели аргументы!");
                }
                
                else
                {
                    await e.Context.Message.RespondAsync("Вы не ввели аргументы!");
                }
            }


        }

        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            sender.Logger.LogInformation(BotEventId, "Клиент запущен!");

            return Task.CompletedTask;
        }

    }
}
