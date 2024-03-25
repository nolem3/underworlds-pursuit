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
    [SerializeField] private float lavaBounceForce = 10;
    [SerializeField] private float dropForce = 6;
    [SerializeField] private float dashDistance = 2;
    [SerializeField] private float dashSpeed = 5;
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private bool unparentSelfWhenJump = true;
    [SerializeField] private bool ignoreGroundedWhenPositiveYVelocty = true;
    [SerializeField] private float doubleTapTime = 0.2f;
    [SerializeField] private float doubleTapThreshold = 0.2f;
    private float doubleTapTimer = -1;
    [SerializeField] private bool lastInputWasRight;
    private float coyoteTimer = 0;
    [SerializeField, Range(0, -100)] private float gravity = -1f;
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
    private Vector3 velocity;
    private float dashTimer = .2f;
    private float currentTime;
    private Vector3 movementDirection = new Vector3(-1.0f, 0f, 0f).normalized;
    [SerializeField] private GameObject droppedPrefab;
    [SerializeField] private GameObject jumpPrefab;
    [SerializeField] private GameObject doubleJumpPrefab;
    [SerializeField] private GameObject dashPrefab;
    [SerializeField] private GameObject doubleJumpIcon;
    [SerializeField] private GameObject dashIcon;
    [SerializeField] private AudioSource jumpSound;

    void Start()
    {
        doubleJumpIcon.SetActive(true);
        dashIcon.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (doubleTapTimer < doubleTapTime && doubleTapTimer >= 0) doubleTapTimer += Time.deltaTime;
        if (coyoteTimer > 0) coyoteTimer -= Time.deltaTime;
        if (dashOver)
        {
            MoveCheck();
            JumpCheck();
        }
        DashCheck();
        transform.Translate(velocity * Time.deltaTime);
        UpdateIcons();
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
        moveInputX = Input.GetAxisRaw("Horizontal");
        velocity.x = moveInputX * moveSpeed;
        doubleTapTimer = -1;
        /*if (lastInputWasRight && moveInputX < -1 * doubleTapThreshold || !lastInputWasRight && moveInputX > doubleTapThreshold)
        {
            doubleTapTimer = -1;
        }
        else if (doubleTapTimer < 0)
        {
            doubleTapTimer = 0;
        }
        if (moveInputX > 0) lastInputWasRight = true;
        else if (moveInputX < 0) lastInputWasRight = false;*/
    }

    private void JumpCheck()
    {
        velocity.y += gravity * Time.deltaTime;
        jumpInput = Input.GetButtonDown("Jump");
        // if (!jumpInput) return;
        if (grounded || coyoteTimer > 0)
        {
            if (jumpInput)
            {
                grounded = false;
                coyoteTimer = 0;
                dropped = false;
                doubleJumped = false;
                velocity.y = jumpForce;
                jumpSound.Play();
                if (unparentSelfWhenJump) transform.parent = null;
                // rb.velocity = Vector2.zero;
                // rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                // Instantiate(jumpPrefab, transform.position, jumpPrefab.transform.rotation);
                //  ^ See if it feels better to only have the particle effect when double jumping,
                //       so you'll know when the double jump is used up.
            }
            else
            {
                velocity.y = 0;
                // moveThisFrame += new Vector3(0, Time.deltaTime * gravity, 0);
            }
        }
        else if (!(grounded || coyoteTimer > 0) && !doubleJumped && jumpInput)
        {
            doubleJumped = true;
            //rb.velocity = Vector2.zero;
            //rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            velocity.y = doubleJumpForce;
            jumpSound.Play();
            Instantiate(doubleJumpPrefab, transform.position, doubleJumpPrefab.transform.rotation);
        }
    }

    public void SetGrounded(bool given)
    {
        if (ignoreGroundedWhenPositiveYVelocty && velocity.y > 0) return;
        grounded = given;
        coyoteTimer = 0;
        if (!given)
        {
            coyoteTimer = coyoteTime;
            doubleJumped = given;
        } 
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public void TouchedLava()
    {
        doubleJumped = false;
        canDash = true;
        currentTime = 0;
        velocity.y = lavaBounceForce;
    }

    public float CurrentMoveInput()
    {
        return moveInputX;
    }

    //dash in straight line
    private void DashCheck()
    {
        Vector3 direction = new Vector3(moveInputX, 0f, 0f).normalized;
        if(direction != new Vector3(0f, 0f, 0f).normalized)
        {
            movementDirection = new Vector3(moveInputX, 0f, 0f).normalized;
        }
        dashInput = Input.GetKeyDown(KeyCode.LeftShift) || (doubleTapTimer > 0 && doubleTapTimer < doubleTapThreshold && (moveInputX > doubleTapThreshold && lastInputWasRight || moveInputX < -1 * doubleTapThreshold && !lastInputWasRight));
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
            doubleTapTimer = -1;
            dashOver = false;
            canDash = false;

            velocity.y = 0;
            rb.velocity = Vector3.zero;
            //rb.angularVelocity = 0f;
            rb.gravityScale = 0f;
            currentTime = dashTimer;
            toDashTo = transform.position + (dashDistance * movementDirection);
            
            float rotationOffset = moveInputX > 0 ? 180 : 0;
            Quaternion dashRotation = Quaternion.Euler(0, 0, 
                        dashPrefab.transform.rotation.eulerAngles.z + rotationOffset);
            Instantiate(dashPrefab, transform.position, dashRotation, transform).GetComponent<ParticleSystem>();
        }
        else if(!dashOver)
        {
            transform.position = Vector3.MoveTowards(transform.position, toDashTo, dashSpeed * Time.deltaTime);
            currentTime -= Time.deltaTime;
            if(Vector3.Distance(toDashTo, transform.position) < .01f || currentTime <= 0) // || if the timer = 0
            {
                //rb.gravityScale = 2.0f;
                dashOver = true;
            }
        }
        else if((grounded || coyoteTimer > 0) && dashOver)
        {
            canDash = true;
        }
    }

    private void UpdateIcons()
    {
        doubleJumpIcon.SetActive(!doubleJumped || grounded);
        dashIcon.SetActive(canDash);
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
 