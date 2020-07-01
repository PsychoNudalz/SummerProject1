using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreStatesScript : MonoBehaviour
{
    public float maxHealth;
    [SerializeField] float health;
    [SerializeField] bool dead;
    public new Renderer renderer;

    private void Awake()
    {
        health = maxHealth;
        updateRenderer();
    }

    private void Update()
    {
    }

    // Update is called once per frame
    public void takeDamage(float damage)
    {

        health -= damage;
        if (maxHealth<= 0)
        {
            dead = true;
        }
        updateRenderer();
        print(health);
    }

    void updateRenderer()
    {
        renderer.material.SetFloat("_FactorValue", health / maxHealth);
    }
}
