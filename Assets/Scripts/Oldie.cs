using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldie : MonoBehaviour
{
    public float moveDelay = 5;
    public float speed = 5;

    Rigidbody2D rigidbody;
    public bool converted;
    float moveLocationTimer;
    Vector2 movePosition;
    SpriteRenderer sprite;
    Propagator propagator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        converted = false;
        moveLocationTimer = 0;
        movePosition = new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f));
        sprite = GetComponentInChildren<SpriteRenderer>();
        propagator = GetComponentInChildren<Propagator>();
    }

    // Update is called once per frame
    void Update()
    {
        // decide movement
        if (!converted)
        {
            if (moveLocationTimer <= 0)
            {
                movePosition = new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f));
                moveLocationTimer = moveDelay;
            }

            if(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 4)
            {
                moveDelay = 2f;
            }
            else
            {
                moveDelay = 5f;
            }


            Move(movePosition);

            moveLocationTimer -= Time.deltaTime;
        }
        //Vector2 position = rigidbody.position;
        //rigidbody.MovePosition(position + (move * speed * Time.deltaTime));
    }

    void Move(Vector2 destination)
    {
        Vector2 moveDirection;
        if (Vector2.Distance(transform.position,destination) >= 0.8f)
        {
            moveDirection = destination - rigidbody.position;
            rigidbody.MovePosition(rigidbody.position + (moveDirection.normalized * speed * Time.deltaTime));
        }else
        {
            //reached
            moveLocationTimer = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Bullet"))
        {
            propagator.Propagate();
            converted = true;
            sprite.color = Color.white;
            rigidbody.velocity = Vector2.zero;
        }

        if(other.gameObject.tag.Equals("Shard"))
        {
            converted = true;
            sprite.color = Color.white;
            rigidbody.velocity = Vector2.zero;
        }
    }
}
