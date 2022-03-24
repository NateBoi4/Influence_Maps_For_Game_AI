using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType { NONE, TOWER, UNIT };

public class Health : MonoBehaviour
{
    public int currentHealth;
    public int damageAmount;
    public int maxHealth;
    public ObjectType type;

    private bool hasEntered = false;

    private void Start()
    {
        switch (type)
        {
            case ObjectType.NONE: break;
            case ObjectType.TOWER:
                maxHealth = 500;
                damageAmount = 50;
                break;
            case ObjectType.UNIT:
                maxHealth = 100;
                damageAmount = 10;
                break;
            default: break;
        }
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!hasEntered && type != ObjectType.TOWER && collision.gameObject.tag == "Unit")
        {
            hasEntered = true;
            currentHealth -= damageAmount;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hasEntered && type != ObjectType.TOWER && collision.gameObject.tag == "Unit")
        {
            hasEntered = false;
        }
    }
}
