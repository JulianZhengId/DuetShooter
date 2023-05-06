using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public static PlayerSpawner instance;
    
    //players prefab
    [SerializeField] private NetworkPrefabRef playerHostPrefab;
    [SerializeField] private NetworkPrefabRef playerJoinPrefab;

    private Dictionary<PlayerRef, NetworkObject> spawnedPlayers = new Dictionary<PlayerRef, NetworkObject>();

    //movement
    private PlayerControls playerControls;
    private Vector2 moveValue;

    //shoot
    private float isShootPressed = 0;

    //shoot method
    private bool isRightChangeWeaponpressed = false;
    private bool isLeftChangeWeaponPressed = false;

    //spawners
    private bool spawn1Pressed;
    private bool spawn2Pressed;
    private bool spawn3Pressed;

    private void Awake()
    {
        instance = this;

        //controls
        playerControls = new PlayerControls();

        //movement
        playerControls.Player.Movement.performed += context => moveValue = context.ReadValue<Vector2>();
        playerControls.Player.Movement.canceled += context => moveValue = Vector2.zero;

        //shoot
        playerControls.Player.Shoot.performed += context => isShootPressed = context.ReadValue<float>();
        playerControls.Player.Shoot.canceled += context => isShootPressed = 0;

        //shoot method
        playerControls.Player.RightSwitchMethod.performed += context => isRightChangeWeaponpressed = true;
        playerControls.Player.LeftSwitchMethod.performed += context => isLeftChangeWeaponPressed = true;

        //spawners
        playerControls.Player.Spawn1.performed += context => spawn1Pressed = true;
        playerControls.Player.Spawn2.performed += context => spawn2Pressed = true;
        playerControls.Player.Spawn3.performed += context => spawn3Pressed = true;
    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData data = new NetworkInputData();

        //movement
        data.movement = moveValue;
        data.movement.Normalize();

        //shoot
        data.isShootPressed = isShootPressed > 0;

        //change weapon
        data.isRightChangeWeaponPressed = isRightChangeWeaponpressed;
        data.isLeftChangeWeaponPressed = isLeftChangeWeaponPressed;
        isRightChangeWeaponpressed = false;
        isLeftChangeWeaponPressed = false;

        //spawners
        data.spawn1Pressed = spawn1Pressed;
        data.spawn2Pressed = spawn2Pressed;
        data.spawn3Pressed = spawn3Pressed;
        spawn1Pressed = false;
        spawn2Pressed = false;
        spawn3Pressed = false;
   
        input.Set(data);
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            NetworkObject networkPlayerObject;
            if (spawnedPlayers.Count == 0)
            {
                networkPlayerObject = runner.Spawn(playerHostPrefab, Vector3.zero, Quaternion.identity, player);
                GetComponent<ItemSpawner>().SpawnRegenItem(new Vector3(2, -2, 0));
                GetComponent<ItemSpawner>().SpawnSpeedUpItem(new Vector3(-2, -2, 0));
            }
            else
            {
                networkPlayerObject = runner.Spawn(playerJoinPrefab, Vector3.zero, Quaternion.identity, player);
                UIManager.instance.TurnOnPlayer2UI();
            }
            spawnedPlayers.Add(player, networkPlayerObject);
        }
        else
        {
            UIManager.instance.TurnOnPlayer2UI();
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (spawnedPlayers.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            spawnedPlayers.Remove(player);
        }
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        //Debug.Log("connected to server");
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
        Debug.Log("failed to connect");
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        //Debug.Log("request to connect");
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
        Debug.Log("custom authentication");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
        //Debug.Log("disconnected from server");
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
        //Debug.Log("host hand over");
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
        //Debug.Log("input mising");
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
        Debug.Log("received reliable data");
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        //Debug.Log("scene loaded successfully");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        //Debug.Log("start to load scene");
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        Debug.Log("session list updated");
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        //Debug.Log("shut down");
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
        Debug.Log("message simulation");
    }

    public PlayerControls GetPlayerControls()
    {
        return playerControls;
    }
}


