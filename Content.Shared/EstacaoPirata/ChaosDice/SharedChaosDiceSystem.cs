using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Content.Shared.Dice;

namespace Content.Shared.EstacaoPirata.ChaosDice;

public abstract class SharedChaosDiceSystem : EntitySystem
{

    public override void Initialize()
    {
        base.Initialize();
    }
    public virtual bool OnInit(EntityUid uid, DiceComponent die, ChaosDiceComponent chaos)
    {
        /*
         Definido em Content.Server.EstacaoPirata.ChaosDice
         */

        return true;
    }

    public virtual void Roll(EntityUid uid, ChaosDiceComponent chaos, DiceComponent die)
    {
            // veja o código do cliente
    }
}
