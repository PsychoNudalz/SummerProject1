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
    public float test;

    [SerializeField] bool primed = false;
    [SerializeField] string collision;

    // Start is called before the first frame update
    private void Awake()
    {
        
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
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!primed && stickTag.Contains(collision.collider.tag))
        {

            getAlignment(collision);
            this.collision = collision.collider.tag.ToString();
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
        rb.constraints = RigidbodyConstraints.FreezeAll;

    }
}
