using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StunModifier : Modifiers
{
    [SerializeField] int Quantity;
    [SerializeField] string yes;
    public override void Draw()
    {
        yes = (string)EditorGUILayout.TextField("Stun: ", yes);
    }
}
