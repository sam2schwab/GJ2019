using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LeaderboardManager : MonoBehaviour
{
    public List<Score> GetScores(bool global = false)
    {
        return new List<Score>();
    }

    public int GetPosition(bool global)
    {
        return 1;
    }

    public void SaveScore(Score toSave)
    {
    }
}