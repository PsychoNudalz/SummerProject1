using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRigControllerComponent : MonoBehaviour
{
    //public GameObject floor;
    public Vector3 currentPosition;
    public bool staticFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        if (staticFlag)
        {
            transform.position += currentPosition;
        }

        //gameObject.transform.SetParent(floor.transform);
        //currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //print(transform.position);
        //print(floor);
        
        transform.position = currentPosition;
    }

    public void setCurrentPostion(Vector3 newPosition)
    {
        currentPosition = newPosition;
    }
}
