using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    public float speed = 5;

    Rigidbody2D rigidbody;
    float moveLocationTimer;
    Vector2 movePosition;
    GameObject player;
    bool chase = false;
    BoxCollider2D batonCollider;
    float hitTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveLocationTimer = 0;
        movePosition = new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f));
        player = GameObject.FindGameObjectWithTag("Player");
        batonCollider = transform.Find("BatonCollider").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movePosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        if (!chase)
        {
            //Player detection
            if (Input.GetButton("Fire1") && (Vector3.Distance(player.transform.position, transform.position) < 4))
            {
                chase = true;
            }

            if (moveLocationTimer <= 0)
            {
                movePosition = new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f));
                moveLocationTimer = 2;
            }
            Move(movePosition);
            moveLocationTimer -= Time.deltaTime;
        }
        else
        {
            speed = 8;
            movePosition = player.transform.position;
            Move(movePosition);

            if (Vector2.Distance(transform.position, movePosition) >= 0.8f)
            {
                Hit();
            }
            hitTimer -= Time.deltaTime;
        }
    }

    void Hit()
    {
        // play animation
        // enable collider
        if(hitTimer <= 0)
        {
            batonCollider.enabled = true;
            hitTimer = 2f;
        }
        
    }

    void Move(Vector2 destination)
    {
        Vector2 moveDirection;
        if (Vector2.Distance(transform.position, destination) >= 0.8f)
        {
            moveDirection = destination - rigidbody.position;
            rigidbody.MovePosition(rigidbody.position + (moveDirection.normalized * speed * Time.deltaTime));
        }
        else
        {
            //reached
            moveLocationTimer = 0;
        }
    }
}
