using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
 
    private int health = 100;
    public Text healthText, scoreText;
    public Slider healthSlider;
    public Camera myCamera;
    private AudioSource s;
    public AudioClip attackSound, collectSound;
    private int score = 0;
    private static int currentLevel = 1;

    public void Start()
    {
        s = myCamera.GetComponent<AudioSource>();
       
        
    }

    public void takeDamage()
    {
        health -= 20;
        healthText.text = "health " + health + "%";
        healthSlider.value = health / 100f;
         transform.localScale= new Vector3(Random.Range(-1 ,-2.5f),1, Random.Range(-1, -2.5f));
        s.PlayOneShot(attackSound);

        if (health == 0)
        {
            SceneManager.LoadScene("Lose");
        }
    }
    public void OnTriggerEnter(Collider x)
    {
        
        if (x.tag == "Pickup")
        {
            s.PlayOneShot(collectSound);
            score++;
            
             
            scoreText.text = "Score: " + score;
            x.gameObject.SetActive(false);
            if (health < 100F)
            {
                health += 20;
                healthText.text = "health " + health + "%";
                healthSlider.value = health / 100f;
            }
                if (score == 8)
                {
                    currentLevel++;
                    if (currentLevel == 3)
                    {
                        SceneManager.LoadScene("Win");
                    }
                    SceneManager.LoadScene("Level" + currentLevel);

                }
            }
        }
    }

