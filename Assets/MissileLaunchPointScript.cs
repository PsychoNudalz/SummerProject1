using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunchPointScript : MonoBehaviour
{
    public GameObject missle;
    public GameObject targetArea;
    public float timeToSpawn = 0.5f;
    private float currentTime;
    public float launchForceY = 1500f;
    [SerializeField] private Vector3 targetDirection;
    public float directionScale = 0.2f;
    public Vector3 randomForce;
    // Start is called before the first frame update
    void Awake()
    {
        currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - currentTime > timeToSpawn)
        {
            currentTime = Time.time;
            launchMissle();

        }
    }

    private void launchMissle()
    {
        targetDirection = targetArea.transform.position - transform.position;
        MissileProjectileScript m = Instantiate(missle).GetComponent<MissileProjectileScript>();
        m.gameObject.transform.position = transform.position;
        m.setTargetPosition(targetArea.GetComponent<MissileTargetAreaScript>().getRandomPoint());
        Vector3 launchForce = new Vector3(Random.Range(-randomForce.x, randomForce.x), launchForceY+ Random.Range(-randomForce.y, randomForce.y), Random.Range(-randomForce.z, randomForce.z));
        launchForce += targetDirection* directionScale;
        m.setInitialLaunch(launchForce,(targetDirection.magnitude)/100f);
        
        print(m+"  "+ targetDirection.magnitude);
    }
}
