using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DefeatManager : MonoBehaviour
{
    public GameObject defeatPopupGo, highscorePopupGo;
    public GameObject defaultButtonDefeat, defaultButtonHighscore;
    public GameObject textScoreDefeat, textRankDefeat, textScoreHighscore, textRankHighscore;
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
    async void Update()
    {
        if (GameManager.Instance.isGameOver && !isDefeated)
        {
            int myScore = GameManager.Instance.score;
            GameObject textScore, textRank;
            isDefeated = true;
            eventSystem.SetSelectedGameObject(null);
            if (await LeaderboardManager.Instance.GetPosition(myScore,false) <= highscoreThreshold && myScore > 0)
            {
                highscorePopupGo.SetActive(true);
                eventSystem.SetSelectedGameObject(defaultButtonHighscore);
                textScore = textScoreHighscore;
                textRank = textRankHighscore;
            }
            else
            {
                defeatPopupGo.SetActive(true);
                eventSystem.SetSelectedGameObject(defaultButtonDefeat);
                textScore = textScoreDefeat;
                textRank = textRankDefeat;
            }
            textScore.GetComponent<Text>().text = "Score: " + myScore;
            textRank.GetComponent<Text>().text = "Local Rank: " + await LeaderboardManager.Instance.GetPosition(myScore, false) + ", Global Rank: " + await LeaderboardManager.Instance.GetPosition(myScore, true);
        }
    }
}
