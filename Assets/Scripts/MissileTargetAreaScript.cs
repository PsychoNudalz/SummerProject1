using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTargetAreaScript : MonoBehaviour
{
    public float areaRadius = 25f;
    public float scale = 0.8f;
    public bool areaScaledToDistance = true;
    public float sizePerUnit = 0.1f;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.transform.localScale = new Vector3(areaRadius * 2f,gameObject.transform.localScale.y,areaRadius*2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getRandomPoint()
    {
        float randomX = Random.Range(-areaRadius*scale, areaRadius * scale);
        float pyth = Mathf.Sqrt(areaRadius * areaRadius - randomX *randomX) * scale;
        float randomZ = Random.Range(-pyth, pyth);
        Vector3 randomPoint = (new Vector3(randomX, 0, randomZ))+transform.position;
        return randomPoint;
    }

    public void setArea(float distance)
    {
        areaRadius = distance * sizePerUnit;
        gameObject.transform.localScale = new Vector3(areaRadius, gameObject.transform.localScale.y, areaRadius);
    }
}
