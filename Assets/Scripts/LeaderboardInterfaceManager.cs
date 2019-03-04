using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardInterfaceManager : MonoBehaviour
{
    [SerializeField] GameObject localLeaderboard, globalLeaderboard;
    [SerializeField] GameObject[] positionsLocal, positionsGlobal;
    LeaderboardManager lbManager;
    List<Score> scoresLocal, scoresGlobal;

    // Start is called before the first frame update
    async void Start()
    {
        lbManager = LeaderboardManager.Instance;
        scoresLocal = await LeaderboardManager.Instance.GetBestScores();
        scoresGlobal = await LeaderboardManager.Instance.GetBestScores(true);
        ShowLocal();
    }

    public void ShowGlobal()
    {
        globalLeaderboard.SetActive(true);
        localLeaderboard.SetActive(false);
        UpdatePositions(positionsGlobal, scoresGlobal);
    }
    public void ShowLocal()
    {
        localLeaderboard.SetActive(true);
        globalLeaderboard.SetActive(false);
        UpdatePositions(positionsLocal, scoresLocal);
    }

    void UpdatePositions(GameObject[] p, List<Score> s)
    {
        for (int i = 0; i < p.Length; i++)
        {
            if (i < s.Count)
            {
                Score score = s[i];
                p[i].GetComponent<Text>().text = (i + 1).ToString("D2") + " - " + score.Name + " - " + score.Value;
            }
            else
            {
                p[i].GetComponent<Text>().text = (i + 1).ToString("D2") + " - ??? - ???";
            }
        }
    }
}
