using UnityEngine;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
	public float speed = 25;
	public Transform anchorTransform;

	private Vector3 _rotationAxis;
	private Transform _playerTransform;
    private bool _isAnchored = true;
    private float _rotationSpeed;

    private Renderer[] renderers; //for ScreenWrapping
    bool isWrappingX = false; //for ScreenWrapping
    bool isWrappingY = false; //for ScreenWrapping

    //Collisions


    // Use this for initialization
    private void Start()
    {
        _playerTransform = gameObject.transform;
        UpdateRotation();
        renderers = GetComponentsInChildren<Renderer>(); //for ScreenWrapping
    }
	
	// Update is called once per frame
	private void Update () {
        if (_isAnchored)
        {
	        if (Input.GetButtonDown("Jump"))
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

        ScreenWrap(); //for ScreenWrapping

    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        GameManager.Instance.PlayerCrash();
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
		
		//update rotation speed (deg/sec)
		var perimeter = 2 * Mathf.PI * radius.magnitude;
		_rotationSpeed = speed * 360 / perimeter;
		
		//update ship angle
		var modifier = _rotationAxis.y < 0 ? -90 : 90; 
		var angle = Vector3.SignedAngle(direction, radius, Vector3.up) + modifier;
		_playerTransform.Rotate(Vector3.up, angle, Space.World);
	}

	private float DistanceToPlayer(PlanetController planet)
	{
		return Vector3.Distance(planet.gameObject.transform.position, _playerTransform.position) - planet.Size / 2;
	}

    private bool CheckRenderers() //for ScreenWrapping
    {
        foreach (var Renderer in renderers)
        {
            if (Renderer.isVisible)
            {
                return true;
            }
        }

        return false;
    }

    void ScreenWrap() //for ScreenWrapping
    {
        var isVisible = CheckRenderers();
        //Debug.Log("isVisible = " + isVisible);

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        //Debug.Log("isWrappingX " + isWrappingX);
        //Debug.Log("isWrappingy " + isWrappingY);

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        //Debug.Log("viewPortPosition.x = " + viewportPosition.x);
        //Debug.Log("viewPortPosition.y = " + viewportPosition.y);
        //Debug.Log("Old Position =" + transform.position);

        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
            Debug.Log("Now");

            isWrappingX = true;
        }

        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.z = -newPosition.z;

            isWrappingY = true;
        }

        transform.position = newPosition;
        //Debug.Log("New Position = " + newPosition);
    }

}
