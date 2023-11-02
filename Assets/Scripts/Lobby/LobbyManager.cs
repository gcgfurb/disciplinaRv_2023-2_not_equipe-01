using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    private Color selectedBackgroundColor = new(142f/255, 139f/255, 139f/255);
    private Color selectedTextColor = new(1f, 1f, 1f);
    
    private Color unselectedBackgroundColor = new(207f/255, 207f/255, 207f/255);
    private Color unselectedTextColor = new(50f/255, 50f/255, 50f/255);

    private bool isJoinSelected = true;

    [SerializeField] private GameObject joinButton;
    [SerializeField] private GameObject createButton;

    [SerializeField] private GameObject joinPanel;
    [SerializeField] private GameObject createPanel;

    [SerializeField] private GameObject roomsScrollPanelContainer;
    [SerializeField] private Button scrollPanelItemPrefab;

    [SerializeField] private Server server;

    private void Start()
    {
        LoadAvailableRooms();

        StartCoroutine(CheckNewRooms());
    }

    private IEnumerator CheckNewRooms()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            LoadAvailableRooms();
        }
    }

    public void LoadAvailableRooms()
    {
        foreach (Transform child in roomsScrollPanelContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in server.cachedRoomList)
        {
            var row = Instantiate(
                original: scrollPanelItemPrefab,
                parent: roomsScrollPanelContainer.transform
            );
            row.transform.localScale = Vector3.one;
            row.gameObject.SetActive(true);

            row.onClick.AddListener(() => OnJoinRoom(row));

            row.GetComponentInChildren<TextMeshProUGUI>().text = $"Room {item.Key}";
        }
    }

    private void OnJoinRoom(Button clickedButton)
    {
        var roomId = clickedButton.GetComponentInChildren<TextMeshProUGUI>().text.Replace("Room ", "");

        server.JoinRoom(roomId);

        joinPanel.SetActive(false);
        createPanel.SetActive(true);
    }

    public void OnJoinSelect()
    {
        if (isJoinSelected) { return; }

        Select(joinButton);
        Unselect(createButton);

        joinPanel.SetActive(true);
        createPanel.SetActive(false);

        LoadAvailableRooms();

        isJoinSelected = true;
    }

    public void OnCreateSelect()
    {
        if (!isJoinSelected) { return; }

        Unselect(joinButton);
        Select(createButton);

        joinPanel.SetActive(false);
        createPanel.SetActive(true);

        isJoinSelected = false;
    }

    private void Select(GameObject component)
    {
        component.GetComponent<Image>().color = selectedBackgroundColor;
        component.GetComponentInChildren<TextMeshProUGUI>().color = selectedTextColor;
    }

    private void Unselect(GameObject component)
    {
        component.GetComponent<Image>().color = unselectedBackgroundColor;
        component.GetComponentInChildren<TextMeshProUGUI>().color = unselectedTextColor;
    }
}
