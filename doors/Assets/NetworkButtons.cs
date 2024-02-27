using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkButtons : NetworkBehaviour
{
    [SerializeField] Button HostBtn;
    [SerializeField] Button ServerBtn;
    [SerializeField] Button ClientBtn;
    [SerializeField] GameObject playerPrefab;
    gameInfo gameInfo;

    //ulong ClientID = 0;

    private void Awake()
    {
        gameInfo = FindAnyObjectByType<gameInfo>();
        HostBtn.onClick.AddListener( () => {  NetworkManager.Singleton.StartHost(); CreatePlayerServerRpc(true); });
        ServerBtn.onClick.AddListener( () => { NetworkManager.Singleton.StartServer(); });
        ClientBtn.onClick.AddListener( () => { NetworkManager.Singleton.StartClient(); CreatePlayerServerRpc(false); });
    }

    [ServerRpc(RequireOwnership = false)]
    private void CreatePlayerServerRpc(bool isHost, ServerRpcParams rpcParams = default)
    {
        gameObject.SetActive(false);
        GameObject player = Instantiate(playerPrefab);
        if (isHost)
        {
            player.transform.position = new Vector3(0, 1.5f, 1);
            gameInfo.SetYellowServerRpc(true);
        }
        else
        {
            player.transform.position = new Vector3(12, 1.5f, 1);
            gameInfo.SetBlueServerRpc(true);
        }
        player.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
    }

    

}
