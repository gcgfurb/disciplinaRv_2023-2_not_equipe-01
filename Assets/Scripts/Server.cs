using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System;
using UnityEngine;

public class Server : MonoBehaviourPunCallbacks
{
    public int maxPlayers = 4;

    [SerializeField()]
    private GameObject controlPanel;

    [SerializeField()]
    private GameObject progressLabel;


    private string gameVersion = "1";
    private bool isConnecting;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);

        Debug.Log("Disconnected");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed joining random room");

        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("Loading game");
            PhotonNetwork.LoadLevel("Game");
        }
    }

    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            isConnecting = PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }


    public void CreateRoom()
    {
        Debug.Log("Creating room...");

        var roomOptions = new RoomOptions()
        {
            MaxPlayers = maxPlayers,
        };

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

    public void JoinRoom(string roomId)
    {
        Debug.Log("Joining room...");
        PhotonNetwork.JoinRoom(roomId);
    }
}
