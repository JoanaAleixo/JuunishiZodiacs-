using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CureDebuffFx : StatusFx
{
    public CureDebuffFx(bool hasEndRoundFx, bool loseStackOnEndRound, bool isBuff) : base(hasEndRoundFx, loseStackOnEndRound, isBuff)
    {
    }

    public override void ApplyEffect(BaseStats chara)
    {
        foreach (var status in chara.currentStatus.ToList())
        {
            if (!status.Key.IsBuff || status.Key is CureDebuffFx)
            {
                chara.currentStatus[status.Key] -= 1;
            }
        }
        CombatUiManager.uiInstance.RepresentStatusFx(chara);
    }

}
