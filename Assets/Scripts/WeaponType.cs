using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : MonoBehaviour
{

    [SerializeField] private WeaponTypesEnum weapon;
    [SerializeField] private GameObject launcher;
    [SerializeField] private GameObject aimer;

    [SerializeField] private float ammo;
    public float Ammo { get => ammo; set => ammo = value; }
    public float Ammo_Max;
    public float Ammo_Cost;
    public bool rechargeable = true;
    public float rechargeRate = 10f;



    public float CooldownBetweenFire;
    [SerializeField] private float timeNow;
    public float TimeNow { get => timeNow; set => timeNow = value; }

    public bool fireWhenFull;
    [SerializeField] private bool full;
    [SerializeField] private bool activeFire;
    public bool ActiveFire { get => activeFire; set => activeFire = value; }

    private void Update()
    {
        if (rechargeable & !isFull() & !activeFire)
        {
            ammo += rechargeRate * Time.deltaTime;
        }
        if (timeNow > 0)
        {
            timeNow -= Time.deltaTime;
        }
    }

    public bool canFire()
    {
        if (timeNow <= 0)
        {
            if (isFull())
            {
                timeNow = CooldownBetweenFire;

                return true;
            }
            if (fireWhenFull)
            {
                if (activeFire && ammo > Ammo_Cost)
                {
                    timeNow = CooldownBetweenFire;

                    return true;
                }
                return false;

            }
            if (ammo >= Ammo_Cost)
            {
                timeNow = CooldownBetweenFire;

                return true;
            }
            else
            {
                return false;
            }

        }
        return false;


    }

    public bool isFull()
    {
        full = Ammo >= Ammo_Max;
        return full;
    }

    public bool consumeAmmo()
    {
        if (canFire())
        {
            ammo -= Ammo_Cost;
            return true;
        }
        return false;
    }
    public void fireWeapon()
    {
        if (consumeAmmo())
        {
            activeFire = true;
            switch (weapon)
            {
                case WeaponTypesEnum.Missile:
                    launcher.GetComponent<MissileLaunchPointScript>().fire();
                    break;
                case WeaponTypesEnum.Trap:
                    launcher.GetComponent<TrapLaunchPointScript>().fire();
                    break;
                case WeaponTypesEnum.Rocket:
                    break;
            }
        }
        else
        {
            activeFire = false;
        }
        //print(ammo);

    }

    public void addAmmo(float amout)
    {
        ammo += amout;
    }

    public string getWeaponType()
    {
        string name = "NULL";
        switch (weapon)
        {
            case WeaponTypesEnum.Missile:
                name = "Missile";
                break;
            case WeaponTypesEnum.Trap:
                name = "Trap";
                break;
            case WeaponTypesEnum.Rocket:
                break;
        }
        return name;
    }


}
