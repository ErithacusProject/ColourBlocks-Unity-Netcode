using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //endlevelui.SetActive(true);
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
