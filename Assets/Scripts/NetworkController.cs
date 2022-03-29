using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    /** Photon docs 
     * Documentation: https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
     * Scripting API: https://doc-api.photonengine.com/en/pun/v2/index.html
     */

    public GameObject PlayMPButton;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    { 
        Debug.Log("Connected to **** " + PhotonNetwork.CloudRegion + " ****\n");
        PlayMPButton.GetComponent<Button>().interactable = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
