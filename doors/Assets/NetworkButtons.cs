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
    int clickCount = 0;

    //ulong ClientID = 0;

    private void Awake()
    {
        HostBtn.onClick.AddListener( () => {  NetworkManager.Singleton.StartHost(); CreatePlayerServerRpc(true); gameObject.SetActive(false); });
        ServerBtn.onClick.AddListener( () => { NetworkManager.Singleton.StartServer(); });
        ClientBtn.onClick.AddListener( () => { NetworkManager.Singleton.StartClient();  CreatePlayerServerRpc(false); clickCount++; if (clickCount > 1) gameObject.SetActive(false); });
    }

    [ServerRpc(RequireOwnership = false)]
    private void CreatePlayerServerRpc(bool isHost, ServerRpcParams rpcParams = default)
    {
        GameObject player = Instantiate(playerPrefab);
        if (isHost)
        {
            player.transform.position = new Vector3(0, 1.5f, 1);
        }
        else
        {
            player.transform.position = new Vector3(12, 1.5f, 1);
            
        }
        player.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
    }

    





}
