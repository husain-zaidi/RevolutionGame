using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletHole;
    bool inHand = true;
    Player player;
    float angle;

    float shootTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inHand)
        {
            //transform.position = player.handPosition.position;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x =  objectPos.x -mousePos.x;
            mousePos.y =  objectPos.y -mousePos.y;

            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            if (player.transform.localScale.x < 0)
                angle -= 180f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }

            shootTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {

        if (shootTimer <= 0)
        {
            GameObject iBullet = Instantiate(bullet, bulletHole.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            if (player.transform.localScale.x < 0)
                iBullet.GetComponent<Bullet>().flip = 1;
            else
                iBullet.GetComponent<Bullet>().flip = -1;
            //iBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            shootTimer = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player = other.GetComponent<Player>();

        if (player != null)
        {
            inHand = true;
        }
    }
}
