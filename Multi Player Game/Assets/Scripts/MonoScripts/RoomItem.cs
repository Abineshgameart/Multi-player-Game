using UnityEngine;

public class RoomItem : MonoBehaviour
{
    public string RoomName;

    public void OnButtonPressed()
    {
        RoomList.Instance.JoinRoomByName(RoomName);
    }
}
