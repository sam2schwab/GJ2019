using UnityEngine;
using Random = System.Random;

public class Asteroid : MonoBehaviour
{
    private static GameObject[] _prefabs;
    private static readonly Random Rng = new Random();
    private void Awake()
    {
        if (_prefabs == null)
            _prefabs = Resources.LoadAll<GameObject>("Asteroids"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject o = gameObject;
        o.GetComponent<MeshFilter>().mesh = null;
        var asteroid = Instantiate(_prefabs[Rng.Next(_prefabs.Length)], transform);
        asteroid.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
    }
}
