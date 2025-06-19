using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class playerSpwaner : MonoBehaviourPunCallbacks
{
    // Public
    public GameObject player;
    public Transform spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
    }
}
