using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 60;
    float timer = 5;

    public int flip;
    int counter = 3;

    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player.localScale.x <= 0) {
            flip = 1;
        }
        else {
            flip = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        transform.Translate(flip * speed * Time.deltaTime, 0, 0);
        if (timer <= 0 || counter <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("Citizen"))
            --counter;
    }
}