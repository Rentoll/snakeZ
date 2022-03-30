using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MPGameController : GameController
{
    private PhotonView myPhotonView;
    private int localBlueScore;
    private int localRedScore;
    private void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        blueName = "ENEMY";
        redName = "YOU";
    }
    public override void AddPoint(bool enemy)
    {
        Debug.Log("Add Point mp");
        if (PhotonNetwork.IsMasterClient)
        {
            if (!enemy)
            {
                localRedScore++;
                redScore = localRedScore;
            }
            else
            {
                localBlueScore++;
                blueScore = localBlueScore;
            }

            myPhotonView.RPC("RPC_SendScore", RpcTarget.Others, blueScore, redScore); 
        }
        redrawScore();
    }

    [PunRPC]
    private void RPC_SendScore(int red, int blue)
    {
        blueScore = blue;
        redScore = red;

        if (localBlueScore != blue)
        {
            localBlueScore = blue;    
        }

        if (localRedScore != red)
        {
            localRedScore = red; 
        }

        redrawScore();
        CheckForWinner();
    }

    private void redrawScore()
    {
        blueScoreDisp.text = blueScore + "/" + maxScore;
        redScoreDisp.text = redScore + "/" + maxScore;
    }

    public override void onQuitPressed()
    {
        PhotonNetwork.LeaveRoom();
        base.onQuitPressed();
    }
}
