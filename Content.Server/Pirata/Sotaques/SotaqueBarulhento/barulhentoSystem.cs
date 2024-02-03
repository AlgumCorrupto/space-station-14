using Robust.Shared.Utility;
using System.Text.RegularExpressions;
using Content.Server.Speech;

namespace Pirata.Sotaque.Barulhento;

sealed class SotaqueBarulhento : AccentEngine
{
    public override void Initialize()
    {
        SubscribeLocalEvent<SotaqueBarulhentoComponent, AccentGetEvent>(OnAccent);
    }
    override protected string path { get; set; } = "";
    private void OnAccent(EntityUid uid, SotaqueBarulhentoComponent component, AccentGetEvent args)
    {
        args.Message = Take(args.Message);
    }
    override protected string Parse(string message) {
        string moddedMessage = message.ToUpper();
        string[] tokens = moddedMessage.Split(' ');
        string lastToken = tokens[tokens.Length - 1];

        if(lastToken.EndsWith("...")) {
            return moddedMessage;
        } else if(lastToken.EndsWith('.')) {
            lastToken = lastToken.Substring(0, lastToken.Length - 1);
            lastToken = addExclamantion(lastToken);
        } else if(lastToken.EndsWith('?')) {
            int punctIndex = lastToken.IndexOf('?');
            lastToken = lastToken.Substring(0,  punctIndex);
            lastToken = addExclInter(lastToken);
        } else if(lastToken.EndsWith('!')) {
            int punctIndex = lastToken.IndexOf('!');
            lastToken = lastToken.Substring(0,  punctIndex);
            lastToken = addExclamantion(lastToken);
        } else {
            lastToken = addExclamantion(lastToken);
        }

        tokens[tokens.Length - 1] = lastToken;
        moddedMessage = String.Join(" ", tokens);
        return moddedMessage;
    }

    private string addExclInter(string message) {
        int quantity = _random.Next(3, 16);
        string modded = message;
        char[] chars = {'!', '?'};
        for(int i = 0; i < quantity; i++) {
            modded += chars[i % 2];
        }

        return modded;
    }
    private string addExclamantion(string message) {
        int exclQuantity = _random.Next(3, 16);
        string modded = message;
        for(int i = 0; i < exclQuantity; i++) {
            modded += '!';
        }

        return modded;
    }
}

