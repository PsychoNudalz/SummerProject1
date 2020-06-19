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
    public Vector2 distance = new Vector2(10, 15); // max distance before leg reposition
    public float maxRange = 30f;
    public float timeToUpdate = 1f;
    [SerializeField] private float time = 0;
    [SerializeField] private bool updateLeg;
    private RaycastHit hit;

    public Vector3 controllerPosition = new Vector3();

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
        updateLeg = Vector3.Distance(transform.position, controllerPosition) < distance.x || Vector3.Distance(transform.position, controllerPosition) > distance.y;
        if (updateLeg)
        {
            //print("too far");
            updateController();
        }
        Debug.DrawRay(transform.position, dir * maxRange, Color.blue);

        time -= Time.deltaTime;
    }

    private void updateController()
    {
        //print("Update Leg: " + gameObject.ToString());

        dir = (spider.rotation * controllerRaycastDirection * Vector3.up);
        print(dir);
        if (Physics.Raycast(transform.position, dir, out hit, maxRange, layerMask))
        {
            controllerPosition = hit.point;
            print("hit: " + hit);
            controller.setCurrentPostion(controllerPosition);
        }

    }
}
