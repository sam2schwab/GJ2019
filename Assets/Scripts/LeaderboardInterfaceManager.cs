using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardInterfaceManager : MonoBehaviour
{
    [SerializeField] GameObject localLeaderboard, globalLeaderboard;
    [SerializeField] GameObject[] positionsLocal, positionsGlobal;
    LeaderboardManager lbManager;
    List<Score> scoresLocal;

    // Start is called before the first frame update
    void Start()
    {
        lbManager = GetComponent<LeaderboardManager>();
        scoresLocal = lbManager.GetBestScores(false);
        ShowLocal();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowGlobal()
    {
        globalLeaderboard.SetActive(true);
        localLeaderboard.SetActive(false);
    }
    public void ShowLocal()
    {
        localLeaderboard.SetActive(true);
        //int testPosition = 5;
        //string testName = "AAA";
        //int testScore = 99999;
        //foreach (var item in positionsLocal)
        //{
        //    int testPosition =  Random.Range(1,10);
        //    string testName = "AAA";
        //    int testScore = Random.Range(9999, 99999);
        //    item.GetComponent<Text>().text = testPosition.ToString("D2")+ " - " + testName + " - " + testScore;

        for (int i = 0; i < positionsLocal.Length; i++)
        {
            if (i < scoresLocal.Count)
            {
                Score score = scoresLocal[i];
                positionsLocal[i].GetComponent<Text>().text = (i + 1).ToString("D2") + " - " + score.Name + " - " + score.Value;
            }
            else
            {
                positionsLocal[i].GetComponent<Text>().text = (i + 1).ToString("D2") + " - ??? - ???";
            }
        }
    }
}
