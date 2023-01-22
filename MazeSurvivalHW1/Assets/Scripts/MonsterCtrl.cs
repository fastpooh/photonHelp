using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum State
    {
        IDLE,
        TRACE
    }
    public State state = State.IDLE;
    public float traceDist = 10.0f;


    private Transform monsterTr;
    private Transform playerTr1;
    private Transform playerTr2;
    private Transform goToPos;
    private NavMeshAgent agent;
    private Animator anim;
    private GameManager gm;
    private PhotonView pv;

    //public int playerNum = 0;
    public int losePlayer = 0;

    //
    private Vector3 recievePos;
    private Quaternion recieveRot;
    public float damping = 10.0f;

    void Start()
    {
        gm = GameManager.Instance;
        pv = GetComponent<PhotonView>();
        monsterTr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        playerTr1 = GameObject.FindWithTag("Player1").GetComponent<Transform>();
        playerTr2 = GameObject.FindWithTag("Player2").GetComponent<Transform>();
        goToPos = monsterTr;

        agent = GetComponent<NavMeshAgent>();
        
        //StartCoroutine(CheckMonsterState());
        //StartCoroutine(MonsterAction());
    }

    
    void Update()
    {   
        //if(!pv.IsMine)
        //{
            float distance1 = Vector3.Distance(playerTr1.position, monsterTr.position);         //if pv is mine?
            float distance2 = Vector3.Distance(playerTr2.position, monsterTr.position);
            //Debug.Log("distance 1 : "+ distance1 +", distance 2: "+ distance2);

            if(distance1 <= traceDist || distance2 <= traceDist)
            {
                state = State.TRACE;
                agent.isStopped = false;
                anim.SetBool("isTrace", true);
                if(distance1 <= distance2)
                    goToPos = playerTr1;
                else
                    goToPos = playerTr2;
            }
            else
            {
                state = State.IDLE;
                agent.isStopped = true;
                anim.SetBool("isTrace", false);
            }

            //pv.RPC("SyncMonster", RpcTarget.Others, goToPos);
            agent.SetDestination(goToPos.position);
        //}
        /*
        if(pv.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, recievePos, Time.deltaTime*damping);
            transform.rotation = Quaternion.Slerp(transform.rotation, recieveRot, Time.deltaTime*damping);
        }
        */
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "Player1")
        {
            losePlayer = 1;
        }
        if(coll.collider.tag == "Player2")
        {
            losePlayer = 2;
        }
    }
/*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            recievePos = (Vector3)stream.ReceiveNext();
            recieveRot = (Quaternion)stream.ReceiveNext();
        }
    }
*/
/*
    [PunRPC]
    void SyncMonster(Transform destination)
    {
        if(state == State.IDLE)
        {
            agent.isStopped = true;
            anim.SetBool("isTrace", false);
        }
        else if(state == State.TRACE)
        {
            agent.isStopped = false;
            anim.SetBool("isTrace", true);
            goToPos = destination;
        }
    }
*/
/*
    IEnumerator CheckMonsterState()
    {
        yield return new WaitForSeconds(0.3f);
        float distance1 = Vector3.Distance(playerTr1.position, monsterTr.position);
        float distance2 = Vector3.Distance(playerTr2.position, monsterTr.position);

        if(distance1 <= traceDist || distance2 <= traceDist)
        {
            state = State.TRACE;
            if(distance1 <= distance2)
                goToPos = playerTr1;
            else
                goToPos = playerTr2;
        }
        else
        {
            state = State.IDLE;
        }
    }

    IEnumerator MonsterAction()
    {
        switch(state)
        {
            case State.IDLE:
                agent.isStopped = true;
                anim.SetBool("isTrace", false);
                break;
            case State.TRACE:
                agent.SetDestination(goToPos.position);
                anim.SetBool("isTrace", true);
                agent.isStopped = false;
                break;
        }
        yield return new WaitForSeconds(0.3f);
    }
*/
}
