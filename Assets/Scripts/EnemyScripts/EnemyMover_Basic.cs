using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover_Basic : MonoBehaviour
{
    public float speed;
    private GameObject home;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
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
            //GameManager.Instance.DamageHome(damage);       MUST BE UNCOMMENTED!!!!
            Destroy(gameObject); 
        }
    }
}
