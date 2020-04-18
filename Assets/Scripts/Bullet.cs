using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 30;
    float timer = 10;
    public int flip;
    // Start is called before the first frame update
    void Start()
    {
        //if (transform.localRotation.z < 0)
        //    flip = 1;
        //else
            flip = -1;
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
        Debug.Log("Bullet collided! " + collision);
    }
}
