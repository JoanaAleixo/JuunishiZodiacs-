using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusFx
{
    private bool hasEndRoundFx;
    private bool loseStackOnEndRound;
    private bool isBuff;

    protected StatusFx(bool hasEndRoundFx, bool loseStackOnEndRound, bool isBuff)
    {
        this.hasEndRoundFx = hasEndRoundFx;
        this.loseStackOnEndRound = loseStackOnEndRound;
        this.isBuff = isBuff;
    }

    public bool HasEndRoundFx { get => hasEndRoundFx; set => hasEndRoundFx = value; }
    public bool LoseStackOnEndRound { get => loseStackOnEndRound; set => loseStackOnEndRound = value; }
    public bool IsBuff { get => isBuff; set => isBuff = value; }

    public abstract void ApplyEffect(BaseStats chara);
}
