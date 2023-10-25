using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class Server : MonoBehaviourPunCallbacks
{
    public int maxPlayers = 4;

    [SerializeField()]
    private GameObject controlPanel;

    [SerializeField()]
    private GameObject progressLabel;

    [SerializeField()]
    private TextMeshProUGUI roomCode;

    private string gameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        roomCode.gameObject.SetActive(false);
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);

        Debug.Log("Disconnected");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"Failed joining random room: {message}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
    }

    public void Play()
    {
        Debug.Log("Loading game");
        PhotonNetwork.LoadLevel("Game");
    }

    public void CreateRoom()
    {
        Debug.Log("Creating room...");

        var roomId = Random.Range(1000, 9999);
        var roomOptions = new RoomOptions()
        {
            MaxPlayers = maxPlayers,
        };
        PhotonNetwork.CreateRoom(roomId.ToString(), roomOptions);

        roomCode.text = $"Your room ID is {roomId}";
        roomCode.gameObject.SetActive(true);
    }

    public void JoinRoom(string roomId)
    {
        Debug.Log("Joining room...");
        
        PhotonNetwork.JoinRoom(roomId);

        roomCode.gameObject.SetActive(false);
    }
}
