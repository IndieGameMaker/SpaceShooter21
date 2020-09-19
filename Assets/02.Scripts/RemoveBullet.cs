using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    public GameObject sparkEffect;

    //충돌했을 때 1회 호출
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "BULLET")
        {
            ContactPoint[] points = coll.contacts;
            Vector3 normalVec = points[0].normal;
            
            GameObject obj = Instantiate(sparkEffect, points[0].point, Quaternion.LookRotation(normalVec));
            Destroy(obj, 0.2f); //(삭제할 오브젝트, 지연시간)
            Destroy(coll.gameObject);
        }
    }
}
