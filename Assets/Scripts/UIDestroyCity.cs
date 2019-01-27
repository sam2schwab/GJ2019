using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDestroyCity : MonoBehaviour
{
    public int position = 0;
    public Sprite destroyedImage;
    // Start is called before the first frame update
    void Start()
    {
        if (position>GameManager.Instance.homeHealth)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (position > GameManager.Instance.homeHealth)
        {
            GetComponent<Image>().sprite = destroyedImage;
        }
    }
}
