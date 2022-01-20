using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Photon.MonoBehaviour
{
    public string verNum = "0.1";
    public string roomName = "room01";
    public Transform spawnPoint;
    public GameObject playerPref;
    public bool isConnected = false;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(verNum);
        Debug.Log("Starting Connection");
    }

    public void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, null, null);
        Debug.Log("Starting Server");
    }

    public void OnJoinedRoom()
    {
        isConnected = true;
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject player = PhotonNetwork.Instantiate(playerPref.name, spawnPoint.position, spawnPoint.rotation, 0) as GameObject;
        player.GetComponent<FPSController>().enabled = true;
        player.GetComponent<FPSController>().fpsCam.SetActive(true);
        player.GetComponent<FPSController>().graphics.SetActive(false);
    }
}
