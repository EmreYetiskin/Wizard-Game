using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private float forceAmount;

    private Transform cameraTransform;

    private void Start()
    {
        
        PlayerInput.Instance.OnBasicAttackAction += PlayerInput_OnBasicAttackAction;
    }
    private void Update()
    {
        cameraTransform = Camera.main.transform;
    }
    private void PlayerInput_OnBasicAttackAction(object sender, System.EventArgs e)
    {
        GameObject spawnedBullet = ObjectPoolManager.SpawnObject(bullet, spawnPos.position, Quaternion.identity);
        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();
        rb.velocity=Camera.main.transform.forward*forceAmount;
    }
}
