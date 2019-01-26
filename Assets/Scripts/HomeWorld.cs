using UnityEngine;
using Random = System.Random;

public class HomeWorld : MonoBehaviour
{
    private static GameObject[] _prefabs;
    private static readonly Random Rng = new Random();

    private GameObject _planet;
    private void Awake()
    {
        if (_prefabs == null)
            _prefabs = Resources.LoadAll<GameObject>("Planets"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject o = gameObject;
        o.GetComponent<MeshFilter>().mesh = null;
        _planet = Instantiate(_prefabs[Rng.Next(_prefabs.Length)], transform);
        _planet.transform.localScale *= 0.1f;
    }

    private void Update()
    {
        _planet.transform.Rotate(Vector3.up, 20 * Time.deltaTime);
    }
}
