using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Triggers : MonoBehaviour
{
    //Variables
    public string level;

    public Animator transition;

    public float transitionTime;

    public GameObject capsule;

    public GameObject key;
    private void Start()
    {

    }

    //On triggered, do one of the actions that the gameObject is named
    private void OnTriggerEnter(Collider other)
    {
        //If its a level trigger, change level based on variable and start coroutine
        if (transform.name == "Trigger_level")
        {

            StartCoroutine(changeLevel(level));
        }

        //If its a conveyor trigger, do these actions (HARDCODED)
        if (transform.name == "Trigger_ConveyorActive" && !GameObject.Find("Capsule"))
        {
            StartCoroutine(startConveyor());
        }

    }

    IEnumerator changeLevel(string levelName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelName);
    }

    IEnumerator startConveyor()
    {
        capsule.SetActive(true);

        yield return new WaitForSeconds(1f);

        key.SetActive(true);
        Destroy(this.gameObject);
    }



}
