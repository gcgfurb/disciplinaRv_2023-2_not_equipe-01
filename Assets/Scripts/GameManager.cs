using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks//, IPunObservable
{
    [SerializeField] private GameObject ball;

    private GameObject networkBall;

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Menu");
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

    //private void Update()
    //{
    //    if (!PhotonNetwork.IsMasterClient)
    //    {
    //        ball.transform.position = Vector3.Lerp(transform.position, networkBall.transform.position, Time.deltaTime * 10f);
    //    }
    //}

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(ball);
    //    }
    //    else
    //    {
    //        networkBall = (GameObject)stream.ReceiveNext();
    //    }
    //}

}
