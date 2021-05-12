using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Use : MonoBehaviour
{

    public Camera cam;

    private Health health;
    private int limitUsesHealth = 3;
    public AudioSource audioHeal;

    public AudioClip voice;

    private GameObject trap01;
    private GameObject trap02;


    public Animator transitionEnd;

    private bool hasKey = false;

    private void Start()
    {
        trap01 = GameObject.Find("ElectricTrap_01");
        trap02 = GameObject.Find("ElectricTrap_02");
        health = GameObject.Find("Player").GetComponent<Health>();
    }
    // Update and get the 'Mouse 1' input
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            //When clicked, run the method 'use'
            use();
        }
    }

    void use()
    {
        //Raycast for where the player is looking
        RaycastHit hit;
        //If the raycast hit an object...

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2f))
        {
            //Debug logs for what object is hit with the tag
            Debug.Log("Object: " + hit.transform.gameObject);
            Debug.Log("Tag: " + hit.transform.tag);

            //If its a screwdriver, disable the help text and destroy the object
            if (hit.transform.gameObject.tag == "Item_Screwdriver")
            {
                Destroy(hit.transform.gameObject);
            }

            //If its a vent and the screwdriver doesn't exist (means its picked up)
            //Desrtoy the vent after 3 seconds
            if (hit.transform.tag == "Useable_Vent" && !GameObject.FindGameObjectWithTag("Item_Screwdriver"))
            {
                Debug.Log("Vent Hit");
                Rigidbody ventRigid = hit.transform.GetComponent<Rigidbody>();

                Destroy(hit.transform.gameObject, 3f);
            }

            //If its a medkit, heal the player
            if (hit.transform.gameObject.tag == "Useable_Medkit")
            {
                Debug.Log("Medkit Hit");

                //if health is less than 100 and limit is greater than 0
                //then give health to the player and subtract the limit
                if (health.GetHealth() < 100 && limitUsesHealth > 0)
                {
                    limitUsesHealth--;
                    health.AddHealth(30);

                    if (!audioHeal.isPlaying)
                    {
                        Debug.Log("Playing medkit sound");
                        audioHeal.Play();
                    }
                }
            }

            //If its a radio, destroy the object
            if (hit.transform.gameObject.tag == "Item_Radio")
            {
                Destroy(hit.transform.gameObject);
                Destroy(GameObject.Find("Door_Unlockable"));
                AudioSource audio = GetComponentInChildren<AudioSource>();
                audio.clip = voice;
                audio.Play();
            }

            //If its a capsule, destroy the object
            if (hit.transform.gameObject.tag == "Item_Capsule")
            {
                Destroy(hit.transform.gameObject);

            }

            //If its a capsule, destroy the object and set bool to true
            if (hit.transform.gameObject.tag == "Item_Key")
            {
                Destroy(hit.transform.gameObject);
                hasKey = true;
            }

            //If its a switchbox, switch one trap off and the other trap on
            if (hit.transform.gameObject.tag == "Useable_Switchbox")
            {

                Debug.Log(trap01);
                Debug.Log(trap02);

                if (trap01.activeSelf)
                {
                    trap01.SetActive(false);
                    trap02.SetActive(true);

                }
                else if(trap02.activeSelf)
                {
                    trap01.SetActive(true);
                    trap02.SetActive(false);  
                }
            }

            //If its a jeep, fade and end the game
            if (hit.transform.gameObject.tag == "Useable_Jeep" && hasKey)
            {
                StartCoroutine(endGame());
            }

        }
    }

    IEnumerator endGame()
    {
        transitionEnd.SetTrigger("Start");

        yield return new WaitForSeconds(0.75f);

        SceneManager.LoadScene("MainMenu");
    }
}
