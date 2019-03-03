using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    LineRenderer laserSight;

    // Start is called before the first frame update
    void Start()
    {
        laserSight = GetComponent<LineRenderer>();

        if (!GameManager.Instance.laserSight)
        {
            laserSight.enabled = false;
            this.enabled = false;
        }
        else if (GameManager.Instance.laserSight)
        {
            laserSight.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        laserSight.SetPosition(0, transform.position);
        laserSight.SetPosition(1, transform.position + transform.up * 200);
    }
}
