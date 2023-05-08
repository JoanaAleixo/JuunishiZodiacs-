using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZoneDebugger : MonoBehaviour
{
    [SerializeField] private ScriptablePlace activePlace;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < activePlace.DislocationStr.Length; i++)
        {
            Gizmos.color = Color.red;
            Vector3 finalPos = Camera.main.WorldToScreenPoint(activePlace.DislocationStr[i].ButtonPosition);
            finalPos.z = -10;
            Gizmos.DrawWireSphere(finalPos, 25f);
        }
    }
}
