using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var viewportPoint = _mainCamera.WorldToViewportPoint(transform.position);
        if (viewportPoint.x > 1.03 || viewportPoint.x < -0.03)
        {
            var ship = transform;
            var position = ship.position;
            position = new Vector3(-position.x, position.y, position.z);
            ship.position = position;
        }
        if (viewportPoint.y > 1.03 || viewportPoint.y < -0.03)
        {
            var ship = transform;
            var position = ship.position;
            position = new Vector3(position.x, position.y, -position.z);
            ship.position = position;
        }
    }
}
