using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;

public class HelpFormatter : DefaultHelpFormatter
{
    public HelpFormatter(CommandContext ctx) : base(ctx) { }

    public override CommandHelpMessage Build()
    {
        EmbedBuilder.Color = DiscordColor.HotPink;

        return base.Build();
    }
}
