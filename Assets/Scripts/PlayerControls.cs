using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public CameraAim cam;
    public PlayerStates playerStates;
    //public MissileLaunchPointScript missileLauncher;

    [SerializeField] private bool aiming;
    [SerializeField] private bool firingWeapon;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            aiming = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            aiming = false;
        }
        cam.setAiming(aiming);

        if (Input.GetMouseButtonDown(0))
        {
            firingWeapon = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerStates.stopCurrentWeapon();
            firingWeapon = false;
        }

        if (Input.GetKeyDown("1"))
        {
            playerStates.setWeapon(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            playerStates.setWeapon(1);
        }
        if (firingWeapon)
        {
            playerStates.fireCurrentWeapon();
        }
    }
}
