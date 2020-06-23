
using System;
using UnityEngine;
using Random = System.Random;

public class Target : MonoBehaviour
{
    Renderer rend;

    public float maxHealth = 50f;
    private float health;

    public bool dropItems = true;
    public GameObject ArmorDrop;
    public GameObject AmmoDrop;
    public GameObject ShotgunDrop;
    public GameObject NailgunDrop;
    private GameObject ItemDrop;

    public bool immune = false;

    public bool regenHealth = false;
    public float regenAmount = 1;

    public bool showColour = false;
    private Color armourColour;

    //public DamagePopup damagePopup;

    //public EnemyType enemyType;

    public bool tethered = false;
    private bool dead = false;





    private Random random = new Random();

    private static GameObject[] items = new GameObject[4];

    public void Start()
    {
        health = maxHealth;
        items[0] = ArmorDrop;
        items[1] = AmmoDrop;
        items[2] = ShotgunDrop;
        items[3] = NailgunDrop;
        rend = GetComponent<Renderer>();

    }

    public void Update()
    {
        if (health <= 0f & !immune)
        {
            Die();
        }
        if (regenHealth)
        {
            RegenerateHealth();
        }
        //print("target hp: "+ health);

    }

    public void TakeDamage(float amount)
    {
        if (amount > 0.5f)
        {
            //DamagePopup dp = Instantiate(damagePopup, transform.position + new Vector3(0, 10, 0), Quaternion.identity);
            //dp.Create(amount, amount > health);
        }
        if (health > -1)
        {
            health -= amount;

        }




    }


    private void Die()
    {
        dead = true;
        //decreaeGameManagerCounter();

        Destroy(gameObject);

        if (dropItems)
        {
            ItemDrop = items[random.Next(4)];
            Instantiate(ItemDrop, transform.position, ItemDrop.transform.rotation);
        }
        
    }

    private void decreaeGameManagerCounter()
    {
        //GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        //gm.GetComponent<GameManagerScript>().increaseKillCount();
    }

    private void RegenerateHealth()
    {
        if (health <= maxHealth)
        {
            health += Time.deltaTime * regenAmount;
            rend.material.SetColor("_Color", new Color(0.8f, 0.8f * (health / maxHealth), 0.8f * (health / maxHealth), 255));
            //rend.material.SetTexture()
        }
    }

    public void setTethered(bool b)
    {
        tethered = b;
    }

    public bool getTethered()
    {
        return tethered;
    }

    public bool isDead()
    {
        return dead;
    }

    /*
    public EnemyType getEnemyType()
    {
        return enemyType;
    }
    */
}




