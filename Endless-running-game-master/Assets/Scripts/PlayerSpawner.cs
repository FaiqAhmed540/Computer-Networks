using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerSpawner : MonoBehaviour
{

    public static PlayerSpawner instance;

    public void Awake()
    {
        instance = this;
    }

    public GameObject playerPrefeb;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
     if(PhotonNetwork.IsConnected) 
        {
            SpawnPlayer();
        }   
    }

    public void SpawnPlayer()
    {
        Transform spawnPoint =  SpawnManager.instance.getspawnpoint();

        Player = PhotonNetwork.Instantiate(playerPrefeb.name, spawnPoint.position, spawnPoint.rotation);
    }
}
