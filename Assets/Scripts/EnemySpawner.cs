using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ship;
    
    private float _intervalBetweenShips = 10f;
    private float _spawnRadius;
    private float _nextSpawn = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnRadius = GameManager.Instance.MainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 60f)).magnitude;
        _nextSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawn)
        {
            Spawn();
            _nextSpawn += _intervalBetweenShips;
        }
    }

    private void Spawn()
    {
        var angle = GameManager.Rng.NextDouble() * 360;
        var position =  Quaternion.Euler(0, (float) angle, 0) * Vector3.left * _spawnRadius;
        position.y = 8;
        var spawnedShip = Instantiate(ship, position, Quaternion.identity);
    }
}
