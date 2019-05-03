using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShieldScript : MonoBehaviour
{
    [Range(1, 36)]
    int points = 36;
    [Range(0, 10)]
    public float xradius = 5;
    [Range(0, 10)]
    public float yradius = 5;
    [Range(0,360)]
    public float shieldAngle = 180f;

    LineRenderer line;

    

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.loop = false;
    }

    private void Update()
    {
        DrawShield();
        CheckForBullets();
    }

    void DrawShield()
    {
        float x;
        float y = 0f;
        float z;

        points = (int) Mathf.Round(shieldAngle / 10);
        line.positionCount = points + 1;
        float angle = -shieldAngle/2;

        for (int i = 0; i < points + 1; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, z));

            angle += (shieldAngle / points);
        }
    }
    
    void CheckForBullets()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, yradius); // the layer mask should look for projectiles

        for (int i = 0;  i < hitColliders.Length; i++)
        {
            if (hitColliders[i].CompareTag("Bullets"))
            {
                Transform bullet = hitColliders[i].transform;
                Vector3 vecToHitPoint = bullet.position - transform.position;

                float dotValue = Vector3.Dot(vecToHitPoint.normalized, transform.forward);// vector from ship position to bullet.  Si l'angle <= shieldAngle, alors on detruit le proj.
                float angle = Mathf.Rad2Deg * Mathf.Acos(dotValue);

                if (angle <= shieldAngle / 2)
                {
                    hitColliders[i].SendMessage("Disapear");
                }

            }
        }
    }
}

