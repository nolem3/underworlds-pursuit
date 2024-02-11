using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    private Vector3 mousePos;
    private Vector3 aimPos;
    private Quaternion rot;
    private float cooldown;

    private float projectileSpeed = 7.0f;

    void Start()
    {
        
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && cooldown <= 0)
        {
            shootGun();
            cooldown = 0.2f;
        }
    }
    private void shootGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        GameObject projectile = Instantiate(bullet, transform.position, quaternion.identity);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if(projectileRb != null)
        {
            Vector2 shootDirection = (mousePosition - transform.position).normalized;
            projectileRb.velocity = new Vector2(shootDirection.x, shootDirection.y) * projectileSpeed;
        }
    }
}
