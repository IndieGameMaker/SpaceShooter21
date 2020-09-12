using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 50.0f;

    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ 1.0f
        float v = Input.GetAxis("Vertical");   // -1.0f ~ 0.0f ~ 1.0f 

        //transform.position += new Vector3(0, 0, 0.1f * v);
        transform.Translate( Vector3.forward * moveSpeed * Time.deltaTime * v ); //(방향 * 속도 * 변위)
        transform.Translate( Vector3.right * moveSpeed * Time.deltaTime * h );   //

        /* 정규화 벡터(Normalized Vector), 단위 벡터(Unit Vector)
            Vector3.forward  = new Vector3(0, 0, 1)
            Vector3.up       = new Vector3(0, 1, 0)
            Vector3.right    = new Vector3(1, 0, 0)

            Vector3.zero     = new Vector3(0, 0, 0)
            Vector3.one      = new Vector3(1, 1, 1)
        */

        Debug.Log("h=" + h + "v=" + v);
    }
}
