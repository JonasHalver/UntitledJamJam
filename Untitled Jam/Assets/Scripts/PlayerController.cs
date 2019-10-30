using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float pushSpeed;
    public bool pushing;
    float currentMoveSpeed;
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

        if (!pushing)
        {
            rb.MovePosition(new Vector2(rb.position.x + Input.GetAxis("Horizontal") * moveSpeed, rb.position.y + Input.GetAxis("Vertical") * moveSpeed));
        }
        else
        {
            rb.MovePosition(new Vector2(rb.position.x + Input.GetAxis("Horizontal") * pushSpeed, rb.position.y + Input.GetAxis("Vertical") * pushSpeed));
        }


        if (Input.GetAxis("Horizontal") > 0 && !pushing)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") < 0 && !pushing)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Vertical") > 0 && !pushing)
        {
            anim.SetBool("up", true);
            transform.GetChild(0).transform.localScale = new Vector3(1, -1, 1);
        }
        if (Input.GetAxis("Vertical") < 0 && !pushing)
        {
            anim.SetBool("up", false);
            transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);

        }

    }
}
