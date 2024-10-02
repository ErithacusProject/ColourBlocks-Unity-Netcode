using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : NetworkBehaviour
{
    int collisions;
    [SerializeField] GameObject endlevelui;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collisions += 1;
            if (collisions == 2)
            {
                
                    Application.Quit();
                
            }
        }
    }

    [ClientRpc]
    void ChangeLevelClientRpc()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            collisions -= 1;
        }
    }
}
