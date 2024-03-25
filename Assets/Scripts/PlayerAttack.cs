using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject shootEffect;
    [SerializeField] private float timeBetweenShots = 0.3f;
    [SerializeField] private AudioSource gunShotSound;
    private Transform playerTransform;
    private Vector2 aimDirection;
    private float cooldown;
    private bool isSpriteFlipped = false;

    [SerializeField] private float projectileSpeed = 7.0f;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")
                                        .GetComponent<Transform>();
    }

    void Update()
    {
        Aim();
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && cooldown <= 0)
        {
            Shoot();
            cooldown = timeBetweenShots;
        }
    }
    
    private void Aim()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        aimDirection = (mousePosition - transform.position).normalized;

        float armAngle;
        if (mousePosition.x < transform.position.x)
        {
            if (isSpriteFlipped) FlipSprite(); // if facing right, face left
            armAngle = Vector2.SignedAngle(Vector2.left, aimDirection);
        }
        else
        {
            if (!isSpriteFlipped) FlipSprite(); // if facing left, face right
            armAngle = Vector2.SignedAngle(Vector2.right, aimDirection);
        }
        transform.rotation = Quaternion.Euler(0, 0, armAngle);
    }

    private void FlipSprite()
    {
        Vector3 newScale = new(-playerTransform.localScale.x, 
                            playerTransform.localScale.y, 
                            playerTransform.localScale.z);
        playerTransform.localScale = newScale;
        isSpriteFlipped = !isSpriteFlipped;
    }
    
    private void Shoot()
    {
        Vector3 bulletPosition = new(transform.position.x + aimDirection.x, 
                                    transform.position.y + aimDirection.y, 
                                    transform.position.z);
        float rotationOffset = isSpriteFlipped ? 180 : 0;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationOffset);

        GameObject projectile = Instantiate(bullet, bulletPosition, bulletRotation);
        gunShotSound.Play();
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        if(projectileRb != null)
        {
            projectileRb.velocity = new Vector2(aimDirection.x, aimDirection.y) * projectileSpeed;
        }
        if (shootEffect != null) Instantiate(shootEffect, transform.position, bulletRotation);
    }
}
