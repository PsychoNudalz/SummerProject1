using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    public Camera cam;
    public GameObject aimer;
    public GameObject item;
    [SerializeField] LayerMask layer;
    // Start is called before the first frame update


    private bool aiming = false;

    private void Awake()
    {
        //Instantiate(aimer,transform);
        
    }
    // Update is called once per frame
    void Update()
    {
        item.GetComponent<MissileTargetAreaScript>().setArea(Vector3.Distance(item.transform.position, transform.position));


        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000f,layer))
        {


            aimer.transform.position = hit.point;
            if (aiming)
            {
                item.transform.position = hit.point;
                item.transform.up = hit.normal;

            }
        }
        
    }

    public void setAiming (bool aiming)
    {
        this.aiming = aiming;
    }
}
