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
        calculateLaunchAngle();

        if (Time.time - currentTime > timeBetweenFire)
        {
            launchTrap();
            currentTime = Time.time;
        }

        Debug.DrawRay(transform.position, fireAngle * transform.forward * speed, Color.green);


    }

    public void fire()
    {

    }

    private void launchTrap()
    {
        GameObject t = Instantiate(trap, transform.position, transform.rotation);
        //fireAngle = Quaternion.Euler(Random.Range(30, 80), Random.Range(0, 360), 0);
        t.GetComponent<Rigidbody>().velocity = (fireAngle *transform.forward* speed);
    }

    void calculateLaunchAngle()
    {
        float gravity = Physics.gravity.y;
        //float disY = aimBall.position.y - transform.position.y;
        //Vector3 dis_Vector = new Vector3(aimBall.position.x - transform.position.x, 0, aimBall.position.z - transform.position.z);
        Vector3 dis_Vector = aimBall.position - transform.position;

        testPoint = aimBall.position;
        float disH = dis_Vector.magnitude;
        //float a = (speed / trapRB.mass);
        float angleX = Mathf.Rad2Deg*(Mathf.Asin((disH * gravity) / (speed*speed))/2);
        //fireAngle = ;
        print(dis_Vector + ",  " + disH + ",  " + angleX);
        transform.rotation = Quaternion.LookRotation(dis_Vector);
        fireAngle = Quaternion.Euler(angleX,0,0);

        return;

        //print(Vector3.RotateTowards(transform.position, aimBall.position, 2*Mathf.PI, 1));
    }

}
