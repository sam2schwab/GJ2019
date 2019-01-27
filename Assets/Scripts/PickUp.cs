using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip sound;
    public GameObject powerUp;
    public WeaponScript weapon;
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); //Rotator
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("PickUp picked up");
            GameManager.Instance.PlaySound(sound);
            player = other.gameObject.GetComponent<PlayerController>();
            if(powerUp != null)
            {
                player.SetPower(powerUp);
            }
            if(weapon != null)
            {
                player.SetWeapon(weapon);
            }

            Destroy(gameObject);
        }
    }
}
