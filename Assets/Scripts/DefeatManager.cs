using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefeatManager : MonoBehaviour
{
    public GameObject defeatPopupGo, highscorePopupGo;
    public GameObject defaultButtonDefeat, defaultButtonHighscore;
    public GameObject textScore, textRank;
    public int highscoreThreshold = 10;
    bool isDefeated = false;
    EventSystem eventSystem;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        highscorePopupGo.SetActive(false);
        defeatPopupGo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver && !isDefeated)
        {
            int myScore = GameManager.Instance.score;
            isDefeated = true;
            eventSystem.SetSelectedGameObject(null);
            if (true)//LeaderboardManager.Instance.GetLocalPosition(myScore) <= highscoreThreshold)
            {
                highscorePopupGo.SetActive(true);
                eventSystem.SetSelectedGameObject(defaultButtonHighscore);
            }
            else
            {
                defeatPopupGo.SetActive(true);
                eventSystem.SetSelectedGameObject(defaultButtonDefeat);
            }
            textScore.GetComponent<Text>().text = "Score: " + myScore;
            //textRank.GetComponent<Text>().text = "Local Rank: " + LeaderboardManager.Instance.GetLocalPosition(myScore)+ ", Global Rank: " + LeaderboardManager.Instance.GetGlobalPosition(myScore);
        }
    }
}
