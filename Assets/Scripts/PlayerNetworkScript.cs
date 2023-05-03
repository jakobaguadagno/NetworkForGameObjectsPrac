using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerNetworkScript : NetworkBehaviour
{

    private Vector2 playerDirection = Vector2.zero;
    [SerializeField] private float playerSpeed = 10;
    [SerializeField] private Transform networkedBall;
    //private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1);

    public void OnDirectionChanged(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started || context.action.phase == InputActionPhase.Performed )
        {
            if(IsHost && IsOwner)
            {
                Debug.Log("Moving Host");
                playerDirection = context.ReadValue<Vector2>();
            }
            if(IsClient && !IsHost && IsOwner)
            {
                PlayerMovementServerRpc(context.ReadValue<Vector2>());
            }
            
        }
        if(context.action.phase == InputActionPhase.Canceled)
        {
            if(IsHost && IsOwner)
            {
                Debug.Log("Moving Host");
                playerDirection = Vector2.zero;
            }
            if(IsClient && !IsHost && IsOwner)
            {
                PlayerMovementServerRpc(Vector2.zero);
            }
            
        }
    }

    public void OnBallSpawn(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started)
        {
            if(IsHost && IsOwner)
            {
                Transform spawnBall = Instantiate(networkedBall, gameObject.transform);
                spawnBall.GetComponent<NetworkObject>().Spawn(true);
            }
            if(IsClient && !IsHost && IsOwner)
            {
                PlayerSpawnBallServerRpc();
            }
            
        }
    }

    public void OnBallDelete(InputAction.CallbackContext context)
    {
        if(context.action.phase == InputActionPhase.Started)
        {
            if(IsHost && IsOwner)
            {
                Transform spawnBall = Instantiate(networkedBall, gameObject.transform);
                spawnBall.GetComponent<NetworkObject>().Spawn(true);
            }
            if(IsClient && !IsHost && IsOwner)
            {
                PlayerSpawnBallServerRpc();
            }
            
        }
    }
    
    void Update()
    {
        if(IsServer || IsHost && IsOwner)
        {
            
            if(playerDirection != Vector2.zero)
            {
                transform.position += new Vector3(playerDirection.x,0,playerDirection.y) * playerSpeed * Time.deltaTime;
            }
        }
    }

    [ServerRpc]
    private void PlayerMovementServerRpc(Vector2 dir)
    {
        Debug.Log("Moving Player: " + OwnerClientId);
        playerDirection = dir;
    }
    [ServerRpc]
    private void PlayerSpawnBallServerRpc()
    {
        Debug.Log("Spawning Ball For: " + OwnerClientId);
        Transform spawnBall = Instantiate(networkedBall, gameObject.transform);
        spawnBall.GetComponent<NetworkObject>().Spawn(true);
    }
    [ServerRpc]
    private void PlayerDeleteBallServerRpc()
    {
        Debug.Log("Spawning Ball For: " + OwnerClientId);
        Transform spawnBall = Instantiate(networkedBall, gameObject.transform);
        spawnBall.GetComponent<NetworkObject>().Spawn(true);
    }
}
