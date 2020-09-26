using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum STATE
{
    IDLE,
    TRACE,
    ATTACK,
    DIE
}


public class MonsterCtrl : MonoBehaviour
{
    [System.NonSerialized]
    public Transform monsterTr;
    [HideInInspector]
    public Transform playerTr;

    private NavMeshAgent agent;

    //몬스터의 상태를 저장하는 변수
    public STATE state = STATE.IDLE;

    //몬스터의 사망여부
    public bool isDie = false;
    //공격사정거리
    public float attackDist = 2.0f;
    //추적사정거리
    public float traceDist = 10.0f;

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

        StartCoroutine(CheckMonsterState());
    }

    //몬스터의 상태를 체크하는 코루틴
    IEnumerator CheckMonsterState()
    {
        while(!isDie)
        {
            //주인공과 몬스터간의 거리를 계산
            float distance = Vector3.Distance(monsterTr.position, playerTr.position);

            if (distance <= attackDist)
            {
                state = STATE.ATTACK;
            }
            else if (distance <= traceDist) // 공격사정거리 < 몬스터위치 <= 추적사정거리 
            {
                state = STATE.TRACE;
            }
            else
            {
                state = STATE.IDLE;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    //몬스터의 행동을 처리하는 코루틴
    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch(state)
            {
                case STATE.IDLE:
                    break;

                case STATE.TRACE:
                    break;

                case STATE.ATTACK:
                    break;

                case STATE.DIE:
                    break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

}
