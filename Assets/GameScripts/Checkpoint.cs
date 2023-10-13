using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public int index;
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<CarController>())
        {
            CarController player = collision.gameObject.GetComponent<CarController>();
            if(player.checkpointIndex == index - 1)
            {
                player.checkpointIndex = index;
                player.CurrentCP = spawnPoint;
            }
        }
    }
}
