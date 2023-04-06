using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCreator : MonoBehaviour
{
    public BoundFx CreateBoundFx()
    {
        return new BoundFx(true, false, false, 5);
    }
}
