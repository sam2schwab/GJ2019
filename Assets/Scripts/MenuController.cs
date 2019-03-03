using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] GameObject[] letters;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
        //GetComponent<AudioSource>().Play();
    }

    public void QuitGame()
    {
        Application.Quit();
        //GetComponent<AudioSource>().Play();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
        //GetComponent<AudioSource>().Play();
    }

    public void Back()
    {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
        //GetComponent<AudioSource>().Play();
    }
    public void Intro()
    {
        SceneManager.LoadScene("IntroVideo", LoadSceneMode.Single);
        //GetComponent<AudioSource>().Play();
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Single);
    }

    public void RegisterScore()
    {
        LeaderboardManager.Instance.SaveScore(new Score(BuildName(), GameManager.Instance.score));
        StartGame();
    }

    public void BackAndRegister()
    {
        LeaderboardManager.Instance.SaveScore(new Score(BuildName(), GameManager.Instance.score));
        Back();
    }

    public void Options()
    {

    }

    string BuildName()
    {
        string name = "";
        foreach (var item in letters)
        {
            name = name + item.GetComponent<Text>().text;
        }
        return name;
    }
}
