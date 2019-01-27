using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ship;
    public GameObject capitalShip;
    public float difficultyFactor = 1;
    
    //waves
    public float waveDuration = 10;
    public float pauseDuration = 2f;
    public float scalingFactor = 0.2f;
    private float _nextPause;
    private float _nextWave;
    private float _initialDifficulty;
    public float waveNumber;
    
    private float _intervalBetweenShips = 12f;
    private float _nextSpawn;
    private float _intervalBetweenCapitalShips = 53f;
    private float _nextCapitalSpawn;
    private float _spawnRadius;
    private float _maxZ;
    private float _maxX;


    // Start is called before the first frame update
    void Start()
    {
        var worldPoint = GameManager.Instance.MainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        _spawnRadius = worldPoint.magnitude + 10f;
        _maxX = -worldPoint.x + 2f;
        _maxZ = -worldPoint.z + 2f;
        var now = Time.time;
        _nextSpawn = now;
        _nextPause = now + waveDuration;
        _nextWave = _nextPause + pauseDuration;
        _nextCapitalSpawn = now + _intervalBetweenCapitalShips;
        waveNumber = 1;
        _initialDifficulty = difficultyFactor;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDifficulty();
        if (Time.time > _nextSpawn)
        {
            Spawn(SpawnType.Ship);
            _nextSpawn += _intervalBetweenShips / difficultyFactor;
        }
        if (Time.time > _nextCapitalSpawn)
        {
            Spawn(SpawnType.CapitalShip);
            _nextCapitalSpawn += _intervalBetweenCapitalShips / difficultyFactor;
        }
    }

    private void UpdateDifficulty()
    {
        if (Time.time > _nextWave)
        {
            waveNumber++;
            _nextWave += waveDuration + pauseDuration;
            difficultyFactor *= scalingFactor;
        }

        if (Time.time > _nextPause)
        {
            _nextPause += waveDuration + pauseDuration;
            _nextSpawn += pauseDuration;
            _nextCapitalSpawn += pauseDuration;
        }
    }

    private void Spawn(SpawnType type)
    {
        var angle = GameManager.Rng.NextDouble() * 360;
        var position =  Quaternion.Euler(0, (float) angle, 0) * Vector3.left * _spawnRadius;
        position.x = Mathf.Clamp(position.x, -_maxX, _maxX);
        position.z = Mathf.Clamp(position.z, -_maxZ, _maxZ);
        position.y = 8;
        switch (type)
        {
            case SpawnType.Ship:
                Instantiate(ship, position, Quaternion.identity);
                break;
            case SpawnType.CapitalShip:
                Instantiate(capitalShip, position, Quaternion.identity);
                break;
            case SpawnType.ShipCluster:
                break;
            default:
                throw new ArgumentOutOfRangeException("type", type, null);
        }
    }
}

internal enum SpawnType
{
    Ship,
    CapitalShip,
    ShipCluster
}
