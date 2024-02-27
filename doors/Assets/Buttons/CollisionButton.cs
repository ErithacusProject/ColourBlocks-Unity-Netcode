using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum colourclass
{
    red, 
    green,
    blue,
    yellow
}

public class CollisionButton : MonoBehaviour
{
    gameInfo gameInfo;
    [SerializeField] colourclass colour;

    private void Start()
    {
        gameInfo = FindAnyObjectByType<gameInfo>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") { return; }
        switch(colour)
        {
            case colourclass.red:
                gameInfo.SetRedServerRpc(true);
                break;
            case colourclass.green:
                gameInfo.SetGreenServerRpc(true);
                break;
            case colourclass.blue:
                gameInfo.SetBlueServerRpc(true);
                break;
            case colourclass.yellow: 
                gameInfo.SetYellowServerRpc(true);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") { return; }
        switch (colour)
        {
            case colourclass.red:
                gameInfo.SetRedServerRpc(false);
                break;
            case colourclass.green:
                gameInfo.SetGreenServerRpc(false);
                break;
            case colourclass.blue:
                gameInfo.SetBlueServerRpc(false);
                break;
            case colourclass.yellow:
                gameInfo.SetYellowServerRpc(false);
                break;
        }
        
    }
}
