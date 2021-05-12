using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    //Ints for current and maximum health
    public int curHealth;
    public int maxHealth = 100;

    public int timeForNextHit;

    public AudioClip clipHurt;

    public AudioSource audioSrc;
    private int nextHit;

    //Get the slider that displays the health
    public Slider healthSlider;

    //Lerping time variable
    public float LerpTime;

    //Set values on start
    void Start()
    {
        audioSrc.clip = clipHurt;
        curHealth = maxHealth;
        healthSlider.maxValue = curHealth;
        healthSlider.value = curHealth;
    }

    //Sets the maximum health (probably won't be needed)
    public void SetMaxHealth(int newMax)
    {
        maxHealth = newMax;
        healthSlider.value = curHealth;
    }

    //Sets the health
    public void SetHealth(int newHP)
    {
        curHealth = newHP;
        healthSlider.value = curHealth;
    }
    //Add to the current health
    public void AddHealth(int HP)
    {
        curHealth += HP;
        healthSlider.value = curHealth;
    }

    //Get the current health
    public int GetHealth()
    {
        return curHealth;
    }

    //Take health away as the player is hurt
    public void TakeDamage(int damageToDeal)
    {
        curHealth -= damageToDeal;
        audioSrc.Play();
    }

    //Handles the health and next hits
    void Update()
    {
        if (nextHit > 0)
        {
            nextHit -= 1;
        } else
        {
            nextHit = 0;
        }

        if (healthSlider.value != curHealth)
        {
            healthSlider.value = curHealth;
        }

        if (curHealth <= 0)
        {
            //Hard coded since only this level contains hazards
            SceneManager.LoadScene("Level_02");
        }
    }

    //On triggered with a trigger_hurt, run this method
    private void OnTriggerStay(Collider hurtTrigger)
    {
        if (hurtTrigger.transform.name == "Trigger_Hurt" && nextHit <= 0)
        {
            Debug.Log("Player hit a hurt trigger");
            TakeDamage(10);
            nextHit = timeForNextHit;
        }
    }

}
