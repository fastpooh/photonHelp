using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerCtrl : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private CharacterController controller;
    private new Transform transform;
    private Animator anim;
    private new Camera camera;

    Vector3 moveVec;

    private PhotonView pv;
    public float moveSpeed = 10.0f;

    private int initHp = 1;
    public int currHp;

    public int coin;
    private TextMeshProUGUI coinUI;

    //private GameObject block3;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        camera = Camera.main;

        pv = GetComponent<PhotonView>();
        coinUI = GameObject.FindGameObjectWithTag("CoinNum")?.GetComponent<TextMeshProUGUI>();

        coin = 0;
        currHp = initHp;
        //coinUI.text = "$ 0";

        //block3 = GameObject.Find("Cube_3");
        if(PhotonNetwork.IsMasterClient)
            Debug.Log("222");
    }

    void Update()
    {
        if(pv.IsMine)
        {
            Move();
            Turn();
            if(coin >= 0)
            {
                coinUI.text = "$ " + coin;
            }

            Hashtable hash = new Hashtable();
            //hash.Add("weaponIndex", weaponIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    

    float hAxis => Input.GetAxis("Horizontal");
    float vAxis => Input.GetAxis("Vertical");
    
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        
        transform.position += moveVec*moveSpeed*Time.deltaTime;
        
        anim.SetBool("isWalk", hAxis != 0.0f || vAxis != 0.0f);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "BronzeCoin")
        {
            coin = coin + 2;
            Destroy(coll.gameObject);
        }
        else if(coll.tag == "SilverCoin")
        {
            coin = coin + 5;
            Destroy(coll.gameObject);
        }
        if(coll.tag == "GoldCoin")
        {
            coin = coin + 7;
            Destroy(coll.gameObject);
        }
    }
/*
    public void gimic_1()
    {
        if(coin >= 5)
        {
            coin = coin - 5;
            block3.SetActive(false);
        }
        else
        {
            coin = coin - 5;
            block3.SetActive(false);
            print("not enough coin.");
        }
    }

/*
    [PunRPC]
    IEnumerator syncCollider()
    {
        if(equipWeapon.GetComponentInChildren<TrailRenderer>().enabled == false)
        {
            equipWeapon.GetComponentInChildren<TrailRenderer>().enabled = true;
            equipWeapon.GetComponent<Collider>().enabled = true;
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.7f);
            equipWeapon.GetComponentInChildren<TrailRenderer>().enabled = false;
            equipWeapon.GetComponent<Collider>().enabled = false;
        }
    }
*/
}
