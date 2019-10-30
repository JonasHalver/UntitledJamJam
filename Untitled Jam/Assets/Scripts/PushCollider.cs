using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCollider : MonoBehaviour
{

    public bool canPush;
    public bool pushing;
    GameObject pushObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canPush)
            {
               transform.parent.GetComponentInParent<PlayerController>().pushing = true;
                pushing = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            transform.parent.GetComponentInParent<PlayerController>().pushing = false;
            pushing = false;
        }

        if (pushing)
        {
            pushObject.transform.position = transform.position;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PushObject")
        {
            canPush = true;
            if (!pushing)
            {
                pushObject = other.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PushObject")
        {
            canPush = false;
            if (!pushing)
            {
                pushObject = null;
            }
        }
    }


}
