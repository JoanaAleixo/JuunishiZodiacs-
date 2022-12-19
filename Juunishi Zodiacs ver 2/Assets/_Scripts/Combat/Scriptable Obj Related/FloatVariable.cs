using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Float Variable")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] public float value;
    [SerializeField] public bool resetable = false;
    [SerializeField] public float resetValue;

    private void OnEnable()
    {
        if (resetable)
        {
            value = resetValue;
        }
    }
}
