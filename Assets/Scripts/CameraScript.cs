using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject Player;
    public Vector3 offsetFromPlayer;
    public bool autoSet = true;


    // Start is called before the first frame update
    void Awake()
    {
        offsetFromPlayer = Player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
