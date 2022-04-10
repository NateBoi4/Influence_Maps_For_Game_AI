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
    public float combatPeriod;

    public int team;

    public GameObject text;
    public TextMesh t;

    private void Start()
    {
        team = gameObject.GetComponent<Propogation>().squadNo;
        text = new GameObject("Health");
        text.transform.SetParent(transform);
        text.transform.localPosition = Vector3.zero;
        t = text.AddComponent<TextMesh>();
        t.characterSize = 0.1f;
        t.fontSize = 150;
        t.color = Color.green;
        switch (type)
        {
            case ObjectType.NONE: break;
            case ObjectType.TOWER:
                maxHealth = 500;
                damageAmount = 50;
                combatPeriod = 5.0f;
                t.transform.localPosition += new Vector3(-1.0f, 1.0f, 0.0f);
                break;
            case ObjectType.UNIT:
                maxHealth = 100;
                damageAmount = 10;
                combatPeriod = 3.0f;
                t.transform.localPosition += new Vector3(0.0f, 5.0f, 0.0f);
                break;
            default: break;
        }
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            if(type == ObjectType.TOWER)
            {
                if(team == 0)
                {
                    GameManager.DestroyRed();
                }
                else if(team == 1)
                {
                    GameManager.DestroyBlue();
                }
            }
            gameObject.GetComponent<Propogation>().Dead();
            Destroy(gameObject);
        }
        if(currentHealth <= 50 && currentHealth > 25)
        {
            t.color = Color.yellow;
        }
        else if(currentHealth <= 25 && currentHealth > 0)
        {
            t.color= Color.red;
        }
        else
        {
            t.color= Color.green;
        }
        t.transform.LookAt(2.0f * transform.position - Camera.main.transform.position);
        t.text = currentHealth.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Health enemy = collision.gameObject.GetComponent<Health>();
        if (enemy)
        {
            if (enemy.team != team)
            {
                StartCoroutine(DoCombat(enemy));
            }
            else
            {
                enemy = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Health enemy = other.gameObject.GetComponent<Health>();
        if (enemy)
        {
            if (enemy.team != team)
            {
                StartCoroutine(DoCombat(enemy));
            }
            else
            {
                enemy = null;
            }
        }
    }

    private IEnumerator DoCombat(Health enemy)
    {
        while (enemy)
        {
            enemy.currentHealth -= damageAmount;
            yield return new WaitForSeconds(combatPeriod);
        }
    }

    private IEnumerator HealUnit()
    {
        yield return new WaitForSeconds(combatPeriod);
    }
}
