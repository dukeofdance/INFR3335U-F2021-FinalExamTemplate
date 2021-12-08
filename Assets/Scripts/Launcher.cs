using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_Text error;

    public GameObject lobbyPanel;
    public GameObject roomPanel;

    public TMP_Text roomName;
    public TMP_Text playerCount;

    public GameObject playerList;
    public Transform playerListContent;

    public Button StartButton;
    public Button LeaveButton;

    public void Start()
    {
        lobbyPanel.SetActive(true);    
        roomPanel.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateRoom()
    {

        if (string.IsNullOrEmpty(createInput.text))
        {
            return;
        }
        Debug.Log("Create: " + createInput.text);
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        Debug.Log("Join: " + joinInput.text);
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        //PhotonNetwork.LoadLevel("Main"); //**
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);

        roomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        playerCount.text = "" + players.Length;

        for(int i = 0; i < players.Length; i++)
        {
            Instantiate(playerList, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(players[i]);

            if (i == 0)
            {
                StartButton.interactable = true;
            }
            else
            {
                StartButton.interactable = false;
            }
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        error.text = message;
        Debug.Log("Error creating room! " + message);
    }
    public void OnClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Loading");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerList, playerListContent).GetComponent<PlayerListing>().SetPlayerInfo(newPlayer);
    }
    public void OnClickStartGame()
    {
        PhotonNetwork.LoadLevel("Arena");
    }
}