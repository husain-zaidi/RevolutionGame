using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Sprite twoThirds;
    public Sprite oneThird;
    public Sprite empty;
    public List<Citizen> converts;

    Player player;
    Image healthbar;
    TextMeshProUGUI ammo;
    TextMeshProUGUI death;
    TextMeshProUGUI win;


    string mode = "Scare";

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        healthbar = GameObject.Find("HealthBar").GetComponent<Image>();
        ammo = GameObject.Find("Ammo").GetComponent<TextMeshProUGUI>();
        death = GameObject.Find("Death").GetComponent<TextMeshProUGUI>();
        win = GameObject.Find("Win").GetComponent<TextMeshProUGUI>();
        win.enabled = false;
        death.enabled = false;
        converts = new List<Citizen>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(player.GetComponentInChildren<Gun>().mode)
        {
            case 0:
                mode = "Scare";
                break;

            case 1:
                mode = "Viral";
                break;
        }

        if(player.health <= 0)
        {
            // show death screen
            death.enabled = true;
        }
        else
        {
            death.enabled = false;
        }
        
        if(converts.Count >= 17)
        {
            win.enabled = true;
            player.health = 0;
        }

        //UI
        ammo.text = "Mode :" + mode + "\n" 
                   +"Scare: " + player.scareAmmo + "\n" 
                   +"Viral: " + player.viralAmmo + "\n"
                   +"Converts: " + converts.Count;
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
