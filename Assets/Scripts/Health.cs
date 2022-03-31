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

    public int team;

    private bool hasEntered = false;

    public GameObject text;
    public TextMesh t;

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
        text = new GameObject("Health");
        text.transform.SetParent(transform);
        text.transform.localPosition = Vector3.zero;
        t = text.AddComponent<TextMesh>();
        t.characterSize = 0.1f;
        t.fontSize = 250;
        t.color = Color.black;
        t.transform.localEulerAngles += new Vector3(45, -90, 0);
        t.transform.localPosition += new Vector3(0.0f, 5.0f, -1.5f);
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        t.text = currentHealth.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!hasEntered && type != ObjectType.TOWER 
            && collision.gameObject.tag == "Unit" && collision.gameObject.GetComponent<Health>().team != team)
        {
            hasEntered = true;
            currentHealth -= damageAmount;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (hasEntered && type != ObjectType.TOWER && collision.gameObject.tag == "Unit" 
            && collision.gameObject.GetComponent<Health>().team != team)
        {
            hasEntered = false;
        }
    }
}
