using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float jumpForce = 12;
    [SerializeField] private float doubleJumpForce = 8;
    [SerializeField] private float dropForce = 6;
    private Rigidbody2D rb;
    private bool grounded;
    private bool dropped;
    private bool doubleJumped;
    [SerializeField] private GameObject droppedPrefab;
    [SerializeField] private GameObject doubleJumpPrefab;
    [SerializeField] private GameObject missilePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !grounded)
        {
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            dropped = false;
            doubleJumped = false;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Instantiate(doubleJumpPrefab, transform.position, doubleJumpPrefab.transform.rotation);
        }
        else if (Input.GetButtonDown("Jump") && !grounded && !doubleJumped)
        {
            doubleJumped = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            Instantiate(doubleJumpPrefab, transform.position, doubleJumpPrefab.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.S) && !grounded && !dropped)
        {
            dropped = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.down * dropForce, ForceMode2D.Impulse);
            Instantiate(droppedPrefab, transform.position, droppedPrefab.transform.rotation);
        }

        // DEMO TEST
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Instantiate(missilePrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(-5, 0, 10), missilePrefab.transform.rotation);
        }
    }

    public void SetGrounded(bool given)
    {
        grounded = given;
    }
}
 