using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //transform.position = cam.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goose"))
        {
            SpriteRenderer sr = collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            sr.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Goose"))
        {
            SpriteRenderer sr = collision.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            sr.enabled = false;
        }
    }
}
