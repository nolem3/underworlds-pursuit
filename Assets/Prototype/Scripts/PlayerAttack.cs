using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject prefab;
    private Vector3 mousePos;
    private Vector3 aimPos;
    private Quaternion rot;
    private float cooldown;

    void Start()
    {
        
    }

    void Update()
    {
        // firePoint.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward);
        // firePoint.LookAt();

        /*Quaternion rotation = Quaternion.LookRotation(
            Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position,
            firePoint.TransformDirection(Vector3.up)
        );
        firePoint.rotation = new Quaternion(0, 0, rotation.z, rotation.w);*/
        /*rot = Quaternion.LookRotation(Camera.main.WorldToScreenPoint(Input.mousePosition) - firePoint.position);
        rot.x = 0;
        rot.y = 0;
        firePoint.rotation = rot;*/

        /*mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        aimPos = mousePos;
        aimPos.z = firePoint.position.z;
        firePoint.LookAt(aimPos);
        firePoint.rotation = Quaternion.Euler(0, 0, firePoint.rotation.z);*/

        /*Vector3 shootDirection;
        shootDirection = Input.mousePosition;
        shootDirection.z = 0.0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - firePoint.position;
        firePoint.rotation = Quaternion.Euler(shootDirection);*/

        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && cooldown <= 0)
        {
            Instantiate(prefab, firePoint.position, firePoint.rotation);
            cooldown = 0.2f;
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("Mouse pos " + Camera.main.ScreenToWorldPoint(Input.mousePosition) + " ; firePointPos " + firePoint.position + " ; direction:" + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position));
        Utils.LookAt2D(firePoint, Camera.main.ScreenToWorldPoint(Input.mousePosition), true);
    }
}
