using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoManager : MonoBehaviour
{
    VideoPlayer videoPlayer;
    bool isDone = false;
    float doneTime = 0f;
    public float waitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += vp =>
        {
            isDone = true;
            doneTime = Time.time;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (isDone && Time.time >= (waitTime + doneTime)) 
        {
            SceneManager.LoadScene("Start");
        }
        if (Input.GetButtonDown("Submit"))
        {
            SceneManager.LoadScene("Start");
        }
    }

    //void EndReached(UnityEngine.Video.VideoPlayer vp)
    //{
    //    vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    //}
}
