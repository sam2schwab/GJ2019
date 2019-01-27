using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float smooth = 1f;
    public float speedY = 0.1f;
    public float speedX = 0f;
    private Quaternion targetRotation;
    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        targetRotation *= Quaternion.AngleAxis(speedY, Vector3.up);
        targetRotation *= Quaternion.AngleAxis(speedX, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smooth * Time.deltaTime);
    }
}
