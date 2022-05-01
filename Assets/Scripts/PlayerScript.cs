using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private int health = 100;
    public Text healthText;
    public Slider healthSlider;

    public void takeDamage()
    {
        health -= 20;
        healthText.text = "health " + health + "%";
        healthSlider.value = health / 100f;

        if(health==0)
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
