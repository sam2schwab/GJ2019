using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePopupGo;
    public GameObject defaultButton;
    bool isPaused = false;
    EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        UnpauseGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Exit") && !GameManager.Instance.isGameOver)
        {
            if (isPaused == false)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        eventSystem.SetSelectedGameObject(null);
        pausePopupGo.SetActive(true);
        eventSystem.SetSelectedGameObject(defaultButton);
        isPaused = true;
        Time.timeScale = 0;
    }
    public void UnpauseGame()
    {
        pausePopupGo.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
