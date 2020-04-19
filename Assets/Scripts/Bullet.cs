using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 70;
    float timer = 5;

    public int flip;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        if (mousePos.x > 0) {
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
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Oldies"))
            Destroy(gameObject);
    }
}