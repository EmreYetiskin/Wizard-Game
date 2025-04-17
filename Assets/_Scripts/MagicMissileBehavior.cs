using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float force;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(Vector3.forward*force,ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
