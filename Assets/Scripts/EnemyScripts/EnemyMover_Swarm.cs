using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover_Swarm : MonoBehaviour
{
    private GameObject home;
    private Vector3 target;

    public bool activated = false;
    public float activationTime = 1f;
    float creationTime;
    public float speedBeforeAct = 4f;
    public float speedAfterAct = 4f;

    public float cycleTime = 4f;
    [Range(0, 90)]
    public float maxAngle = 90f;
    float degreePerSec;
    float activationFacing;
    float upperBound;
    float lowerBound;
    float facing;
    int dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        home = GameManager.Instance.home;
        target = home.transform.position;
        target.y = 8;

        creationTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        print("0.DegreePerFrame = " + degreePerSec * Time.deltaTime);
        upperBound = activationFacing + maxAngle;
        lowerBound = activationFacing - maxAngle;

        if (!activated)
        {
            transform.position += transform.forward * speedBeforeAct * Time.deltaTime;   //moving part

            if (Time.time - creationTime >= activationTime)
            {
                Activate();
            }
        }
            
        if (activated)
        {
            print("1.degreePerFrame = " + degreePerSec * Time.deltaTime);
            transform.position += transform.forward * speedAfterAct * Time.deltaTime;   //moving part

            if (facing > upperBound || facing < lowerBound)
            {
                dir *= -1;
                print("2." + facing + " > " + upperBound + " OR < " + lowerBound);
            }

            degreePerSec = ((2 * maxAngle) / cycleTime) * dir;
            transform.Rotate(Vector3.up, degreePerSec * Time.deltaTime);
            facing += degreePerSec * Time.deltaTime;
            print("3.facing = " + facing + ", and degreePerFrame = " + degreePerSec * Time.deltaTime);

            // Arrivé à destination
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                GameManager.Instance.Explosion(); //Play audio sound
                //GameManager.Instance.DamageHome(damage);        MUST BE UNCOMMENTED!!!!
                Destroy(gameObject);
            }
        }
        
    }

    void Activate()
    {
        transform.LookAt(target);
        activationFacing = transform.eulerAngles.y;
        facing = activationFacing;
        activated = true;
    }
}
