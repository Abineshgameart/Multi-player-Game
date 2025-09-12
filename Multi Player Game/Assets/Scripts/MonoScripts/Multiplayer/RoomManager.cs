using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    // Private


    // Public
    public GameObject lobby;
    public GameObject lobbyBtn;
    public GameObject player;
    [Space]
    // public Transform spawnPoint;
    public TMP_InputField createRoomTxt;
    public TMP_InputField joinRoomTxt;


    public string roomNameToJoin;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server.");

        PhotonNetwork.JoinLobby();
    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("We're in Lobby.");

        //PhotonNetwork.JoinOrCreateRoom("test", null, null);

    }

    public void CreatingRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTxt.text, null, null);
    }
    
    public void JoiningRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomTxt.text);
    }



    public override void OnJoinedRoom()
    {
        if (lobby != null && lobby.activeSelf)
        {
            lobby.SetActive(false);
        }

        base.OnJoinedRoom();

        Debug.Log("We're Connected and in a room!");

        SceneManager.LoadScene("Area1");
        
        // GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
    }

    public void LobbyScreen()
    {
        if (lobby != null && !lobby.activeSelf)
        {
            lobby.SetActive(true);
            lobbyBtn.SetActive(false);

            Debug.Log("Connecting...");

            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
}
