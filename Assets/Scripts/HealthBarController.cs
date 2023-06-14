using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // this is required to load a new scene

public class HealthBarController : MonoBehaviour
{
    public Slider healthSlider; // reference to slider component on the game object
    public GameObject player; // reference so can call functions from scripts attached to player
    public GameObject HUD; // same but so can call scripts from canvas

    void Update()
    {
        //if (player.GetComponent<PlayerMovement>().currentHP <= 0)
        //{
        //    SceneManager.LoadScene(0);
       // }
    }
    public void SetMaxHealth(float health) // gets health value from passed in value of maxHealth from movement script
    {
        healthSlider.maxValue = health; // Sets value of health bar slider to value of health
        healthSlider.value = health; // sets the actual visual health as the value of the passed in variable
        Debug.Log(health);
    }

    public void AddHealth(string tag)
    {
        if (player.GetComponent<PlayerMovement>().currentHP > 50)
        {
            player.GetComponent<PlayerMovement>().currentHP = 100; // ensure player does not get over 100 hp
            healthSlider.value = player.GetComponent<PlayerMovement>().currentHP;
        }
        else
        {
            player.GetComponent<PlayerMovement>().currentHP += 50;
            healthSlider.value = player.GetComponent<PlayerMovement>().currentHP;
        }
    }

    void SelfTerminate()
    {
        Destroy(gameObject);
    }
}
