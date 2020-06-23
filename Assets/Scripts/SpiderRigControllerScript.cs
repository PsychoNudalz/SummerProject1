using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRigControllerScript : MonoBehaviour
{
    public SpiderRigControllerComponent controller;

    public Transform spider;
    public Quaternion controllerRaycastDirection;
    private Vector3 dir;
    [SerializeField] private LayerMask layerMask;
    //public Vector2 distance = new Vector2(10, 15); // max distance before leg reposition
    public float distance = 100f;
    public float maxRange = 30f;
    public float timeToUpdate = 1f;
    [SerializeField] private float time = 0;
    [SerializeField] private bool updateLeg;
    private RaycastHit hit;

    public Vector3 controllerPosition = new Vector3();


    public SpiderRigControllerComponent midController;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        controller.gameObject.SetActive(true);
        updateController();
        //controllerRaycastDirection = controllerRaycastDirection.normalized;
        //controllerRaycastDirection = controllerRaycastDirection.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0)
        {
            updateController();
            time += timeToUpdate;
        }
        //updateLeg = Vector3.Distance(transform.position, controllerPosition) < distance.x || Vector3.Distance(transform.position, controllerPosition) > distance.y;
        if (raycastController())
        {
            //print("too far");
            updateController();
        }
        Debug.DrawRay(transform.position, dir * maxRange, Color.blue);
        updateMidController();

        time -= Time.deltaTime;
    }

    private void updateController()
    {
        //print("Update Leg: " + gameObject.ToString());

        dir = (spider.rotation * controllerRaycastDirection * Vector3.up);
        //print(dir);
        if (Physics.Raycast(transform.position, dir, out hit, maxRange, layerMask))
        {
            controllerPosition = hit.point;
            //print("hit: " + hit);
            controller.setCurrentPostion(controllerPosition);
        }
    }

    private bool raycastController()
    {
        dir = (spider.rotation * controllerRaycastDirection * Vector3.up);
        if (Physics.Raycast(transform.position, dir, out hit, maxRange, layerMask))
        {
            float distanceToNewPoint = Vector3.Distance(hit.point, controller.transform.position);
            //print(distanceToNewPoint);
            if (distanceToNewPoint > distance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public void updateMidController()
    {
        Vector3 midPoint = transform.position + (hit.point - transform.position)*.2f;
        
        midController.setCurrentPostion(midPoint + offset);
    }
}
