using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteractions 
{
    public static float CheckInteraction(ELEMENT caracter, DAMAGETYPE attack) {

        float absorbValue = 0f;
        float blockValue = 0.5f;
        float weaknessValue = 1.5f;

        switch (caracter)
        {
            case ELEMENT.Water:
                if(attack == DAMAGETYPE.Metal)
                {
                    return absorbValue;
                }
                if (attack == DAMAGETYPE.Fire)
                {
                    return blockValue;
                }
                if (attack == DAMAGETYPE.Rock)
                {
                    return weaknessValue;
                }
                return 1;
            case ELEMENT.Nature:
                if (attack == DAMAGETYPE.Water)
                {
                    return absorbValue;
                }
                if (attack == DAMAGETYPE.Rock)
                {
                    return blockValue;
                }
                if (attack == DAMAGETYPE.Metal)
                {
                    return weaknessValue;
                }
                return 1;
            case ELEMENT.Fire:
                if (attack == DAMAGETYPE.Nature)
                {
                    return absorbValue;
                }
                if (attack == DAMAGETYPE.Metal)
                {
                    return blockValue;
                }
                if (attack == DAMAGETYPE.Water)
                {
                    return weaknessValue;
                }
                return 1;
            case ELEMENT.Rock:
                if (attack == DAMAGETYPE.Fire)
                {
                    return absorbValue;
                }
                if (attack == DAMAGETYPE.Water)
                {
                    return blockValue;
                }
                if (attack == DAMAGETYPE.Nature)
                {
                    return weaknessValue;
                }
                return 1;
            case ELEMENT.Metal:
                if (attack == DAMAGETYPE.Rock)
                {
                    return absorbValue;
                }
                if (attack == DAMAGETYPE.Nature)
                {
                    return blockValue;
                }
                if (attack == DAMAGETYPE.Fire)
                {
                    return weaknessValue;
                }
                return 1;
            case ELEMENT.NoElement:
                return 1;
            default: return 1;
        }
    } 
}
