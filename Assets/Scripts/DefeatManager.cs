using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefeatManager : MonoBehaviour
{
    public GameObject defeatPopupGo;
    public GameObject defaultButton;
    bool isDefeated = false;
    EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        defeatPopupGo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver && !isDefeated)
        {
            isDefeated = true;
            eventSystem.SetSelectedGameObject(null);
            defeatPopupGo.SetActive(true);
            eventSystem.SetSelectedGameObject(defaultButton);
        }
    }
}
