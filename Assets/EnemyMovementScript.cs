using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementScript : MonoBehaviour
{
    public GameObject target;
    public float range;

    public NavMeshAgent agent;
    public GameObject enemy;
    public int enemyAmount;
    public float spawnRange = 12f;
    public float damagePerEnemy = 10;
    public float attackRange = 15;
    [SerializeField] GameObject[] enemyArray;
    //public Animator animator;
    [SerializeField] bool attack = false;
    public float timeBeteenAttack = 1f;
    [SerializeField] float timeNow;
    [SerializeField] Collision c;
    public string c_name;
    // Update is called once per frame

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Core");
        agent.SetDestination(target.transform.position);
        enemyArray = new GameObject[enemyAmount];
        for (int i = 0; i < enemyAmount; i++)
        {
            enemyArray[i] = Instantiate(enemy, transform);
            enemyArray[i].transform.position = transform.position + new Vector3(spawnRange / enemyAmount * i, 0, 0);
            
        }
        var rand = new System.Random();
        //enemyArray= enemyArray.OrderBy(x => Random.Next(0,1)).ToList();
        reshuffle();
        int j = 0;
        foreach (GameObject e in enemyArray)
        {
            e.transform.RotateAround(transform.position, transform.up, 360f / (enemyAmount - 1) * j);
            e.transform.forward = transform.forward;
            j++;
        }
    }
    void Update()
    {
        //animator.SetBool("attack", attack);
        if (attack&& Time.time - timeNow > timeBeteenAttack)
        {
            agent.isStopped = true;
            if (!attackCore())
            {
                attack = false;
                agent.isStopped = false;

            }
            timeNow = Time.time;
            
        }
        
        //print(attack + " ,  " + agent.isStopped);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(target))
        {
            print("hit target");
            attack = true;
            c = collision;
        }
        
    }

    void reshuffle()
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < enemyArray.Length; t++)
        {
            GameObject tmp = enemyArray[t];
            int r = Random.Range(t, enemyArray.Length);
            enemyArray[t] = enemyArray[r];
            enemyArray[r] = tmp;
        }
    }

    bool attackCore()
    {
        c_name = c.gameObject.name;
        target.GetComponent<CoreStatesScript>().takeDamage(damagePerEnemy * enemyAmount);
        foreach(GameObject e in enemyArray)
        {
            e.GetComponentInChildren<Animator>().Play("Attack");
        }
        //print(Vector3.Distance(target.transform.position, transform.position));
        if (Vector3.Distance(target.transform.position, transform.position)>attackRange)
        {
            print("out of range "+ Vector3.Distance(target.transform.position, transform.position));
            return false;
        }
        return true;
    }

}
