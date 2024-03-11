using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShieldReflect : MonoBehaviour
{
    [SerializeField] private GameObject reflectedBullet;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("PlayerBullet"))
        {
            Debug.Log("Shield shot");
            Vector3 bulletPosition = new(other.transform.position.x, 
                                        other.transform.position.y, 
                                        other.transform.position.z);
            GameObject projectile = Instantiate(reflectedBullet, bulletPosition, quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            if(projectileRb != null)
            {
                Rigidbody2D playerBulletRb = other.GetComponent<Rigidbody2D>();
                projectileRb.velocity = new Vector2(-1 * playerBulletRb.velocity.x, -1 * playerBulletRb.velocity.y);
            }
            Destroy(other.gameObject);
        }
    }

    
}
