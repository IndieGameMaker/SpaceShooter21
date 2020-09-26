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

    void Start()
    {
        GameObject spawnPointGroup = GameObject.Find("SpawnPointGroup");  
        if (spawnPointGroup != null)
        {
            points = spawnPointGroup.GetComponentsInChildren<Transform>();
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
