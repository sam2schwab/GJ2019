using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DefaultButton : MonoBehaviour
{
    public GameObject button;
    EventSystem myEventSystem;
    // Start is called before the first frame update
    void Start()
    {
        myEventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myEventSystem.currentSelectedGameObject==null)
        {
            Debug.Log("is null");
            myEventSystem.SetSelectedGameObject(button);
        }
    }
}
