using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    public int life;
    public int shield;
    public int speed;
    public int damage;
    private GameObject home;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        home = GameManager.Instance.home;
        target = home.transform.position;
        target.y = 20;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            GameManager.Instance.Explosion(); //Play audio sound
            GameManager.Instance.DamageHome(damage); 
            GetWrecked();
        }
    }

    private void GetWrecked()
    {
        Destroy(gameObject);
    }

    

}