using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRigControllerComponent : MonoBehaviour
{
    //public GameObject floor;
    public Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {


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
