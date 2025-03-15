using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpforce= 4f;
    [SerializeField] private float radius;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform transformObj;
    private Rigidbody rb;
    private float playerRadius = 0.5f;
    private float playerHeight = 1.8f;
    private bool isWalking;
    private bool canJump;

    private Transform cameraTransform;

    //[SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();

        PlayerInput.Instance.OnJumpAction += PlayerInput_OnJumpAction;
        PlayerInput.Instance.OnBasicAttackAction += PlayerInput_OnBasicAttackAction;
        PlayerInput.Instance.OnSelfHealAction += PlayerInput_OnSelfHealAction;
    }

    private void PlayerInput_OnSelfHealAction(object sender, System.EventArgs e)
    {
        Debug.Log("self healed");
    }

    private void PlayerInput_OnBasicAttackAction(object sender, System.EventArgs e)
    {
        BasicAttack();
    }

    private void PlayerInput_OnJumpAction(object sender, System.EventArgs e)
    {
        HandleJump();
    }

    private void Update()
    {
        HandleMovement();

        //Vakti gelince modelin boyutunu tam olarak bulmak için kullan 
        /*Vector3 sizeVec = skinnedMeshRenderer.bounds.size;
        Debug.Log(sizeVec);*/
    }
    private void HandleMovement()
    {
        Vector2 inputVector = PlayerInput.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        moveDir = moveDir.x * cameraTransform.right.normalized + moveDir.z * cameraTransform.forward.normalized;
        moveDir.y = 0f;

        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * (playerHeight - 1), playerRadius, moveDir, moveDistance);
        //canJump = Physics.CheckSphere(transformObj.transform.position, radius, groundLayerMask);
        canJump = Physics.Raycast(transform.position, Vector3.down);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -0.5f || moveDir.x > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -0.5f || moveDir.z > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {

                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        if (isWalking)
        {
            //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
    private void HandleJump()
    {
        if (canJump)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }
    private void BasicAttack()
    {
        Debug.Log("basic attack pressed");
    }
}