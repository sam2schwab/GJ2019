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

    public int score = 0;

    //For sound
    public AudioClip[] explosionClips;
    public AudioClip playerCrashClip;
    public AudioClip enemyDeathClip;
    public AudioClip enemyHitClip;
    private AudioSource audioSource;


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

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageHome(int damage)
    {
        homeHealth -= damage;
        print("homeHealth = " + homeHealth);
        if (homeHealth == 0)
        {
            Debug.Log("Game Over");  //Death();
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
        audioSource.clip = explosionClips[Rng.Next(explosionClips.Length)];
        audioSource.pitch = Rng.Next(0,8) * 0.1f + 0.6f;
        audioSource.Play();
    }

    public void PlayerCrash()
    {
        audioSource.clip = playerCrashClip;
        audioSource.Play();
    }

    public void EnemyDeath()
    {
        audioSource.clip = enemyDeathClip;
        audioSource.Play();
    }

    public void EnemyHit()
    {
        audioSource.clip = enemyHitClip;
        audioSource.Play();
    }

    #endregion
}
