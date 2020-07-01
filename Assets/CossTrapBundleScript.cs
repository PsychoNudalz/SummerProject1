using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CossTrapBundleScript : MonoBehaviour
{
    public GameObject trap;
    public Rigidbody rb;
    public Collider c;


    public int amount;
    [SerializeField] int amountCounter;
    public Quaternion fireAngle;
    [SerializeField] public GameObject[] trapArray;
    [SerializeField] LayerMask stickLayer;
    [SerializeField] string[] stickTag;
    [SerializeField] bool primed = false;
    public float timeToDetnate = 2f;
    [SerializeField] float timeNow = Mathf.Infinity;
    public float timeBetweenTrap = 0.5f;
    [SerializeField] float timeNow2 = 0;

    public float launchForce = 3000f;
    public float launchAngle = 30f;
    public bool randomAngle = true;
    public Vector2 randomAngleRange;

    // Start is called before the first frame update
    void Start()
    {
        trapArray = new GameObject[amount];
        amountCounter = trapArray.Length-1;
        for (int i = 0; i < amount; i++)
        {
            trapArray[i] = Instantiate(trap, transform.position, transform.rotation);
            trapArray[i].SetActive(false);
        }
    }
    private void Awake()
    {
        rb.AddTorque(new Vector3(0, 500, 0));

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!primed && stickTag.Contains(collision.collider.tag))
        {
            getAlignment(collision);
            primed = true;
            timeNow = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (primed && Time.time - timeNow >= timeToDetnate)
        {
            if (timeNow2 <= 0)
            {
                if (!detnation())
                {
                    Destroy(gameObject);
                }
                timeNow2 = timeBetweenTrap;
            }
            timeNow2 -= Time.deltaTime;
        }
    }
    private void getAlignment(Collision c)
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, c.transform.position - transform.position, out hit, 5f, stickLayer))
        {
            Physics.Raycast(transform.position, -Vector3.up, out hit, 5f, stickLayer);
        }

        transform.position = hit.point;

        transform.up = hit.normal;
        print(transform.up);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        foreach (GameObject g in trapArray)
        {
            g.transform.position = transform.position + transform.up * 0.3f;
        }

        transform.Rotate(Vector3.up, Random.Range(0, 360));
        //c.collider.enabled = false;
    }

    private bool detnation()
    {
        
        if (amountCounter >= 0)
        {
            if (amountCounter == 0)
            {
                trapArray[0].GetComponent<CrossTrapScript>().timeToPrimable = -1f;
                trapArray[0].SetActive(true);
            }
            else
            {
                GameObject currentTrap = trapArray[amountCounter];
                currentTrap.SetActive(true);
                Quaternion angleQ = (Quaternion.Euler(new Vector3(0, (360f / (amount-1)) * amountCounter, 0))) * Quaternion.Euler(new Vector3(launchAngle + Random.Range(randomAngleRange.x, randomAngleRange.y), 0, 0));
                Vector3 angle = transform.rotation* angleQ * Vector3.up;
                currentTrap.GetComponent<Rigidbody>().AddForce(launchForce * angle);
            }
            amountCounter--;
            return true;
        }
        return false;


    }
}
