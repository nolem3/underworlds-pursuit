using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float jumpForce = 12;
    [SerializeField] private float doubleJumpForce = 8;
    [SerializeField] private float dropForce = 6;
    [SerializeField] private float dashDistance = 2;
    [SerializeField] private float dashSpeed = 5;
    private Rigidbody2D rb;
    private bool jumpInput;
    private bool grounded;
    private bool doubleJumped;
    private bool dropped;
    private bool dashInput;
    private bool dashOver = true;
    private bool canDash = true;
    private Vector3 toDashTo;
    private float moveInputX;
    private Vector3 movementDirection = new Vector3(-1.0f, 0f, 0f).normalized;
    [SerializeField] private GameObject droppedPrefab;
    [SerializeField] private GameObject doubleJumpPrefab;
    [SerializeField] private GameObject missilePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        MoveCheck();
        JumpCheck();
        dashCheck();

        // Drop Ability 
        /* if (Input.GetKeyDown(KeyCode.S) && !grounded && !dropped)
        {
            dropped = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
            Instantiate(droppedPrefab, transform.position, droppedPrefab.transform.rotation);
        } */
    }

    private void MoveCheck()
    {
        transform.Translate(moveInputX * moveSpeed * Time.deltaTime * transform.right);
    }

    private void JumpCheck()
    {
        jumpInput = Input.GetButtonDown("Jump");
        if (!jumpInput) return;
        if (grounded)
        {
            grounded = false;
            dropped = false;
            doubleJumped = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Instantiate(doubleJumpPrefab, transform.position, doubleJumpPrefab.transform.rotation);
        }
        else if (!grounded && !doubleJumped)
        {
            doubleJumped = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            Instantiate(doubleJumpPrefab, transform.position, doubleJumpPrefab.transform.rotation);
        }
    }

    public void SetGrounded(bool given)
    {
        grounded = given;
        if (!given)
        {
            doubleJumped = given;
        } 
    }

    public float CurrentMoveInput()
    {
        return moveInputX;
    }

    //dash in straight line
    private void dashCheck()
    {
        Vector3 direction = new Vector3(moveInputX, 0f, 0f).normalized;
        if(direction != new Vector3(0f, 0f, 0f).normalized)
        {
            movementDirection = new Vector3(moveInputX, 0f, 0f).normalized;
        }
        dashInput = Input.GetKeyDown(KeyCode.C);

        if (!dashInput && canDash){
            return;
        }
        //if can dash set a target position to dash to. set can dash to false
        //check if target position has been hit, if it has set can dash to true.
        // when can you dash? if not dashed since hitting the ground or dash being over. 
        // standing still on a platform, dashing while on a platform can't dash if(!dashed && grounded) set dashed to true and to dash to
        // in the air can dash, can't dash until grounded if(!dashed && !grounded) set dashed to true 
        //when is the dash over? when the player hits the target position
        if(canDash)
        {
            dashOver = false;
            canDash = false;

            rb.gravityScale = 0f;
            toDashTo = transform.position + (dashDistance * movementDirection);
        }
        else if(!dashOver)
        {
            Debug.Log(transform.position + " Position");
            Debug.Log(toDashTo + " To dash to");
            transform.position = Vector3.MoveTowards(transform.position, toDashTo, dashSpeed * Time.deltaTime);
            if(Vector3.Distance(toDashTo, transform.position) < .01f)
            {
                rb.gravityScale = 2.0f;
                dashOver = true;
            }
        }
        else if(grounded && dashOver)
        {
            canDash = true;
        }
    }
}
 