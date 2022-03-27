using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviourPunCallbacks
{
    private PhotonView myPhotonView;
    private int players;
    private int maxPlayers;
    private float waitTime = 15;
    private float currentWaitTime;
    public Text playerCount;
    public Text startTimer;


    private bool readyToStart = false;
    private bool startingGame = false;
    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();

        PlayerCountUpdate();
    }

    void PlayerCountUpdate()
    {
        players = PhotonNetwork.PlayerList.Length;
        maxPlayers = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCount.text = players + "/" + maxPlayers;

        if (players == maxPlayers)
        {
            readyToStart = true;
        }
        else
        {
            readyToStart = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();
        if (PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, currentWaitTime);
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        currentWaitTime = timeIn;
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate(); 
    }


    // Update is called once per frame
    void Update()
    {
        WaitingForMorePlayers();
    }

    void WaitingForMorePlayers()
    {
        if (players == 1)
        {
            ResetTimer();
        }

        if (readyToStart)
        {
            currentWaitTime -= Time.deltaTime;
        }

        string tempTimer = string.Format("{00}", currentWaitTime);
        startTimer.text = tempTimer;

        if (currentWaitTime <= 0f)
        {
            if (startingGame)
                return;
            StartGame();
        }
    }

    void ResetTimer()
    {
        currentWaitTime = waitTime;
    }

    public void StartGame() 
    {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(3);
    }
}
