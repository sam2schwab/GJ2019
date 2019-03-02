using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayControls : MonoBehaviour
{
    [SerializeField] Sprite controllerControls, keyboardControls;
    string[] joysticks;
    bool isUsingController = false;

    // Start is called before the first frame update
    void Start()
    {
        joysticks = Input.GetJoystickNames();
        foreach (var x in joysticks)
        {
            if (x.Contains("Controller"))
            {
                isUsingController = true;
            }
        }
        if (isUsingController)
        {
            GetComponent<Image>().sprite = controllerControls;
        }
        else
        {
            GetComponent<Image>().sprite = keyboardControls;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
