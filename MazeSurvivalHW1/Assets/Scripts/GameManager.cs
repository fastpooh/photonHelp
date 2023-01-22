using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    public int i;
    public int players = 0;
    //public Button exitBtn;

    void Awake()
    {
        CreatePlayer();
        //exitBtn.onClick.AddListener(() => OnExitClick());
    }

    void CreatePlayer()
    {
        i = 0;
        foreach(var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName}, {player.Value.ActorNumber}");
            i++;
        }

        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        Debug.Log($"i : {i}");
        if(i == 1)
        {
            PhotonNetwork.Instantiate("Player1", points[i].position, points[i].rotation, 0);
            //PhotonNetwork.InstantiateRoomObject("Monster", points[3].position, points[3].rotation, 0);
        }
        else if(i == 2)
        {
            players = 2;
            PhotonNetwork.Instantiate("Player2", points[i].position, points[i].rotation, 0);
            //PhotonNetwork.Instantiate("Monster", points[3].position, points[3].rotation, 0);
            StartCoroutine(MonsterGen(points));
        }
        else
            Debug.LogError("Error!");
    }


    void Update()
    {
        
    }

    IEnumerator MonsterGen(Transform[] points)
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.Instantiate("Monster", points[3].position, points[3].rotation, 0);
    }

/*
    private void OnExitClick()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("StartUI");
    }
*/
}