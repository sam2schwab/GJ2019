using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
	public float speed = 25;
	public Transform anchorTransform;

	private Vector3 _rotationAxis;
	private Transform _playerTransform;
    private bool _isAnchored = true;
    private float _rotationSpeed;

    // Use this for initialization
	private void Start ()
	{
		_playerTransform = gameObject.transform;
		UpdateRotation();
	}
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetButtonDown("Jump"))
        {
	        ToggleAnchor();
        }

        if (_isAnchored)
        {
            _playerTransform.RotateAround(anchorTransform.position, _rotationAxis, _rotationSpeed * Time.deltaTime);
        }

        if (!_isAnchored)
        {
            _playerTransform.Translate(speed * Vector3.up * Time.deltaTime);
        }
       
    
	}

	private void ToggleAnchor()
	{
		_isAnchored = !_isAnchored;
		if (!_isAnchored) return;
		anchorTransform = FindNearestPlanet();
		UpdateRotation();
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

	private Transform FindNearestPlanet()
	{
		var planets = GameManager.Instance.planets;
		var nearestPlanet = planets.First();
		var nearestPlanetDistance = DistanceToPlayer(nearestPlanet);
		foreach (var planet in planets)
		{
			var dist = DistanceToPlayer(planet);
			if (dist >= nearestPlanetDistance) continue;
			nearestPlanet = planet;
			nearestPlanetDistance = dist;
		}

		return nearestPlanet.transform;
	}

	private float DistanceToPlayer(PlanetController planet)
	{
		return Vector3.Distance(planet.gameObject.transform.position, _playerTransform.position) - planet.Size;
	}
}
