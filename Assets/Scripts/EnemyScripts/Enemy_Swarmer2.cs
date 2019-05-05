using UnityEngine;

public class Enemy_Swarmer2 : MonoBehaviour
{
    private Enemy_SwarmSoul soul;
    private GameObject home;
    private Vector3 target;
    private Transform anchor;

    public int life;
    public int shield;

    private Vector3 origin;
    [Range(0f, 1f)]
    public float perc = 0;
    public float xRadius;
    public float yRadius;
    public float yRot;
    public float period;
    public Vector3 dir;
    public int rotDir;

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

        soul = GetComponentInParent<Enemy_SwarmSoul>();
        anchor = transform.parent;

        xRadius = Random.Range(3f, 9f);
        yRadius = Random.Range(3f, 9f);
        //yRot = Random.Range(5f, 20f);
        period = Random.Range(3f, 5f);
        rotDir = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    void OnValidate() // On load and on modification in the inspector
    {
        Move();
    }

    private void Move()
    {
        // Movement : Translation
        if (rotDir == 1) perc += (1/period) * Time.deltaTime;
        else perc -= (1 / period) * Time.deltaTime;
        perc %= 1;

        float x = Mathf.Cos(perc * 2 * Mathf.PI) * xRadius;
        float y = Mathf.Sin(perc * 2 * Mathf.PI) * yRadius;

        Vector3 newPos = new Vector3(x, 0f, y);
        transform.localPosition = newPos;

        // Movement : Rotation
        //anchor.Rotate(transform.up, yRot * Time.deltaTime);
        dir = new Vector3(-y, 0f, x); //tangent
        if(rotDir == 1) transform.localRotation = Quaternion.LookRotation(dir.normalized);
        else transform.localRotation = Quaternion.LookRotation(dir.normalized *-1);
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