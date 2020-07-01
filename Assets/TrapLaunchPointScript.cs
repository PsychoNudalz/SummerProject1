using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLaunchPointScript : MonoBehaviour
{

    public GameObject trap;
    public Rigidbody trapRB;
    public Transform aimBall;
    public Vector3 testPoint;
    public Quaternion fireAngle;
    public float speed = 10f;
    public float timeBetweenFire = 1f;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawRay(transform.position, fireAngle * transform.forward * speed, Color.green);
    }

    public void fire()
    {
        calculateLaunchAngle();
        launchTrap();

    }

    private void launchTrap()
    {
        GameObject t = Instantiate(trap, transform.position, Quaternion.Euler(0,0,0));
        Vector3 v = (transform.rotation*(fireAngle * Vector3.forward).normalized * speed);
        print(v);
        t.GetComponent<Rigidbody>().velocity = (v);
    }

    void calculateLaunchAngle()
    {
        float gravity = Physics.gravity.y;
        Vector3 dis_Vector = (aimBall.position - transform.position);
        transform.rotation = Quaternion.LookRotation(dis_Vector.normalized);
        
        //testPoint = aimBall.position;
        float disH = dis_Vector.magnitude;
        float sinValue = (disH * gravity) / (speed * speed);
        if (sinValue < -1f)
        {
            sinValue = -1f;
        }
        float angleX = Mathf.Rad2Deg*(Mathf.Asin(sinValue))/2f;
        //print(sinValue + ",   " + angleX);
        fireAngle = Quaternion.Euler(angleX,0,0);
        print(dis_Vector + ",  " + disH + ",  " + fireAngle + ",  "+transform.rotation);



        return;

        //print(Vector3.RotateTowards(transform.position, aimBall.position, 2*Mathf.PI, 1));
    }

}
