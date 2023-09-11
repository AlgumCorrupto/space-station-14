using System;
using Content.Shared.Dice;
using Content.Shared.Popups;
using JetBrains.Annotations;
using Robust.Shared.Random;
using Content.Shared.Storage;
using Content.Shared.EstacaoPirata.ChaosDice;
using Content.Server.GameTicking;
using System.Linq;
using System.Xml.Schema;

namespace Content.Server.EstacaoPirata.ChaosDice;

[UsedImplicitly]
public sealed class ChaosDiceSystem : SharedChaosDiceSystem {
    [Dependency] private readonly IRobustRandom _random = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedPopupSystem _popup = default!;
    [Dependency] private readonly SharedDiceSystem _shareddice = default!;
    [Dependency] private readonly GameTicker _gameticker = default!;
    private bool _inited = false; 
    public override bool OnInit(EntityUid uid, DiceComponent die, ChaosDiceComponent chaos)
    {
        if (die == null || chaos == null)
            return false;

        die.Sides = chaos.Rules.Count + 1;
        return true;
    }

    public override void Roll(EntityUid uid, ChaosDiceComponent chaos, DiceComponent? die = null)
    {
        if (!Resolve(uid, ref die))
            return;

        if(this._inited == false)
        {
            this._inited = this.OnInit(uid, die, chaos);
        }


        var roll = _random.Next(1, die.Sides + 1);
        var ev = _random.Next(0, die.Sides - 1);
        // var rule = EntitySpawnCollection.GetSpawns(chaos.Rules, _random);

        _shareddice.SetCurrentSide(uid, roll, die);

        foreach (var rule in EntitySpawnCollection.GetSpawns(chaos.Rules, _random))
            System.Console.WriteLine(rule);
        // TODO: consertar essa MERDA
        //if (rule != null)
        //   _gameticker.AddGameRule(rule);

        _popup.PopupEntity(Loc.GetString("dice-component-on-roll-land", ("die", uid), ("currentSide", die.CurrentValue)), uid);
        _audio.PlayPvs(die.Sound, uid);
    }
}
