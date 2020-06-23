using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunchPointScript : MonoBehaviour
{
    public GameObject missle;
    public GameObject targetArea;
    public float timeBetweenFire = 0.1f;
    public int missilesPerVolley = 1;
    private float currentTime;
    public float launchForceY = 3000f;
    [SerializeField] private Vector3 targetDirection;
    public float directionScale = 0.2f;
    public Vector3 randomForce;
    private bool firing = false;
    private int volleyCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (firing)
        {
            activeLaunchMissile();
            if (volleyCount >= missilesPerVolley)
            {
                firing = false;
                volleyCount = 0;
            }
        }
    }

    public void launchMissle()
    {
        targetDirection = targetArea.transform.position - transform.position;
        MissileProjectileScript m = Instantiate(missle).GetComponent<MissileProjectileScript>();
        m.gameObject.transform.position = transform.position;
        m.setTargetPosition(targetArea.GetComponent<MissileTargetAreaScript>().getRandomPoint());
        Vector3 launchForce = new Vector3(Random.Range(-randomForce.x, randomForce.x), launchForceY+ Random.Range(-randomForce.y, randomForce.y), Random.Range(-randomForce.z, randomForce.z));
        launchForce += targetDirection* directionScale;
        m.setInitialLaunch(launchForce,(targetDirection.magnitude)/(launchForceY/10f));
        
        //print(m+"  "+ targetDirection.magnitude);
    }

    public void fire()
    {
        firing = true;
    }

    
    public void activeLaunchMissile()
    {
        if (Time.time - currentTime > timeBetweenFire)
        {
            currentTime = Time.time;
            volleyCount++;
            launchMissle();

        }
    }

    
}
