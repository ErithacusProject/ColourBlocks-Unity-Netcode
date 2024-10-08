using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : NetworkBehaviour
{
    Rigidbody m_rigidBody;
    Vector3 m_movementDirection;
    [SerializeField] float m_movementForce;
    PlayerInput m_playerInput;
    Vector3 m_initialPos;

    public void SetInitialPos(Vector3 pos) { m_initialPos = pos; }

    public override void OnNetworkSpawn()
    {
        DontDestroyOnLoad(gameObject);
        m_rigidBody = GetComponent<Rigidbody>(); 
        if (!IsOwner) { return; }
        m_playerInput = GetComponent<PlayerInput>();
        m_playerInput.currentActionMap.FindAction("Movement").performed += StartMove;
        m_playerInput.currentActionMap.FindAction("Movement").canceled += StartEnd;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "Level 1") { return; }
        if (IsHost)
        {
            SetStartPosServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SetStartPosServerRpc(ServerRpcParams rpcParams = default)
    {
        gameObject.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
        m_rigidBody.velocity = Vector3.zero; 
        gameObject.transform.position = m_initialPos;
    }

    void StartMove(InputAction.CallbackContext context) 
    { 
        Vector2 dir = context.ReadValue<Vector2>();
        m_movementDirection = new Vector3(dir.x, 0f, dir.y);
        StartCoroutine(Move());
    }

    void StartEnd(InputAction.CallbackContext context)
    {
        m_movementDirection = Vector3.zero;
        MoveCharacterServerRpc(Vector3.zero);
    }

    IEnumerator Move()
    {
        while (m_movementDirection != Vector3.zero)
        {
            m_rigidBody.AddForce(m_movementDirection);
            m_rigidBody.velocity = Vector3.ClampMagnitude(m_rigidBody.velocity, 1f);
            MoveCharacterServerRpc(m_rigidBody.velocity);
            yield return new WaitForFixedUpdate();
        }
        m_rigidBody.velocity = Vector3.zero;
    }

    [ServerRpc]
    protected void MoveCharacterServerRpc(Vector3 velocity)
    {
        m_rigidBody.velocity = velocity;
    }

}
