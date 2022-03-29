using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameController : MonoBehaviourPunCallbacks
{
    public Text blueScoreDisp;
    public Text redScoreDisp;
    public Text WinnerAnnounce;
    public GameObject WinnerDisplay;

    protected const int maxScore = 15;

    protected int blueScore = 0;
    protected int redScore = 0;

    protected string blueName = "BLUE";
    protected string redName = "RED";
    public virtual void AddPoint(bool enemy)
    {
        Debug.Log("Add Point sp");
        if (!enemy)
        {
            redScore++;
            redScoreDisp.text = redScore + "/" + maxScore;
        }
        else 
        {
            blueScore++;
            blueScoreDisp.text = blueScore + "/" + maxScore;
        }
    }

    public void AddPointAndCheckForWinner(bool enemy)
    {
        AddPoint(enemy);
        CheckForWinner();
    }

    protected void CheckForWinner()
    {
        bool WeHaheAWinner = false;
        string message = "";

        if (blueScore == maxScore)
        {
            message += "<b><color='blue'>" + blueName + "</color></b>";
            WeHaheAWinner = true;
        }
        else if (redScore == maxScore)
        {
            message += "<b><color='red'>" + redName + "</color></b>";
            WeHaheAWinner = true;
        }

        if (WeHaheAWinner)
        {
            Time.timeScale = 0;
            message += " Winner!\n<b>CONGRATULATIONS!!!</b>";
            WinnerAnnounce.text = message;
            WinnerDisplay.SetActive(true);
        }
    }

    public virtual void onQuitPressed()
    {
        Time.timeScale = 1;
        Debug.Log("QuitPressed");
        SceneManager.LoadScene(0);
    }
}
