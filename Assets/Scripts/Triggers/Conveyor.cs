using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public GameObject belt;
    public Transform endPoint;
    public float conveySpeed;

    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, endPoint.position, conveySpeed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
