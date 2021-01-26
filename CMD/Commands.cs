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


        var msg = new DiscordMessageBuilder()
        .WithEmbed(embed: embed)
        .SendAsync(ctx.Channel);


        
    }

    [Command("user"), Description("Показывает информацию об определенном юзере"), Aliases("юзеринфо", "профиль", "profile", "userinfo", "юзер")]
    public async Task UserCommand(CommandContext ctx, [Description("Юзер")] DiscordMember member)
    {

        await ctx.TriggerTypingAsync();

        var Embed = new DiscordEmbedBuilder();

        if (member.IsOwner)
            Embed.Description += "Данный участник является владельцем сервера!\n";
        if (member.IsBot)
            Embed.Description += "Данный участник является ботом!\n";
        if (member.IsCurrent)
            Embed.Description += "Данный участник является самим ботом, через который вы смотрите юзеринфо\n";


        Embed.Timestamp = ctx.Message.Timestamp;
        Embed.Color = member.Color;
        Embed.Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = $"{member.AvatarUrl}" };
        Embed.AddField(name: "Имя юзера:", value: $"{member.Mention}({member.DisplayName})");
        Embed.Footer = new DiscordEmbedBuilder.EmbedFooter { Text = $"ИД юзера: {member.Id}", IconUrl = $"{member.AvatarUrl}" };
        Embed.AddField(name: "Дата создания юзера:", value: $"{member.CreationTimestamp}");
        Embed.AddField(name: "Дата захода юзера на сервер:", value: $"{member.JoinedAt}");



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

        if (member.IsOwner)
            Embed.Description += "Данный участник является владельцем сервера!\n";
        if (member.IsBot)
            Embed.Description += "Данный участник является ботом!\n";


        Embed.Description = "";
        Embed.Color = member.Color;
        Embed.Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = $"{member.AvatarUrl}" };
        Embed.AddField(name: "Имя юзера:", value: $"{member.Mention}({member.DisplayName})");
        Embed.Footer = new DiscordEmbedBuilder.EmbedFooter { Text = $"ИД юзера: {member.Id}", IconUrl = $"{member.AvatarUrl}" };
        Embed.AddField(name: "Дата создания юзера:", value: $"{member.CreationTimestamp}");
        Embed.AddField(name: "Дата захода юзера на сервер:", value: $"{member.JoinedAt}");
        ;
        
        

        var msg = await new DiscordMessageBuilder()
        .WithEmbed(embed: Embed)
        .SendAsync(ctx.Channel);

    }

    [RequireGuild]
    [Command("сервер")]
    public async Task ServerCommand(CommandContext ctx)
    {
        DiscordGuild guild = ctx.Guild;

        var embed = new DiscordEmbedBuilder();

      

        embed.Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = guild.IconUrl };
        embed.AddField(name: "Название сервера:", value: guild.Name);
        embed.AddField(name: "Владелец сервера:", value: guild.Owner.Mention);
        embed.AddField(name: "Дата создания сервера", value: $"{guild.CreationTimestamp}");
        embed.AddField(name: "Количество участников на сервере:", value: $"{guild.MemberCount}");

        var msg = await new DiscordMessageBuilder()
        .WithEmbed(embed: embed)
        .SendAsync(ctx.Channel);
    }

}
    

    

