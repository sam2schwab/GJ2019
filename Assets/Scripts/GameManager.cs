using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    #endregion

    private static System.Random rng = new System.Random();

    public List<PlanetController> planets;
    public GameObject home;
    public int homeHealth = 5;

    public AudioClip[] explosionClips;
    public AudioClip playerCrashClip;
    public AudioClip enemyDeathClip;
    private AudioSource audioSource;


    // Start is called before the first frame update
    private void Awake()
    {
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

    #region Sound clips

    public void Explosion()
    {
        audioSource.clip = explosionClips[rng.Next(explosionClips.Length)];
        audioSource.pitch = rng.Next(0,8) * 0.1f + 0.6f;
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

    #endregion
}
