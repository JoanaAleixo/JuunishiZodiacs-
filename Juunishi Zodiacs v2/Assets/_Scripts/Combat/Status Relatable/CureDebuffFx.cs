using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CureDebuffFx : StatusFx
{
    public CureDebuffFx(bool hasEndRoundFx, bool loseStackOnEndRound, bool isBuff) : base(hasEndRoundFx, loseStackOnEndRound, isBuff)
    {
    }

    public override void ApplyEffect(BaseStats chara)
    {
        foreach (var status in chara.currentStatus)
        {
            if (!status.Key.IsBuff)
            {
                chara.currentStatus[status.Key] -= 1;
            }
        }
    }

}
