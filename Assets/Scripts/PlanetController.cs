using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public float Size
    {
        get { return this.transform.localScale.y; }
        set { transform.localScale = new Vector3(value, value, value); }
    }

    private const float Ratio = 0.0131f;
    public float GravityRadius { get; private set; }
    public float radiusMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        GravityRadius = Size * radiusMultiplier / 2;
        var radius = radiusMultiplier * 0.135f;
        gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale = new Vector3(radius,radius,radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
