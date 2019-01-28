using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    int score = 0;
    Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        int newScore = GameManager.Instance.score;
        if (score != newScore)
        {
            scoreText.text = "Score: " + newScore.ToString();
            score = newScore;
        }
    }
}
