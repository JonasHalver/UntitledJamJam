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
    Transform sprite;
    Transform pushCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(1);
        anim = sprite.GetComponent<Animator>();
        pushCollider = transform.GetChild(0);
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
            sprite.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") < 0 && !pushing)
        {
            sprite.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Vertical") > 0 && !pushing)
        {
            anim.SetBool("up", true);
            pushCollider.localScale = new Vector3(1, -1, 1);
        }
        if (Input.GetAxis("Vertical") < 0 && !pushing)
        {
            anim.SetBool("up", false);
            pushCollider.localScale = new Vector3(1, 1, 1);

        }

    }
}
