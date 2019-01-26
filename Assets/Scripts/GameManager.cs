using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;
    
    #endregion
    
    public List<PlanetController> planets;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        planets = FindObjectsOfType<PlanetController>().ToList();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
