using System.Collections.Generic;
using Content.Shared.Storage;
using Robust.Shared.Serialization;
using Robust.Shared.Audio;
using Robust.Shared.GameStates;

namespace Content.Shared.EstacaoPirata.ChaosDice;

[RegisterComponent]
public sealed partial class ChaosDiceComponent : Component
{
    /// <summary>
    /// Lista de eventos que o Chaos Dice pode causar
    /// </summary>
    [DataField("ruleSpawn")]
    public List<EntitySpawnEntry> Rules = new();

}
