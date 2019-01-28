using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public HardPoint emissionPoint;
    public GameObject shot;
    public Sprite weaponIcon;
    Reload weaponReload;
    private float nextFire = 0.0f;
    public float fireRate;
    public int maxAmmo = -1;
    public int ammo;
    public AudioClip shotSound;

    public PickUpType type;

    // Start is called before the first frame update
    void Start()
    {
        ammo = maxAmmo;
        weaponReload = GameObject.FindGameObjectWithTag("PowerUpIcon").GetComponent<Reload>();
        if (type == PickUpType.Weapon) ammo = -1;
        weaponReload.UpdateWeaponSprite(weaponIcon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if(ammo != 0)
        {
            if(Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, transform.position, transform.rotation);
                GameManager.Instance.PlaySound(shotSound);
                if (ammo > 0)
                {
                    ammo--;
                    weaponReload.UpdateAmmo(ammo / (float)maxAmmo);
                }
            }
        }
      

    }
}
