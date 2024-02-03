using Robust.Shared.Utility;
using System.Text.RegularExpressions;
using Content.Server.Speech;

namespace Pirata.Sotaque.Presidencial;

sealed class SotaquePresidencial : AccentEngine
{
    public override void Initialize()
    {
        SubscribeLocalEvent<SotaquePresidencialComponent, AccentGetEvent>(OnAccent);
    }
    override protected string path { get; set; } = "";
    private void OnAccent(EntityUid uid, SotaquePresidencialComponent component, AccentGetEvent args)
    {
        args.Message = Take(args.Message);
    }
    protected override string Parse(string message)
    {
        List<Regex> ogs = new List<Regex>(){
            new Regex(@"s"),
            new Regex(@"S"),
            new Regex(@"c([eiEI])"),
            new Regex(@"C([eiEI])"),
            new Regex(@"([mn])($|[bcdfgjklpqrstvxzBCDFGJKLPQRSTVXZ\s\W])"),
            new Regex(@"([MN])($|[bcdfgjklpqrstvxzBCDFGJKLPQRSTVXZ\s\W])"),
            new Regex(@"([BCDFGJKLPQSTVbcdfjklpqstv])l"),
            new Regex(@"([BCDFGJKLPQSTVbcdfjklpqstv])L"),
            new Regex(@"f"),
            new Regex(@"F"),
            new Regex(@"ç"),
            new Regex(@"Ç"),
            new Regex(@"(o)[rR]($|[\W\s])"),
            new Regex(@"(O)[rR]($|[\W\s])")
        };
        List<string> mods = new List<string>() {
            "ƒ",
            "Ƒ",
            "ƒ$1",
            "Ƒ$1",
            "$1ƒ$2",
            "$1Ƒ$2",
            "$1r",
            "$1R",
            "ƒ",
            "Ƒ",
            "ƒ",
            "Ƒ",
            "ô$2",
            "Ô$2"
        };

        List<string>moddedToken = new List<string>();
        string[] tokens = message.Split(' ');
        foreach(string token in tokens) {
            bool added = false;
            string currentToken = token;
            for(int i = 0; i < ogs.Count; i++) {
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
