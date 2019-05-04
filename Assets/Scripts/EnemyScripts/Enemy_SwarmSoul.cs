using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SwarmSoul : MonoBehaviour
{
    public int nbOfChildren;
    public int damage;
    public int scoreValue;

    public float speed;
    private GameObject home;
    private Vector3 target;

    [Range(3f,9f)]
    public float radius = 4;

    public int nbOfPoints = 36;
    public Vector3[] points;

    private void Awake()
    {
        points = new Vector3[nbOfPoints];
        CalculateSphere();
    }

    // Start is called before the first frame update
    void Start()
    {
        home = GameManager.Instance.home;
        target = home.transform.position;
        target.y = 8;
        transform.LookAt(target);
    }
    private void Update()
    {
        // Gets destroyed?
        nbOfChildren = transform.childCount;

        if (nbOfChildren < 1)
        {
            Die();
        }

        //Sphere of influence
        CalculateSphere();


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

    private void Die()
    {
        GameManager.Instance.AugmentScore(scoreValue);
        Destroy(gameObject);
    }

    private void CalculateSphere()
    {
        float x;
        float y = 0f;
        float z;

        float angle = 0;

        for (int i = 0; i < nbOfPoints; i++)
        {
            x = Mathf.Sin(angle) * radius;
            z = Mathf.Cos(angle) * radius;

            points[i] = new Vector3(x, y, z);

            angle += 2 * Mathf.PI / nbOfPoints;
        }
    }
        
}