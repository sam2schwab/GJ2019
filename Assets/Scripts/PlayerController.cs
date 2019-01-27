using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 25;
	public Transform anchorTransform;

	private Vector3 _rotationAxis;
	private Transform _playerTransform;
    private bool _isAnchored = true;
    private float _rotationSpeed;

    //Shooting
    public GameObject shot;
    public Transform noseShotSpawn;
    //public Transform wingShotSpawnL;
    //public Transform wingShotSpawnR;
    public float fireRate = 0.05f;
    private float nextFire = 0.0f;
    private AudioSource shotSound;

    public GameObject explosion;

    // Use this for initialization
    private void Start()
    {
        _playerTransform = gameObject.transform;
        UpdateRotation();
        shotSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	private void Update () {
		var axis = Input.GetAxis("Vertical");
		if (Mathf.Abs(axis - 0f) > 0.01f)
		{
			speed += 5 * axis * Time.deltaTime; 
			var radius = _playerTransform.position - anchorTransform.position;
			UpdateRotationSpeed(radius);
		}
        if (_isAnchored)
        {
	        if (CheckButton("Jump"))
	        {
		        _isAnchored = false;
	        }
	        _playerTransform.RotateAround(anchorTransform.position, _rotationAxis, _rotationSpeed * Time.deltaTime);
        }
        else
        {
	        TryToAnchor();
            _playerTransform.Translate(speed * Vector3.up * Time.deltaTime);
        }

        //Shooting
        if (CheckButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, noseShotSpawn.position, noseShotSpawn.rotation);
            shotSound.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Planets")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            //GameManager.Instance.PlayerCrash();
            GameManager.Instance.isGameOver = true;
        }
        
    }

    private void TryToAnchor()
	{
		foreach (var planet in GameManager.Instance.planets)
		{
			if (planet.transform == anchorTransform) continue;
			var radius = _playerTransform.position - planet.gameObject.transform.position;
			if (radius.magnitude < planet.GravityRadius && Vector3.Angle(_playerTransform.up, radius) - 90 < 1.5)
			{
				anchorTransform = planet.transform;
				UpdateRotation();
				_isAnchored = true;
				return;
			}
		}
	}

	private void UpdateRotation()
	{
		//update rotation axis
		var direction = _playerTransform.up;
		var radius = _playerTransform.position - anchorTransform.position;
		_rotationAxis = Vector3.Cross(radius, direction).y < 0 ? Vector3.down : Vector3.up;

		UpdateRotationSpeed(radius);
		
		//update ship angle
		var modifier = _rotationAxis.y < 0 ? -90 : 90; 
		var angle = Vector3.SignedAngle(direction, radius, Vector3.up) + modifier;
		_playerTransform.Rotate(Vector3.up, angle, Space.World);
	}

	private void UpdateRotationSpeed(Vector3 radius)
	{
//update rotation speed (deg/sec)
		var perimeter = 2 * Mathf.PI * radius.magnitude;
		_rotationSpeed = speed * 360 / perimeter;
	}

	private bool CheckButton(string button)
    {
	    return Input.GetButton(button) && !GameManager.Instance.isGameOver;
    }
}
