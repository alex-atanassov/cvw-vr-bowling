using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    void Start()
    {
        Physics.IgnoreLayerCollision(6,7);
        Physics.IgnoreLayerCollision(7,8);
    }
}
