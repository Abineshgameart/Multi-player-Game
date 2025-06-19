using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomList : MonoBehaviourPunCallbacks
{
    // Public
    public static RoomList Instance;

    public GameObject roomManagerGameobject;
    public RoomManager roomManager;

    [Header("UI")]
    public Transform roomListParent;
    public GameObject roomListItemPrefab;


    // Private
    private List<RoomInfo> cacheRoomList = new List<RoomInfo>();

    private void Awake()
    {
        Instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        // Precaution that if it is already Connected
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (cacheRoomList.Count <= 0)
        {
            cacheRoomList = roomList;
        }
        else
        {
            foreach (var room in roomList)
            {
                for (int i = 0; i < cacheRoomList.Count; i++)
                {
                    if (cacheRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = cacheRoomList;

                        if (room.RemovedFromList)
                        {
                            newList.Remove(newList[i]);
                        }
                        else
                        {
                            newList[i] = room;
                        }

                        cacheRoomList = newList;
                    }
                }
            }
        }

        UpdateUI();
    }

    //  ==========  User Defined Functions =============

    private void UpdateUI()
    {
        foreach (Transform roomItem in roomListParent)
        {
            Destroy(roomItem.gameObject);
        }

        foreach (var room in cacheRoomList)
        {
            GameObject roomItem = Instantiate(roomListItemPrefab, roomListParent);

            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name;
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount.ToString() + "/16";

            roomItem.GetComponent<RoomItem>().RoomName = room.Name;
        }
    }

    public void JoinRoomByName(string _name)
    {
        // roomManager.roomNameToJoin = _name;
        PhotonNetwork.JoinRoom(_name);
    }
}
