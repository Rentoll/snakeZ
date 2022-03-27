using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class MyPhotonPlayer : MonoBehaviour
{
    PhotonView myPV;
    GameObject myPlayerAvatar;
    private bool second = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Spawn");
        myPV = GetComponent<PhotonView>();
        if (GameObject.FindGameObjectsWithTag("PhotonPlayer").Length == 1)
        {
            second = true;
        }
        if (myPV.IsMine)
        {
            if (second)
            {
                myPlayerAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "SnakeHeadMP"), Vector2.zero, Quaternion.identity);
                myPlayerAvatar.gameObject.GetComponent<SnakeMP>().enemy = true;
            }
            else
            {
                myPlayerAvatar = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "SnakeHeadMP"), new Vector3(10f, 10f, 0.0f), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
