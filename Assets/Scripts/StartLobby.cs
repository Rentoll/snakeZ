using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartLobby : MonoBehaviourPunCallbacks
{
    private int RoomSize = 2;

    [SerializeField]
    private GameObject StartMPButton;
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedtoMaster");
        PhotonNetwork.AutomaticallySyncScene = true;
        StartMPButton.SetActive(true);
    }
    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            StartMPButton.SetActive(true);
        }
    }
    public void OnStartMP()
    {
        Debug.Log("OnStartMP");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("CreateRoom");
        int roomNumber = Random.Range(0, 10000);

        RoomOptions roomOps = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)RoomSize
        };

        PhotonNetwork.CreateRoom("Room" + roomNumber, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

}
