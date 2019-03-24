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
    public List<PlanetController> asteroids;
    public PlanetController home;
    public int homeHealth = 5;
    public bool isGameOver = false;
    public int score = 0;

    [Header("Laser Sight")]
    public bool laserSight = false;

    [Header("Small Asteroids")]
    public bool smallAsteroids = false;

    [Header ("Many Lives")]
    public bool manyLives = false;
    public int playerLives = 1;
    public int howManyLives = 3;
    public float deathTime = 15f;
    public float remainingDeathTime = 15f;
    public GameObject playerPrefab;

    [Header("Sound Clips")]
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
        asteroids = new List<PlanetController>(planets);
        asteroids.Remove(home);
    }

    private void Start()
    {
        if (manyLives)
        {
            playerLives = howManyLives;
        }
    }

    public void DamageHome(int damage)
    {
        homeHealth -= damage;
        print("homeHealth = " + homeHealth);
        if (homeHealth <= 0)
        {
            isGameOver = true;
            Debug.Log("Game Over");
            //Death();
        }
    }

    public void PlayerDeath()
    {
        playerLives--;

        if (playerLives > 0)
        {
            StartCoroutine(RespawnPlayer());
        }

        else if (playerLives <= 0)
        {
            isGameOver = true;
        }
    }

    public void AugmentScore(int gain)
    {
        score += gain;
        print("Score : " + score);
    }

    private IEnumerator RespawnPlayer()
    {
        for (remainingDeathTime = deathTime; remainingDeathTime>0; remainingDeathTime--)
        {
            yield return new WaitForSeconds(1f);
        }

        GameObject newPlayer = Instantiate(playerPrefab);
        PlayerController newPlayerController = newPlayer.GetComponent<PlayerController>();
        newPlayerController.anchorTransform = home.transform;
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
