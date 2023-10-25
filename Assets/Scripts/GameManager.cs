using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} entered the room");

        if (PhotonNetwork.IsMasterClient)
        {
            LoadGame();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player {otherPlayer.NickName} left the room");

        if (PhotonNetwork.IsMasterClient)
        {
            LoadGame();
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void LoadGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Trying to laod level but is not master client");
            return;
        }
        Debug.Log($"Loading level for {PhotonNetwork.CurrentRoom.PlayerCount} players");
        PhotonNetwork.LoadLevel("Game");
    }
}
