using Robust.Shared.Utility;
using System.Text.RegularExpressions;
using Content.Server.Speech;

namespace Pirata.Sotaque.Interior;

sealed class SotaqueSertanejo : AccentEngine
{
    public override void Initialize()
    {
        SubscribeLocalEvent<SotaqueInteriorComponent, AccentGetEvent>(OnAccent);
        getPath();
    }
    override protected string path { get; set; } = "/Prototypes/EstacaoPirata/Sotaques/Interior";
    private void OnAccent(EntityUid uid, SotaqueInteriorComponent component, AccentGetEvent args)
    {
        args.Message = this.Take(args.Message);
    }
    override protected string Parse(string message) {
        Regex[] ogs = {
            new Regex(@"ão([\W\s]|$)"),
            new Regex(@"ÃO([\W\s]|$)"),
            new Regex(@"e([\W\s]|$)"),
            new Regex(@"E([\W\s]|$)"),
            new Regex(@"ou($|[\s\W])"),
            new Regex(@"OU($|[\s\W])"),
            new Regex(@"O([\W\s]|$)"),
            new Regex(@"o([\W\s]|$)"),
            new Regex(@"([AIUaiu])[rR]([\W\s]|$)"),
            new Regex(@"e[rR]([\W\s]|$)"),
            new Regex(@"E[rR]([\W\s]|$)"),
            new Regex(@"o[rR]([\W\s]|$)"),
            new Regex(@"O[rR]([\W\s]|$)"),
        };
        String[] mods = {
            "aum$1",
            "AUM$1",
            "i$1",
            "I$1",
            "ô$1",
            "Ô$1",
            "U$1",
            "u$1",
            "$1$2",
            "ê$1",
            "Ê$1",
            "ô$1",
            "Ô$1"
        };
        List<string>moddedToken = new List<string>();
        string[] tokens = message.Split(' ');
        foreach(string token in tokens) {
            bool added = false;
            string currentToken = token;
            for(int i = 0; i < ogs.Length; i++) {
                if(ogs[i].IsMatch(token)) {
                    currentToken = ogs[i].Replace(currentToken, mods[i]);
                    added = true;
                }
            }
            if(!added) {
                moddedToken.Add(token);
            } else {
                moddedToken.Add(currentToken);
            }
                added = false;
        }
        string modded = string.Join(" ", moddedToken);
        modded = char.ToUpper(modded[0]) + modded.Substring(1);
        return modded;
    }
}
