using UnityEngine;

public class Enemy_Swarmer : MonoBehaviour
{
    //private Enemy_SwarmSoul soul;
    private GameObject home;
    private Vector3 target;

    public int life;
    public int shield;

    public float strafeSpeed;
    public float strafeDistance;
    Vector3 start;
    Vector3 right;
    [Range(-1f, 1f)]
    public float perc;
    public float strafeLerpPos;

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
        strafeLerpPos = Random.Range(0f, 90f);
        start = transform.localPosition;
        right = new Vector3(strafeDistance, 0, 0) + start;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        strafeLerpPos += strafeSpeed * Time.deltaTime;
        perc = Mathf.Sin(strafeLerpPos);
        transform.localPosition = Vector3.LerpUnclamped(start, right, perc);

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