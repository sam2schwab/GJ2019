using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Transform anchorTransform;
	private Transform _playerTransform;
    public float speed = 10;
    private bool isAnchored = true;
	
	// Use this for initialization
	private void Start ()
	{
		_playerTransform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            isAnchored = !isAnchored;
        }

        if (isAnchored)
        {
            _playerTransform.RotateAround(anchorTransform.position, Vector3.up, -20 * Time.deltaTime);
        }

        if (!isAnchored)
        {
            _playerTransform.Translate(speed * Vector3.up * Time.deltaTime);
        }
       
    
	}
}
