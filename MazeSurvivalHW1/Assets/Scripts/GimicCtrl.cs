using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GimicCtrl : MonoBehaviour
{
    private PlayerCtrl enterPlayer1;
    private PlayerCtrl enterPlayer2;
    public GameManager gameManager;
    private PhotonView pv;
    public GameObject block1;
    public GameObject block2;

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        
    }

    public void gimic_1()
    {
        if(enterPlayer1.coin >= 5)
        {
            enterPlayer1.coin = enterPlayer1.coin - 5;
            block1.SetActive(false);
            pv.RPC("syncBlockDisappear", RpcTarget.Others, null);
        }
        else
        {
            print("not enough coin.");
        }

        if(enterPlayer2.coin >= 5)
        {
            enterPlayer2.coin = enterPlayer2.coin - 5;
            block1.SetActive(false);
            pv.RPC("syncBlockDisappear", RpcTarget.Others, null);
        }
        else
        {
            print("not enough coin.");
        }
    }

    public void gimic_2()
    {
        if(enterPlayer1.coin >= 7)
        {
            enterPlayer1.coin = enterPlayer1.coin - 7;
            block2.SetActive(true);
            pv.RPC("syncBlockAppear", RpcTarget.Others, null);
        }
        else
        {
            print("not enough coin.");
        }

        if(enterPlayer2.coin >= 7)
        {
            enterPlayer2.coin = enterPlayer2.coin - 7;
            block2.SetActive(true);
            pv.RPC("syncBlockAppear", RpcTarget.Others, null);
        }
        else
        {
            print("not enough coin.");
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if(coll.tag == "Player1")
        {
            enterPlayer1 = coll.GetComponent<PlayerCtrl>();
        }

        if(coll.tag == "Player2")
        {
            enterPlayer2 = coll.GetComponent<PlayerCtrl>();
        }
    }

    [PunRPC]
    void syncBlockDisappear()
    {
        if(block1.activeSelf == true)
        {
            block1.SetActive(false);
        }
    }
    [PunRPC]
    void syncBlockAppear()
    {
        if(block2.activeSelf == false)
        {
            block2.SetActive(true);
        }
    }
}
