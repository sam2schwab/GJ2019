using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    public PlayerController player;
    int mask;
    float buffer = 1f;

    private void Start()
    {
        mask = LayerMask.GetMask("Boundary");

        Camera cam = GameManager.Instance.MainCamera;
        Vector3 upperRightCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, 72.42f));;

        GameObject _upBoundary = new GameObject("UpBoundary");
        _upBoundary.layer = 8;
        BoxCollider upCl = _upBoundary.AddComponent<BoxCollider>() as BoxCollider;
        upCl.tag = "Boundary";
        upCl.isTrigger = true;
        upCl.center = new Vector3(0, 0, upperRightCorner.z + buffer);
        upCl.size = new Vector3((upperRightCorner.x + buffer) * 2, 1, 1);

        GameObject _downBoundary = new GameObject("DownBoundary");
        _downBoundary.layer = 8;
        BoxCollider downCl = _downBoundary.AddComponent<BoxCollider>() as BoxCollider;
        downCl.tag = "Boundary";
        downCl.isTrigger = true;
        downCl.center = new Vector3(0, 0, (upperRightCorner.z + buffer) * -1);
        downCl.size = new Vector3((upperRightCorner.x + buffer) * 2, 1, 1);

        GameObject _leftBoundary = new GameObject("LeftBoundary");
        _leftBoundary.layer = 8;
        BoxCollider leftCl = _leftBoundary.AddComponent<BoxCollider>() as BoxCollider;
        leftCl.tag = "Boundary";
        leftCl.isTrigger = true;
        leftCl.center = new Vector3(upperRightCorner.x + buffer, 0, 0);
        leftCl.size = new Vector3(1, 1, (upperRightCorner.z + buffer) * 2);

        GameObject _rightBoundary = new GameObject("RightBoundary");
        _rightBoundary.layer = 8;
        BoxCollider rightCl = _rightBoundary.AddComponent<BoxCollider>() as BoxCollider;
        rightCl.tag = "Boundary";
        rightCl.isTrigger = true;
        rightCl.center = new Vector3((upperRightCorner.x + buffer) * -1, 0, 0);
        rightCl.size = new Vector3(1, 1, (upperRightCorner.z + buffer) * 2);

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Boundary"))
        {
            if (!player.IsAnchored)
            {
                Wrap();
            }
        }
    }

    void Wrap()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 200f, mask))
        {
            transform.position = hit.point;
            player.anchorTransform = null;
        }
    }
}
