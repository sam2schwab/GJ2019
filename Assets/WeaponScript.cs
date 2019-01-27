using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public HardPoint emissionPoint;
    public GameObject shot;
    private float nextFire = 0.0f;
    public float fireRate;
    public AudioClip shotSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void Shoot()
    {
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, transform.position, transform.rotation);
            GameManager.Instance.PlaySound(shotSound);
        }

    }
}
