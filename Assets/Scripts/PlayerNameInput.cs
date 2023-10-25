using UnityEngine;

using Photon.Pun;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInput : MonoBehaviour
{
    const string PLAYER_NAME_PREF_KEY = "PlayerName";

    void Start()
    {
        var playerName = string.Empty;
        var inputField = GetComponent<TMP_InputField>();
        if (PlayerPrefs.HasKey(PLAYER_NAME_PREF_KEY))
        {
            playerName = PlayerPrefs.GetString(PLAYER_NAME_PREF_KEY);
            inputField.text = playerName;
        }

        PhotonNetwork.NickName = playerName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            Debug.LogError("Player Name is null or whitespace");
            return;
        }

        PlayerPrefs.SetString(PLAYER_NAME_PREF_KEY, value);
        PhotonNetwork.NickName = value;
    }
}
