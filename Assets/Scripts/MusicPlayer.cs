using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private static object _instance;
    public AudioClip mainMenuSong;
    public AudioClip battleSong;

    private void Awake()
    {
        if (_instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += (scene, loadscene) => 
        {
            switch (scene.name)
            {
                case "main":
                    _audioSource.clip = battleSong;
                    break;
                case "Start":
                    _audioSource.clip = mainMenuSong;
                    break;
            }
            PlayMusic();
        };


    }
}
