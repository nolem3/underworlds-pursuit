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
    [SerializeField] private float dashForce = 5;
    private Rigidbody2D rb;
    private bool jumpInput;
    private float moveInput;
    private bool grounded;
    private bool doubleJumped;
    private bool dropped;
    private bool dashInput;
    private bool canDash;
    [SerializeField] private GameObject droppedPrefab;
    [SerializeField] private GameObject doubleJumpPrefab;
    [SerializeField] private GameObject missilePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        moveInput = Input.GetAxisRaw("Horizontal");
        transform.Translate(moveInput * moveSpeed * Time.deltaTime * transform.right);
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
            canDash = !given;
        } 
    }

    public float CurrentMoveInput()
    {
        return moveInput;
    }

    //dash in straight line
    private void dashCheck()
    {
        Debug.Log("dash initiated");
        dashInput = Input.GetKeyDown("c");
        if (!dashInput){
            return;
        }
        if (canDash)
        {
            canDash = false;
            Vector2 movementDirection = new Vector2(moveInput, 0f).normalized;
            rb.AddForce(movementDirection * dashForce, ForceMode2D.Impulse);
            StartCoroutine("DisableGravity");
        }
    }

    IEnumerator DisableGravity()
    {
        rb.gravityScale = 0f;

        yield return new WaitForSeconds(.5f);

        rb.gravityScale = 2f;
    }
}
 