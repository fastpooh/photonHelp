using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Detector : MonoBehaviourPunCallbacks
{
    public enum State
    {
        IDLE,
        TRACE
    }
    public State state = State.IDLE;
    public float traceDist = 10.0f;

    public GameObject monster;
    private NavMeshAgent agent;
    private Transform player1Tr;
    private Transform player2Tr;
    private Transform goToPos;
    private int playerNum = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNum == 2)
        {
            float distance1 = Vector3.Distance(player1Tr.position, monster.transform.position);
            float distance2 = Vector3.Distance(player2Tr.position, monster.transform.position);

            if(distance1 <= traceDist || distance2 <= traceDist)
            {
                state = State.TRACE;
                if(distance1 <= distance2)
                    goToPos = player1Tr;
                else
                    goToPos = player2Tr;
            }
        else
        {
            state = State.IDLE;
        }
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if(coll.tag == "Player1")
        {
            player1Tr = coll.GetComponent<Transform>();
        }
        if(coll.tag == "Player2")
        {
            player2Tr = coll.GetComponent<Transform>();
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "Player1")
            playerNum++;
        if(coll.tag == "Player2")
            playerNum++;
    }
}
