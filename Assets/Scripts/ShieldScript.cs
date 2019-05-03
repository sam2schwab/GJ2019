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

                //Trouver le point du shield qui correspond à l'angle d'attaque et vérifier que le projectile est sur la ligne du shield
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
                float z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
                Vector3 vect = new Vector3(x, 0, z);
                bool magCheck = vecToHitPoint.magnitude < vect.magnitude + 0.4 && vecToHitPoint.magnitude > vect.magnitude - 0.4;
                Debug.LogFormat("VecToHitPoint.Magnitude = {0}, vect.mag = {1}, magCheck = {2}, angle = {3}", vecToHitPoint.magnitude, vect.magnitude, magCheck, angle);

                // si le projectile frappe le bouclier, détruire le projectile
                if (angle <= shieldAngle / 2 && magCheck)
                {
                    hitColliders[i].SendMessage("Disapear");
                }

            }
        }
    }
}

