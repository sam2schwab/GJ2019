using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWave : MonoBehaviour
{
    int wave = 1;
    Text waveText;
    // Start is called before the first frame update
    void Start()
    {
        waveText = GetComponent<Text>();
        waveText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        //int newWave = GameManager.Instance.wave;
        //if (wave != newWave)
        //{
        //    waveText.text = "Wave: " + newWave.ToString();
        //}
    }
}
