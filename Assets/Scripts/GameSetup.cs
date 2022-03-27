using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PhotonPlayer"), Vector2.zero, Quaternion.identity);
    }
}
