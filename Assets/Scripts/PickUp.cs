using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public AudioClip sound;
    public WeaponScript weapon;
    
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
            var player = other.gameObject.GetComponent<PlayerController>();
            if (weapon.type == PickUpType.Power && player.powerUps.Count > 0) return;
            GameManager.Instance.PlaySound(sound);
            if(weapon.type == PickUpType.Power)
            {
                player.SetPower(weapon);
            }
            else
            {
                player.SetWeapon(weapon);
            }

            Destroy(gameObject);
        }
    }
}
