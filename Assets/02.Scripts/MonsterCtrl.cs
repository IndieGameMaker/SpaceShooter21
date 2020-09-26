using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    [System.NonSerialized]
    public Transform monsterTr;
    [HideInInspector]
    public Transform playerTr;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        monsterTr = GetComponent<Transform>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        if (playerObj != null)
        {
            playerTr = playerObj.GetComponent<Transform>();
        }

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTr.position);
    }
}
