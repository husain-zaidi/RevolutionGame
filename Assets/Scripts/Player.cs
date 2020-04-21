using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3; 
    public float speed;
    public Transform handPosition;
    public bool inTrashCan = false;
    public int scareAmmo = 5;
    public int viralAmmo = 5;
    public AudioClip hitAudio;


    Rigidbody2D rigidbody;
    Collider2D bounds;
    AudioSource audio;

    float trashcanCooldown = 2f;
    float damageCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        bounds = GetComponent<Collider2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            Vector2 move = new Vector2();
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

            mousePos.x = mousePos.x - objectPos.x;

            if (mousePos.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }

            if (inTrashCan)
            {
                if (Input.GetKey(KeyCode.C) && trashcanCooldown <= 0)
                {
                    transform.position = transform.position - Vector3.right * 2 - Vector3.forward * 4;
                    bounds.enabled = true;
                    inTrashCan = false;
                    trashcanCooldown = 2f;
                }
                trashcanCooldown -= Time.deltaTime;
                return;
            }

            Vector2 position = rigidbody.position;
            rigidbody.MovePosition(position + (move * speed * Time.deltaTime));

            damageCooldown -= Time.deltaTime;
        }
        if(health <= 0)
        {
            if(Input.GetButton("Fire1"))
            {
                health = 3;
                transform.position = new Vector3(0.26f, 12.06f, 0);
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.tag.Equals("ScareAmmo"))
        {
            scareAmmo += 5;
            Destroy(other.gameObject);
        }

        if (other.tag.Equals("ViralAmmo"))
        {
            viralAmmo += 5;
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("TrashCan") && Input.GetKey(KeyCode.C))
        {
            transform.position = other.transform.position + Vector3.forward * 4;
            bounds.enabled = false;
            inTrashCan = true;
        }

        if (other.tag.Equals("Baton") && damageCooldown <= 0)
        {
            audio.PlayOneShot(hitAudio);
            health--;
            damageCooldown = 1f;
        }

    }



}
