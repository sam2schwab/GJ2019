using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatManager : MonoBehaviour
{
    public GameObject defeatPopupGo;
    // Start is called before the first frame update
    void Start()
    {
        defeatPopupGo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.homeHealth == 0)
        {
            defeatPopupGo.SetActive(true);
        }
    }
    }
}
