using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{

    public float maxHealth=1;
    public float startingHealth=1;
    float currentHealth;

    void Start()
    {
        currentHealth=startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth > maxHealth)currentHealth=maxHealth;

        if(currentHealth <= 0){
            Die();
        }
    }

    public float GetHealth(){
        return currentHealth;
    }

    public void Damage(float dmg){
        currentHealth -= dmg;

        // Damage resistances?
    }

    public void Heal(float heal){
        currentHealth += heal;
    }

    public void Die(){
        Debug.Log("Destroying: "+gameObject);
        Destroy(gameObject);

        // Death effect / Animation?
    }
}
