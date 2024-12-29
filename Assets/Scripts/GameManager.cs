using Unity.Netcode;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private NetworkManager m_NetworkManager;
    public static GameManager Instance;
    void Awake()
    {
        m_NetworkManager = GetComponent<NetworkManager>();
        Instance = this;
        if(!PlayerPrefs.HasKey("Mode")) return;
        string mode = PlayerPrefs.GetString("Mode");

        if(mode == "Host")
        {
            m_NetworkManager.StartHost();
        }
        else if(mode == "Client")
        {
            m_NetworkManager.StartClient();
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
        {
            StartButtons();
        }
        else 
        {
            StatusLabels();
        }
        GUILayout.EndArea();
    }
    void StartButtons()
    {
        if (GUILayout.Button("Host"))
        {
            m_NetworkManager.StartHost();
        }
        
        if (GUILayout.Button("Client")) 
        {
            m_NetworkManager.StartClient();
        }
        
        if (GUILayout.Button("Server")) 
        {
            m_NetworkManager.StartServer();
        }
    }
    void StatusLabels()
    {
        var mode = m_NetworkManager.IsHost ?
            "Host" : m_NetworkManager.IsServer ? "Server" : "Client";
        GUILayout.Label("Transport: " +
            m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
        GUILayout.Label("Mode: " + mode);
    }
    
    public int score1 = 0, score2 = 0;
}
