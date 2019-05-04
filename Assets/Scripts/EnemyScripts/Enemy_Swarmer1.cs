using UnityEngine;

public class Enemy_Swarmer1 : MonoBehaviour
{
    private Enemy_SwarmSoul soul;
    private GameObject home;
    private Vector3 target;

    public int life;
    public int shield;

    //public float strafeSpeed;
    //public float strafeDistance;
    Vector3 targetA;
    Vector3 targetB;
    int targetInt;
    bool justChangedTarget = false;
    [Range(0f, 1f)]
    public float perc;
    public float strafeLerpPos;
    public float strafeSpeed;

    Quaternion quat;
    float rotPerc;
    public float rotSpeed;

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
        //home = GameManager.Instance.home;
        //target = home.transform.position;
        //transform.LookAt(target);
        //target.y = 8;

        soul = GetComponentInParent<Enemy_SwarmSoul>();
        strafeLerpPos = 0;
        strafeSpeed += Random.Range(-30f, 30f);
        targetA = transform.localPosition;
        targetInt = Random.Range(0, soul.nbOfPoints-1);
        targetB = soul.points[targetInt];
        quat = new Quaternion();
        quat = Quaternion.LookRotation(targetB - targetA);
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        strafeLerpPos += strafeSpeed * Time.deltaTime;
        rotPerc += rotSpeed * Time.deltaTime;
        perc = (Mathf.Sin(Mathf.Deg2Rad * strafeLerpPos)+1)/2;
        transform.localPosition = Vector3.Lerp(targetA, targetB, perc);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, quat, rotPerc);

        if (perc > 0.45f && perc < 0.55f) justChangedTarget = false;

        if (!justChangedTarget)
        {
            if (perc > 0.999f)
            {
                targetA = NewTarget(targetInt);
                quat = Quaternion.LookRotation(targetA - targetB);
                rotPerc = 0f;
                justChangedTarget = true;
                print("change target A. Target is " + targetInt);
            }

            if (perc < 0.001f)
            {
                targetB = NewTarget(targetInt);
                quat = Quaternion.LookRotation(targetB - targetA);
                rotPerc = 0f;
                justChangedTarget = true;
                print("change target B. Target is " + targetInt);
            }
        }



    }

    private Vector3 NewTarget(int tInt)
    {
        int min = soul.nbOfPoints / 4;
        int max = min*3;
        targetInt = (tInt + Random.Range(min, max)) % soul.nbOfPoints;
        return soul.points[targetInt];
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
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}