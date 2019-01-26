using UnityEngine;
using Random = System.Random;

public class HomeWorld : MonoBehaviour
{
    private static GameObject[] _prefabs;
    private static readonly Random Rng = new Random();
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
        var planet = Instantiate(_prefabs[Rng.Next(_prefabs.Length)], transform);
        planet.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
    }
}
