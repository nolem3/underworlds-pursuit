using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Color SetAlpha(Color given, float newAlpha)
    {
        return new Color(given.r, given.g, given.b, newAlpha);
    }

    public static Color ChangeAlpha(Color given, float alphaChange)
    {
        return new Color(given.r, given.g, given.b, given.a + alphaChange);
    }


    public static void LookAt2D(Transform from, Vector3 to, bool zeroZ=false)
    {
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*Vector2 firePointPos = new Vector2(firePoint.position.x, firePoint.position.y);
        if (mousePos.x - this.transform.position.x > 0)
        {
            firePoint.transform.position = new Vector2(0.28f + this.transform.position.x, -0.14f + this.transform.position.y);
        }
        else
        {
            firePoint.transform.position = new Vector2(-0.28f + this.transform.position.x, -0.14f + this.transform.position.y);
        }*/
        if (zeroZ) to.z = from.position.z;
        Vector2 lookDir = to - from.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        // from.GetComponent<Rigidbody2D>().rotation = angle;
        from.rotation = Quaternion.Euler(0, 0, angle);
    }
}
