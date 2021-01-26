using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


public class PingModule : BaseCommandModule
{
    [Command("пинг")]
    public async Task GreetCommand(CommandContext ctx)
    {
        await ctx.TriggerTypingAsync();

        var msg = await new DiscordMessageBuilder()
            .WithContent($"Понг! Пинг бота: {ctx.Client.Ping}мс")
            .WithReply(ctx.Message.Id)
            .SendAsync(ctx.Channel);

    }

    [Command("рандом"), Description("Получить рандомное число из двух чисел"), Aliases("RNG", "random")]

    public async Task RNGCommand(CommandContext ctx, [Description("Минимальное число")] int min, [Description("Максимальное число")] int max)
    {

        await ctx.TriggerTypingAsync();

        var embed = new DiscordEmbedBuilder();

        var random = new Random();

        embed.Color = DiscordColor.HotPink;
        embed.AddField(name: "Ваши числа:", value: $"{min}, {max}");
        embed.AddField(name: "Сгенерировано число:", value: $"{random.Next(min, max)}");
        embed.Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail{ Url = "https://www.clipartmax.com/png/full/100-1003417_random-dice-icon-png.png" };
        embed.Timestamp = ctx.Message.Timestamp;


        var msg = await new DiscordMessageBuilder()
        .WithEmbed(embed: embed)
        .WithReply(ctx.Message.Id)
        .SendAsync(ctx.Channel);
    }

    [Command("user")]
    public async Task UserCommand(CommandContext ctx, DiscordMember member)
    {

        await ctx.TriggerTypingAsync();

        var Embed = new DiscordEmbedBuilder();

        Embed.Color = member.Color;
        Embed.Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = $"{member.AvatarUrl}" };
        Embed.AddField(name: "Юзер:", value: $"{member.Mention}");

        var msg = await new DiscordMessageBuilder()
        .WithEmbed(embed: Embed)
        .SendAsync(ctx.Channel);

    }

    [Command("user")]
    public async Task User2Command(CommandContext ctx)
    {

        DiscordMember member = ctx.Member;

        await ctx.TriggerTypingAsync();

        var Embed = new DiscordEmbedBuilder();

        Embed.Color = member.Color;
        Embed.Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = $"{member.AvatarUrl}" };
        Embed.Footer = new DiscordEmbedBuilder.EmbedFooter { Text = "тест.." };
        Embed.AddField(name: "Юзер:", value: $"{member.Mention}({member.DisplayName})");
        Embed.AddField(name: "Айди:", value: $"{member.Id}");
        Embed.AddField(name: "Юзер:", value: $"{member.Roles}");

        var msg = await new DiscordMessageBuilder()
        .WithEmbed(embed: Embed)
        .SendAsync(ctx.Channel);

    }

}
    

    

