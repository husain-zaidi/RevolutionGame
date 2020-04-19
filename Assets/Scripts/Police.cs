using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    public float speed = 5;
    public float roamRange = 3f;

    Rigidbody2D rigidbody;
    float moveLocationTimer;
    Vector2 movePosition;
    GameObject player;
    bool chase = false;
    BoxCollider2D batonCollider;
    float hitTimer = 0;
    float trashCanCooldown = 8;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveLocationTimer = 0;
        movePosition = new Vector2(transform.position.x + Random.Range(-roamRange, roamRange), transform.position.y + Random.Range(-roamRange, roamRange));
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
            speed = 5;
            //Player detection
            if (Input.GetButton("Fire1") && (Vector3.Distance(player.transform.position, transform.position) < 4))
            {
                chase = true;
            }

            if (moveLocationTimer <= 0)
            {
                movePosition = new Vector2(transform.position.x + Random.Range(-roamRange, roamRange), transform.position.y + Random.Range(-roamRange, roamRange));
                moveLocationTimer = 2;
            }
            Move(movePosition);
            moveLocationTimer -= Time.deltaTime;
        }
        else
        {
            speed = 5.5f;
            movePosition = player.transform.position;
            Move(movePosition);

            if (Vector2.Distance(transform.position, movePosition) <= 0.8f && !player.GetComponent<Player>().inTrashCan)
            {
                rigidbody.velocity = Vector2.zero;
                Hit();
            }

            if(player.GetComponent<Player>().inTrashCan)
            {
                // stop chasing after cooldown
                trashCanCooldown -= Time.deltaTime;
                if(trashCanCooldown <= 0)
                {
                    chase = false;
                    trashCanCooldown = 8f;
                }
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
