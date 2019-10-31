using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrder : MonoBehaviour
{
    SpriteRenderer SR;
    public static GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Player"))
            player = gameObject;
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SR.sortingOrder = (int) (-transform.position.y * 10);

        if (SR.sortingOrder > player.GetComponent<SpriteRenderer>().sortingOrder)
            gameObject.layer = 12;
        else
            gameObject.layer = 0;
    }
}
