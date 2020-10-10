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
    private Animator anim;

    //몬스터의 상태를 저장하는 변수
    public STATE state = STATE.IDLE;

    //몬스터의 사망여부
    public bool isDie = false;
    //공격사정거리
    public float attackDist = 2.0f;
    //추적사정거리
    public float traceDist = 10.0f;

    //Animator HashTable 미리 Hash 추출
    private int hashAttack;
    private int hashHit;
    private int hashDie = Animator.StringToHash("Die");
    private int hashDance = Animator.StringToHash("PlayerDie");

    //Monster Health
    public float hp = 100.0f;

    void OnEnable()
    {
        //이벤트의 연결
        PlayerCtrl.OnPlayerDie += this.YouWin;

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
    }

    void OnDisable()
    {
        //이벤트 연결정보 해제
        PlayerCtrl.OnPlayerDie -= this.YouWin;
    }

    void Awake()
    {
        monsterTr = GetComponent<Transform>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("PLAYER");
        if (playerObj != null)
        {
            playerTr = playerObj.GetComponent<Transform>();
        }

        agent = GetComponent<NavMeshAgent>();
        anim  = GetComponent<Animator>();

        hashAttack = Animator.StringToHash("IsAttack");
        hashHit    = Animator.StringToHash("Hit");
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
                    anim.SetBool("IsTrace", false);
                    agent.isStopped = true;
                    break;

                case STATE.TRACE:
                    anim.SetBool(hashAttack, false);
                    anim.SetBool("IsTrace", true);
                    agent.SetDestination(playerTr.position);
                    agent.isStopped = false;
                    break;

                case STATE.ATTACK:
                    agent.isStopped = true;
                    anim.SetBool(hashAttack, true);
                    break;

                case STATE.DIE:
                    break;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    //충돌 콜백함수
    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("BULLET"))
        {
            Destroy(coll.gameObject);
        }
    }

    public void OnDamage()
    {
        anim.SetTrigger(hashHit);//anim.SetTrigger("Hit");

        hp -= 20.0f;
        if (hp <= 0.0f)
        {
            MonsterDie();
        }        
    }

    void MonsterDie()
    {
        GameManager.instance.IncreseScore(10);

        GetComponent<CapsuleCollider>().enabled = false;
        agent.isStopped = true;
        StopAllCoroutines();
        anim.SetTrigger(hashDie);

        Invoke("ReturnPooling", 5.0f);
    }

    void ReturnPooling()
    {
        hp = 100.0f;
        GetComponent<CapsuleCollider>().enabled = true;

        this.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("Monster Hit = " + coll.gameObject.name);
    }

    public void YouWin()
    {
        StopAllCoroutines();
        agent.isStopped = true;
        anim.SetTrigger(hashDance);
    }
}
