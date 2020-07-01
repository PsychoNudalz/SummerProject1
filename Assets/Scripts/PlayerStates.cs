using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    //Base States
    public float maxHeath = 100f;
    public WeaponType[] weaponTypes;
    [SerializeField] private WeaponType currentWeapon;
    //public TextMeshProUGUI UI_AmmoValue;


    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weaponTypes[1];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setWeapon(int num)
    {
        currentWeapon.ActiveFire = false;
        if (num < weaponTypes.Length)
        {
            currentWeapon = weaponTypes[num];
        }
    }

    public void fireCurrentWeapon()
    {
        currentWeapon.fireWeapon();
    }

    public void stopCurrentWeapon()
    {
        currentWeapon.ActiveFire = false;
    }

    public string getAmmo()
    {
        return "Weapon Type: " + currentWeapon.getWeaponType() + "\n Ammo: " + currentWeapon.Ammo.ToString("0");

    }
}
