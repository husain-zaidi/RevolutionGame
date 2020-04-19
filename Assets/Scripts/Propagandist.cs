using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Propagandist : MonoBehaviour
{
    public float speed = 5;
    public float convertLimit = 10;

    Rigidbody2D rigidbody;
    float moveLocationTimer;
    Vector2 movePosition;
    GameManager manager;
    float revertTimer = 5f;
    Oldie target;
    bool reverting = false;
    bool giveUp = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveLocationTimer = 0;
        movePosition = new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f));
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Flip sprite on movement direction
        if (movePosition.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        // Propagandist gives up
        if(manager.converts.Distinct().ToList().Count >= convertLimit)
        {
            giveUp = true;
            // frustrated animation
            transform.GetComponentInChildren<SpriteRenderer>().color = Color.gray;
            return;
        }

        // search nearest convert
        if(!reverting && manager.converts.Count > 0)
        {
            Oldie nearestConvert = manager.converts.Distinct().ToList()[0];
            float shortestDistance = float.MaxValue;
            foreach(Oldie convert in manager.converts.Distinct().ToList())
            {
                float dist = Vector3.Distance(transform.position, convert.transform.position);
                if (dist < shortestDistance)
                {
                    nearestConvert = convert;
                    shortestDistance = dist; 
                }
            }
            target = nearestConvert;
            reverting = true;

        }

        // go and convert 
        if(reverting)
        {
            speed = 7;
            Move(target.transform.position);
            if(Vector2.Distance(transform.position, target.transform.position) <= 0.8f)
            {
                revertTimer -= Time.deltaTime;
            }

            if(revertTimer <= 0)
            {
                target.converted = false;
                target.sprite.color = Color.yellow;
                manager.converts.RemoveAll(IsTarget);
                reverting = false;
                revertTimer = 5f;
            }

        }else
        {
            speed = 5;
            // Random movement
            if (moveLocationTimer <= 0)
            {
                movePosition = new Vector2(transform.position.x + Random.Range(-4f, 4f), transform.position.y + Random.Range(-4f, 4f));
                moveLocationTimer = 2;
            }
            Move(movePosition);
            moveLocationTimer -= Time.deltaTime;

        }

    }

    private bool IsTarget(Oldie oldie)
    {
        return (target.name.Equals(oldie.name));
    }

    // Generic Move function
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
