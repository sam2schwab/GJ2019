﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats_Basic : MonoBehaviour
{
    public int life;
    public int shield;

    public int damage;
    
    public int scoreValue;

    //For flashing on hit
    public float flashTime = 0.1f;
    Color originalColor;
    private Material mat;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponentInChildren<MeshRenderer>().material;
        originalColor = mat.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetWrecked()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullets")
        {
            BulletMover bulletScript = other.gameObject.GetComponent<BulletMover>();
            int damage = bulletScript.damage;
            bool isDestroyed = bulletScript.isDestroyed;

            if (isDestroyed) Destroy(other.gameObject);
            GameManager.Instance.EnemyHit();
            FlashRed();

            if (shield > 0)
            {
                if (damage < shield)
                {
                    shield -= damage;
                    damage = 0;
                }
                else
                {
                    damage -= shield;
                    shield = 0;
                }
            }
  
            life -= damage;

            if (life < 1)
            {
                Die();
            }
        }
    }

    void FlashRed()
    {
        mat.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    void ResetColor()
    {
        mat.color = originalColor;
    }

    private void Die()
    {
        //GameManager.Instance.Explosion();
        GameManager.Instance.AugmentScore(scoreValue);
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
        

}