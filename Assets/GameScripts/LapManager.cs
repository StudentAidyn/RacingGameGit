using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints;
    public int totalLaps;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("IN_TRIGGER");
        if (collision.gameObject.GetComponent<CarController>())
        {
            CarController player = collision.gameObject.GetComponent<CarController>();
            if (player.checkpointIndex == checkpoints.Count)
            {
                //if
                player.checkpointIndex = 0;
                player.lapNumber++;

                Debug.Log($"You are now on lap{player.lapNumber} out of {totalLaps}");

                if (player.lapNumber > totalLaps)
                {
                    player.lapNumber = 1;
                    Debug.Log("YOU WON!!");
                }
            }
        }
    }
}


//https://www.youtube.com/watch?v=F1JRy8nFTb4&ab_channel=BMo