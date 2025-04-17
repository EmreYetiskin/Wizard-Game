using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissileBehavior : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
