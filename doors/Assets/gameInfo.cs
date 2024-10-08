using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class gameInfo : NetworkBehaviour
{
    public NetworkVariable<bool> red = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> blue = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> green = new NetworkVariable<bool>(false);
    public NetworkVariable<bool> yellow = new NetworkVariable<bool>(false);

    public override void OnNetworkSpawn()
    {
        if (red.Value)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (blue.Value)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (green.Value)
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
        if (yellow.Value)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
    }


    private void Start()
    {
        if (red.Value)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        if (blue.Value)
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (green.Value)
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
        if (yellow.Value)
        {
            transform.GetChild(3).gameObject.SetActive(false);
        }
        Screen.SetResolution(650, 720, false);
        red.OnValueChanged += SetRedClientRpc;
        blue.OnValueChanged += SetBlueClientRpc;
        green.OnValueChanged += SetGreenClientRpc;
        yellow.OnValueChanged += SetYellowClientRpc;
    }

    [ServerRpc(RequireOwnership =false)]
    public void SetRedServerRpc(bool val)
    {
        red.Value = val;
    }



    [ClientRpc]
    public void SetRedClientRpc(bool prev, bool val)
    {
        if (prev == val) return;
        transform.GetChild(0).gameObject.SetActive(!val);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetBlueServerRpc(bool val)
    {
        blue.Value = val;
    }

    [ClientRpc]
    public void SetBlueClientRpc(bool prev, bool val)
    {
        if (prev == val) return;
        transform.GetChild(1).gameObject.SetActive(!val);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetGreenServerRpc(bool val)
    {
        green.Value = val;
    }

    [ClientRpc]
    public void SetGreenClientRpc(bool prev, bool val)
    {
        if (prev == val) return;
        transform.GetChild(2).gameObject.SetActive(!val);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetYellowServerRpc(bool val)
    {
        yellow.Value = val;
    }

    [ClientRpc]
    public void SetYellowClientRpc(bool prev, bool val)
    {
        if (prev == val) return;
        transform.GetChild(3).gameObject.SetActive(!val);
    }
}
