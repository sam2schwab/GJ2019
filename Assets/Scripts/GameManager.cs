using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlanetController[] planets;

    // Start is called before the first frame update
    private void Awake()
    {
        planets = FindObjectsOfType<PlanetController>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
