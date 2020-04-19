using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 3; 
    public float speed;
    public Transform handPosition;
    public bool inTrashCan = false;

    Rigidbody2D rigidbody;
    Collider2D bounds;

    float trashcanCooldown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        bounds = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2();
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        Vector3 mousePos = Input.mousePosition;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        
        if (mousePos.x > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            transform.localScale = Vector3.one;
        }

        if(inTrashCan)
        {
            if(Input.GetKey(KeyCode.C) && trashcanCooldown <= 0)
            {
                transform.position = transform.position - Vector3.right*2 - Vector3.forward * 4;
                bounds.enabled = true;
                inTrashCan = false;
                trashcanCooldown = 2f;
            }
            trashcanCooldown -= Time.deltaTime;
            return;
        }
    
        Vector2 position = rigidbody.position;
        rigidbody.MovePosition(position + (move * speed * Time.deltaTime));

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Baton"))
        {
            health--;
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
    }



}
