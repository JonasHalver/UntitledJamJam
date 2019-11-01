using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static int health = 100;

    public KeyCode scareButton;
    bool canScare = true;
    public float scareCooldown = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canScare)
        {
            if (Input.GetKeyDown(scareButton))
            {
                SpookGooses();
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSecondsRealtime(scareCooldown);
        canScare = true;
    }

    public void SpookGooses()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, GooseAI.acceptableDistanceToPlayer);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Goose"))
            {
                col.gameObject.SendMessage("Spook");
                print("Spooked " + col.gameObject.ToString());
            }
        }
    }

    public void Scare(int damage)
    {
        health -= damage;
    }
}
