using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class EnemySpawner : MonoBehaviour
{
    private class SpawnDetails
    {
        public float NextSpawn;
        public float Interval;
        public float FirstSpawnDelay;
        public Action Spawn;
    }

    public GameObject ship;
    public GameObject capitalShip;
    public GameObject engineerShip;
    public float difficultyFactor = 1;
    
    //waves
    public float waveDuration = 10;
    public float pauseDuration = 2f;
    public float scalingFactor = 1.1f;
    private float _nextPause;
    private float _nextWave;

    private readonly List<SpawnDetails> _spawnDetails = new List<SpawnDetails>();
    
    private float _spawnRadius;
    private float _maxZ;
    private float _maxX;


    void Awake()
    {
        _spawnDetails.Add(new SpawnDetails()
        {
            Interval = 12f,
            FirstSpawnDelay = 1000, //0f,
            Spawn = () =>
            {
                Spawn(ship);
            }
        });
        _spawnDetails.Add(new SpawnDetails()
        {
            Interval = 53f,
            FirstSpawnDelay = 1000, //53f,
            Spawn = () =>
            {
                Spawn(capitalShip);
            }
        });
        _spawnDetails.Add(new SpawnDetails()
        {
            Interval = 0.1f,
            Spawn = () =>
            {
                const int degreesRange = 50;
                int count = GameManager.Instance.asteroids.Count;
                int index = GameManager.Rng.Next(count);
                PlanetController asteroid = GameManager.Instance.asteroids[index];
                float angle = Vector3.SignedAngle(asteroid.transform.position, Vector3.left, Vector3.down);
                Spawn(engineerShip, angle - 25);
                Spawn(engineerShip, angle + 25);
                angle = angle + GameManager.Rng.Next(degreesRange + 1) - degreesRange / 2f;
                Spawn(engineerShip, angle);
            }
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 worldPoint = GameManager.Instance.MainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        _spawnRadius = worldPoint.magnitude + 10f;
        _maxX = -worldPoint.x + 2f;
        _maxZ = -worldPoint.z + 2f;
        float now = Time.time;
        _nextPause = now + waveDuration;
        _nextWave = _nextPause + pauseDuration;
        _spawnDetails.ForEach(details => { details.NextSpawn = now + details.FirstSpawnDelay; });
        GameManager.Instance.wave = 1;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDifficulty();
        _spawnDetails.ForEach(details =>
        {
            if (!(Time.time > details.NextSpawn)) return;
            details.Spawn?.Invoke();
            details.NextSpawn += details.Interval / difficultyFactor;
        });
    }

    private void UpdateDifficulty()
    {
        if (Time.time > _nextWave)
        {
            GameManager.Instance.wave++;
            _nextWave += waveDuration + pauseDuration;
            difficultyFactor *= scalingFactor; 
            _spawnDetails.ForEach(details =>
            {
                details.NextSpawn += (details.NextSpawn - Time.time) / scalingFactor; 
            });
        }

        if (!(Time.time > _nextPause)) return;
        _nextPause += waveDuration + pauseDuration;
        _spawnDetails.ForEach(details => { details.NextSpawn += pauseDuration; });
    }

    private void Spawn(GameObject enemyShip)
    {
        var angle = (float) (GameManager.Rng.NextDouble() * 360);
        Spawn(enemyShip, angle);
    }
    
    private void Spawn(GameObject enemyShip, float angle)
    {
        Vector3 position =  Quaternion.Euler(0, angle, 0) * Vector3.left * _spawnRadius;
        position.x = Mathf.Clamp(position.x, -_maxX, _maxX);
        position.z = Mathf.Clamp(position.z, -_maxZ, _maxZ);
        position.y = 8;
        
        Instantiate(enemyShip, position, Quaternion.identity);
        
    }
}

internal enum SpawnType
{
    Ship,
    CapitalShip,
    Swarm,
    MotherShip,
    Engineer
}
