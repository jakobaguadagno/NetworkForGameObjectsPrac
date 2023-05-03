using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    /// <summary>
    /// Starts Server.
    /// </summary>
    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    /// <summary>
    /// Starts Host.
    /// </summary>
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    /// <summary>
    /// Starts Client.
    /// </summary>
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
