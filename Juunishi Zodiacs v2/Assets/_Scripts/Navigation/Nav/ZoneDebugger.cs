using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZoneDebugger : MonoBehaviour
{
    [SerializeField] private ScriptablePlace activePlace;

    [SerializeField] ScriptableItem _itemInPlace;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < activePlace.DislocationStr.Length; i++)
        {
            Gizmos.color = Color.red;
            Vector3 finalPos = Camera.main.WorldToScreenPoint(activePlace.DislocationStr[i].ButtonPosition);
            finalPos.z = -10;
            Gizmos.DrawWireSphere(finalPos, 25f);
        }

        Gizmos.color = Color.yellow;
        Vector3 itemFinalPos = Camera.main.WorldToScreenPoint(_itemInPlace.ItemPositionInNav);
        itemFinalPos.z = -10;
        Gizmos.DrawWireSphere(itemFinalPos, 25f);
    }

}
