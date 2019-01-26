using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    #region Singleton

    public static HomeController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
          
    }

    #endregion

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
