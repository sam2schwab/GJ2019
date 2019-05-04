using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Basic : MonoBehaviour
{
    public int life;
    public int shield;
    public int damage;
    public int scoreValue;

    public float speed;
    private GameObject home;
    private Vector3 target;

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

        //For movement
        home = GameManager.Instance.home;
        target = home.transform.position;
        target.y = 8;
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // At destination
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            GameManager.Instance.Explosion(); //Play audio sound
            GameManager.Instance.DamageHome(damage);
            Destroy(gameObject);
        }
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