using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Sprite twoThirds;
    public Sprite oneThird;
    public Sprite empty;

    Player player;
    Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthbar = GameObject.Find("HealthBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(player.health)
        {
            case 2:
                healthbar.sprite = twoThirds;
                break;

            case 1:
                healthbar.sprite = oneThird;
                break;

            case 0:
                healthbar.sprite = empty;
                break;
        }
    }
}
