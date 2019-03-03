using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

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

    public void Test()
    {
        //cycl
    }
}
