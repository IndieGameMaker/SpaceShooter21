using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //몬스터가 출현할 위치정보
    public Transform[] points;

    //몬스터 프리팹
    public GameObject monsterPrefab;

    //몬스터의 출현 간격(생성 간격)
    public float createTime = 3.0f;
    private WaitForSeconds ws;
    public bool isGameOver = false;

    void Start()
    {
        ws = new WaitForSeconds(createTime);

        GameObject spawnPointGroup = GameObject.Find("SpawnPointGroup");  
        if (spawnPointGroup != null)
        {
            points = spawnPointGroup.GetComponentsInChildren<Transform>();
        }  

        StartCoroutine(this.CreateMonster());    
    }

    //몬스터를 생성하는 함수
    IEnumerator CreateMonster()
    {
        while(!isGameOver)
        {
            yield return ws;

            //Points 배열의 첨자(Index)에 대한 단수발생
            int idx = Random.Range(1, points.Length); //1 ~ 25
            //몬스터 생성(Clone)
            GameObject monster = Instantiate<GameObject>(monsterPrefab);
            //위치와 회전값을 설정
            monster.transform.position = points[idx].position;

            //벡터의 뺄셈 연산
            /*
                벡터 A , B
                A - B   ==> B -> A 방향 벡터
            */

            Vector3 dir = points[0].position - points[idx].position;
            Quaternion rot = Quaternion.LookRotation(dir);
            monster.transform.rotation = rot;
        }
    }
}
