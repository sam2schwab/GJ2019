using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private List<GameObject> _powerUps;
    
    public float intervalBetweenSpawns = 20f;
    public int maxPowerups = 4;
    private List<GameObject> onMap = new List<GameObject>();
    private float _nextSpawn;
    private float _maxZ;
    private float _maxX;


    // Start is called before the first frame update
    void Start()
    {
        _powerUps = Resources.LoadAll<GameObject>("PowerUps").ToList();
        var worldPoint = GameManager.Instance.MainCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 60f));
        _maxX = -worldPoint.x - 3f;
        _maxZ = -worldPoint.z - 3f;
        var now = Time.time;
        _nextSpawn = now;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextSpawn)
        {
            onMap = onMap.FindAll(go => go != null);
            if (onMap.Count < maxPowerups)
                Spawn();
            _nextSpawn += intervalBetweenSpawns;
        }
    }

    private void Spawn()
    {
        var rng = GameManager.Rng;
        Vector3 position;
        do
        {
            var x = (float) rng.NextDouble() * 2 * _maxX - _maxX;
            var z = (float) rng.NextDouble() * 2 * _maxZ - _maxZ;
            position = new Vector3(x, 0, z);
        } while (IntersectsPlanet(position));

        var pickup = _powerUps[rng.Next(_powerUps.Count)];
        onMap.Add(Instantiate(pickup, position, Quaternion.identity));
    }

    private bool IntersectsPlanet(Vector3 position)
    {
        foreach (var planet in GameManager.Instance.planets)
        {
            if ((planet.gameObject.transform.position - position).magnitude < (planet.Size / 2 + 5))
                return true;
        }

        return false;
    }
}
