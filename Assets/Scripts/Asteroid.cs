using UnityEngine;
using Random = System.Random;

public class Asteroid : MonoBehaviour
{
    private static GameObject[] _prefabs;
    private static readonly Random Rng = new Random();
    private const int MaxRotationSpeed = 20;
    
    private GameObject _asteroid;
    private Vector3 _eulers;

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
        _asteroid = Instantiate(_prefabs[Rng.Next(_prefabs.Length)], transform);
        _asteroid.transform.localScale *= 0.1f;
        _asteroid.transform.localPosition *= 0.1f; 
        _eulers = new Vector3((float) (Rng.NextDouble()*MaxRotationSpeed),(float) (Rng.NextDouble()*MaxRotationSpeed),(float) (Rng.NextDouble()*MaxRotationSpeed));
    }

    private void Update()
    {
        _asteroid.transform.Rotate(_eulers * Time.deltaTime);
    }
}
