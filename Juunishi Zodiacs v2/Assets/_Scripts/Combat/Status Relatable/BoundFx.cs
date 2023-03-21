using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundFx : StatusFx
{
    private int damagePerStack;

    public int DamagePerStack { get => damagePerStack; set => damagePerStack = value; }

    public BoundFx(bool hasEndRoundFx, bool loseStackOnEndRound, bool isBuff, int damage) : base(hasEndRoundFx, loseStackOnEndRound, isBuff)
    {
        DamagePerStack = damage;
    }

    public override void ApplyEffect(PlayableCaracter chara)
    {
        chara.currentStatus.TryGetValue(this, out int value);
        chara.TakeDamage(DamagePerStack * value, DAMAGETYPE.Physical);
    }
}
