using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.MovePosition(new Vector2(rb.position.x + Input.GetAxis("Horizontal")*moveSpeed, rb.position.y + Input.GetAxis("Vertical")* moveSpeed));

        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            anim.SetBool("up", true);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            anim.SetBool("up", false);
        }

    }
}
