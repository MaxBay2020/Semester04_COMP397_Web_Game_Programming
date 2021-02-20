using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlaneController : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var controller = other.gameObject.GetComponent<CharacterController>();
            controller.enabled = false;
            other.gameObject.transform.position = spawnPoint.position;
            controller.enabled = true;
        }
    }
}
