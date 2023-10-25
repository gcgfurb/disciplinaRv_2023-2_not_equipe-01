using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLobby : MonoBehaviour
{
    public void LoadLobby()
    {
        Debug.Log("Loading lobby");
        SceneManager.LoadScene("Lobby");
    }
}
