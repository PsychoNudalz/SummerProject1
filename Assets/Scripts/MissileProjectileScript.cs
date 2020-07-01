using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissileProjectileScript : MonoBehaviour
{
    public Rigidbody rigidbody;
    public string[] tagTargets;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 direction = new Vector3();
    [SerializeField] private bool launched = false;
    [SerializeField] private bool collided = false;


    public float maxSpeed;
    [SerializeField] private float speed = 0;
    public float timeToLaunch = 4f;
    private float startTime;
    public Vector3 launchForce = new Vector3();

    public float explodeRadius = 15f;
    public float damage = 50f;

    public GameObject explosion;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        setDirection();
        startTime = Time.time;
        explosion = Instantiate(explosion, transform.position, transform.rotation);
        explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > timeToLaunch)
        {
            if (!launched)
            {
                setDirection();
                print(gameObject + " fire at "+targetPosition);

                resetForce();
                launched = true;

            }
            if (!collided)
            {
                travelToPosition();

            }

        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        collisionHandle(collision);
    }

    



    public void collisionHandle(Collider collision)
    {
        string tag = collision.gameObject.tag;
        if (tagTargets.Contains(tag))
        {
            //print(gameObject + " hit " + collision.gameObject);
            if (launched)
            {
                rigidbody.angularVelocity = new Vector3();
                rigidbody.velocity = new Vector3();
                collided = true;
                //Destroy(gameObject);
                detnation();
            }
        }
    }

    private void travelToPosition()
    {
        rigidbody.AddForce(direction * maxSpeed);
    }

    private void setDirection()
    {
        direction = targetPosition - transform.position;
        direction.Normalize();
    }

    public void setTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public void setInitialLaunch(Vector3 force, float ttl)
    {
        setLaunchForce(force);
        setTTL(ttl);
    }

    public void setLaunchForce(Vector3 force)
    {
        rigidbody.AddForce(force);
        launchForce = force;
    }

    public void setTTL(float ttl)
    {
        timeToLaunch = ttl;
    }

    private void resetForce()
    {
        rigidbody.AddForce(-launchForce);
        
    }
    
    public void detnation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        explosion.GetComponent<ParticleSystem>().Play();
        Target t;
        foreach (Collider nearByObject in colliders)
        {
            t = nearByObject.GetComponent<Target>();
            if (t != null)
            {
                t.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
        Destroy(explosion, 1.5f);

    }
}
