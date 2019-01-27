using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public int damage=1;
    Rigidbody rb;
    float lifeTime = 3.0f;
    float birthTime; 

    void Start()
    {
        birthTime = Time.time;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - birthTime > lifeTime)
        {
            Disapear();
        }
    }

    void Disapear()
    {
        Destroy(gameObject);
    }
}
