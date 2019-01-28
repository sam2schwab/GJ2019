using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float fuseTime;
    public GameObject explosion;
    public GameObject Bomb_bullet;
    public float tickingDelay;

    private float detonationTime;
    private AudioSource tickingSound;
    private bool isTicking = false;
    private float tickingTime;

    // Start is called before the first frame update
    void Start()
    {
        detonationTime = Time.time + fuseTime;
        tickingTime = Time.time + tickingDelay;
        tickingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > detonationTime) Explode();
        if (!isTicking && Time.time > tickingTime)
        {
            tickingSound.Play();
            isTicking = true;
        }
    }

    private void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(Bomb_bullet, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
