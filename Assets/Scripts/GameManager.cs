using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    #endregion

    private Camera _mainCamera;
    public Camera MainCamera
    {
        get
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
            }
            return _mainCamera;
        }
    }

    public static readonly System.Random Rng = new System.Random();

    public List<PlanetController> planets;
    public GameObject home;
    public int homeHealth = 5;
    public bool isGameOver = false;
    public int score = 0;

    //For sound
    public AudioClip[] explosionClips;
    public AudioClip playerCrashClip;
    public AudioClip enemyDeathClip;
    public AudioClip enemyHitClip;
    public int wave;
    
    //For LeaderboardManager
    public LeaderboardManager leaderboardManager;

    // Start is called before the first frame update
    private void Awake()
    {
        var cubeMaps = Resources.LoadAll<Cubemap>("Skyboxes");
        var cubeMap = cubeMaps[Rng.Next(cubeMaps.Length)];
        var material = new Material(Shader.Find("Skybox/Cubemap"));
        material.SetTexture("_Tex", cubeMap);
        RenderSettings.skybox = material;
        Instance = this;
        planets = FindObjectsOfType<PlanetController>().ToList();
    }

    public void DamageHome(int damage)
    {
        homeHealth -= damage;
        print("homeHealth = " + homeHealth);
        if (homeHealth == 0)
        {
            isGameOver = true;
            Debug.Log("Game Over");
            //Death();
        }
    }

    public void AugmentScore(int gain)
    {
        score += gain;
        print("Score : " + score);
    }

    #region Sound clips

    public void Explosion()
    {
        var clip = explosionClips[Rng.Next(explosionClips.Length)];
        var pitch = Rng.Next(0, 8) * 0.1f + 0.6f;
        PlaySound(clip, pitch);
    }

    public void PlayerCrash()
    {
        PlaySound(playerCrashClip);
    }

    public void EnemyDeath()
    {
        PlaySound(enemyDeathClip);
    }

    public void EnemyHit()
    {
        PlaySound(enemyHitClip);
    }

    public void PlaySound(AudioClip clip, float pitch = 1f)
    {
        var audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.Play();
        StartCoroutine(DeleteSource(audioSource, clip.length));
    }

    private IEnumerator DeleteSource(AudioSource audioSource, float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        Destroy(audioSource);
    }



    #endregion
}
