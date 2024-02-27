using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelScript : MonoBehaviour
{
    int collisions;

    public event System.Action LevelFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collisions += 1;
            if (collisions == 2)
            {
                LevelFinished?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collisions -= 1;
        }
    }
}
