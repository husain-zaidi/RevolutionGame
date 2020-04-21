using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject scareBullet;
    public GameObject viralBullet;

    public Transform bulletHole;
    public AudioClip noAmmo;
    public AudioClip shootAudio;
    public int mode = 0;

    bool inHand = true;
    Player player;
    AudioSource audio;
    float angle;
    float shootTimer = 0;
    private float keyDelay;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inHand && player.health > 0)
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
            if(Input.GetKey(KeyCode.F) && keyDelay <= 0)
            {
                if (mode == 0)
                    mode = 1;
                else
                    mode = 0;
                keyDelay = 0.3f;
                Debug.Log("Mode: " + mode);
            }

            keyDelay -= Time.deltaTime;
            shootTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        if (shootTimer <= 0)
        {
            if(mode == 0)
            {
                if (player.scareAmmo > 0)
                {
                    player.scareAmmo--;
                    audio.PlayOneShot(shootAudio);
                    GameObject iBullet = Instantiate(scareBullet, bulletHole.position, Quaternion.Euler(new Vector3(0, 0, angle)));
                }
                else
                {
                    audio.PlayOneShot(noAmmo);
                }
            }
            else if (mode == 1)
            {
                if (player.viralAmmo > 0)
                {
                    GameObject iBullet = Instantiate(viralBullet, bulletHole.position, Quaternion.Euler(new Vector3(0, 0, angle)));
                    player.viralAmmo--;
                    audio.PlayOneShot(shootAudio);
                }
                else
                {
                    audio.PlayOneShot(noAmmo);
                }
            }
            
            shootTimer = 0.5f;
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
