using System.Linq;
using System.Text.RegularExpressions;
using Content.Server.Speech;

namespace Pirata.Sotaque.Cebolinha;

sealed class CebolinhaSystem : AccentEngine {
    public override void Initialize()
    {
        SubscribeLocalEvent<SotaquedoCebolinhaComponent, AccentGetEvent>(OnAccent);
    }
    override protected string path { get; set; } = "";
    private void OnAccent(EntityUid uid, SotaquedoCebolinhaComponent component, AccentGetEvent args)
    {
        args.Message = Take(args.Message);
    }

    protected override string Parse(string message)
    {
        char[] original = {'r', 'R'};
        char[] modified = {'l', 'L'};

        string moddedMessage =  "";
        foreach(char charac in message) {
            bool moddedFlag = false;
            for(int i = 0; i < original.Length; i++) {
                if(charac == original[i]) {
                    moddedMessage += modified[i];
                    moddedFlag = true;
                    break;
                }
            }
            if(!moddedFlag) {
                moddedMessage += charac;
            }
        }
        return moddedMessage;
    }
}
