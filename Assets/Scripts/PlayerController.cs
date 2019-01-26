using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Transform anchorTransform;
	private Transform _playerTransform;
	
	// Use this for initialization
	private void Start ()
	{
		_playerTransform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	private void Update () {
		_playerTransform.RotateAround(anchorTransform.position, Vector3.up, -20*Time.deltaTime);
	}
}
