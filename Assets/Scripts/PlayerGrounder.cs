using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrounder : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;

    private void Start()
    {
        if (playerMove == null)
        {
            playerMove = GameObject.FindFirstObjectByType<PlayerMove>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment")) playerMove.SetGrounded(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment")) playerMove.SetGrounded(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment")) playerMove.SetGrounded(false);
    }
}
