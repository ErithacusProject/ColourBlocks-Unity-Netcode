using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LoadPlayersIntoLevel : NetworkBehaviour
{
    [SerializeField] GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayerServerRpc(IsHost);
    }

    [ServerRpc(RequireOwnership = false)]
    private void CreatePlayerServerRpc(bool isHost, ServerRpcParams rpcParams = default)
    {
        GameObject player = Instantiate(playerPrefab);
        if (isHost)
        {
            Vector3 startPos = new Vector3(0, 1.5f, 1);
            player.transform.position = startPos;
            player.GetComponent<PlayerMovement>().SetInitialPos(startPos);
        }
        else
        {
            Vector3 startPos = new Vector3(12, 1.5f, 1);
            player.transform.position = startPos;
            player.GetComponent<PlayerMovement>().SetInitialPos(startPos);

        }
        player.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
    }
}
