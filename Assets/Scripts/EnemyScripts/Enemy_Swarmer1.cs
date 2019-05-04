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
    Vector3 nextTarget;
    Vector3 lastTarget;
    [Range(0f, 1f)]
    public float perc;
    public float strafeLerpPos;
    public float strafeSpeed;

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

        //soul = GetComponentInParent<Enemy_SwarmSoul>();
        strafeLerpPos = 0;
        lastTarget = transform.localPosition;
        int targetInt = Random.Range(0, 35);
        nextTarget = soul.points[targetInt];
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        strafeLerpPos += strafeSpeed * Time.deltaTime;
        perc = Mathf.Sin(strafeLerpPos);
        transform.localPosition = Vector3.LerpUnclamped(lastTarget, nextTarget, perc);

       
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