using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oldie : MonoBehaviour
{
    public float moveDelay = 5;
    public float speed = 5;

    Rigidbody2D rigidbody;
    bool converted;
    float moveLocationTimer;
    Vector2 movePosition;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        converted = false;
        moveLocationTimer = 0;
        movePosition = new Vector2(transform.position.x + Random.Range(-8f, 8f), transform.position.y + Random.Range(-8f, 8f));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2();
        // decide movement
        if (!converted)
        {
            
            if (moveLocationTimer <= 0)
            {
                movePosition = new Vector2(transform.position.x + Random.Range(-8f, 8f), transform.position.y + Random.Range(-8f,8f));
                moveLocationTimer = moveDelay;
            }

            rigidbody.MovePosition(movePosition);

            moveLocationTimer -= Time.deltaTime;
        }
        //color the guy
        // collision
        //Vector2 position = rigidbody.position;
        //rigidbody.MovePosition(position + (move * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Bullet"))
        {

        }
    }
}
