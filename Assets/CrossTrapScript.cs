using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrossTrapScript : MonoBehaviour
{
    public Transform raycastReference0;
    public Transform raycastReference1;
    public Transform raycastReference2;
    public Transform raycastReference3;

    public Rigidbody rb;

    public float offsetAngle = 20f;
    public float range = 30f;
    public float explosionRadius = 35f;
    public float explosionDamage = 100f;
    [SerializeField] LayerMask detnationLayer;
    [SerializeField] LayerMask stickLayer;
    [SerializeField] string[] stickTag;
    //public float test;

    public float timeToPrimable = 0.1f;
    [SerializeField] bool primed = false;
    //[SerializeField] string collision;
    public GameObject explosion;
    // Start is called before the first frame update
    private void Awake()
    {
        rb.AddTorque(new Vector3(0, 500, 0));
        transform.Rotate(transform.up, Random.Range(0, 360));
        explosion = Instantiate(explosion, transform.position, transform.rotation);
        explosion.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (primed)
        {
            if (raycastTrap() || raycastTrap(0) || raycastTrap(1) || raycastTrap(2) || raycastTrap(3))
            {
                detnation();
            }
        }else if (!primed)
        {
            timeToPrimable -= Time.deltaTime;
            rb.AddTorque(new Vector3(0, 500, 0));

        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!primed && stickTag.Contains(collision.collider.tag) && timeToPrimable < 0)
        {

            getAlignment(collision);
            //this.collision = collision.collider.tag.ToString();
            primed = true;
        }
    }

    public bool raycastTrap()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, range, detnationLayer))
        {
            if (hit.collider.CompareTag("Target"))
            {
                //print("hit " + hit.collider);
                return true;
            }
        }
        Debug.DrawRay(transform.position, transform.up * range, Color.green);

        return false;
    }

    public bool raycastTrap(int n)
    {
        RaycastHit hit;
        Vector3 castPoint;
        Vector3 castAngle = transform.up;
        Transform reference;
        if (n == 3)
        {
            reference = raycastReference3;

        }
        else if (n == 1)
        {
            reference = raycastReference1;
        }
        else if (n == 2)
        {
            reference = raycastReference2;
        }
        else
        {
            reference = raycastReference0;

        }
        castPoint = reference.position;
        castAngle = Quaternion.AngleAxis(offsetAngle, reference.right) * transform.up;
        if (Physics.Raycast(castPoint, castAngle, out hit, range, detnationLayer))
        {
            if (hit.collider.CompareTag("Target"))
            {
                //print(n+" hit " + hit.collider);
                return true;
            }
        }
        //print(castPoint+"  "+ castAngle);
        Debug.DrawRay(castPoint, castAngle * range, Color.green);
        return false;
    }
    public void detnation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        explosion.GetComponent<ParticleSystem>().Play();
        Target t;
        foreach (Collider nearByObject in colliders)
        {
            t = nearByObject.GetComponent<Target>();
            if (t != null)
            {
                t.TakeDamage(explosionDamage);
            }
        }
        Destroy(gameObject);
        Destroy(explosion, 1.5f);
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
        transform.Rotate(Vector3.up, Random.Range(0, 360));

        rb.constraints = RigidbodyConstraints.FreezeAll;

    }
}
