using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnim
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runLeft;
    public AnimationClip runRight;
    public AnimationClip[] die;
}

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    public float turnSpeed = 100.0f;

    public PlayerAnim playerAnim;

    private Animation animation;

    private float initHp = 100.0f;
    private float currHp = 100.0f; //(currHp / initHp) 백분율 Health Bar 구현할 때 사용

    void Start()
    {
        animation = GetComponent<Animation>();

        animation.Play(playerAnim.idle.name);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ 1.0f
        float v = Input.GetAxis("Vertical");   // -1.0f ~ 0.0f ~ 1.0f 
        float r = Input.GetAxis("Mouse X");    //마우스 좌우 이동 변위값

        //이동로직
        Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed);
        //회전로직
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * r);

        //애니메이션 처리
        PlayAnim(h, v);
    }

    void PlayAnim(float h, float v)
    {
        if (v >= 0.1f) //전진
        {
            animation.CrossFade(playerAnim.runForward.name, 0.3f);
        }
        else if (v <= -0.1f) //후진
        {
            animation.CrossFade(playerAnim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)  //오른쪽
        {
            animation.CrossFade(playerAnim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f) //왼쪽
        {
            animation.CrossFade(playerAnim.runLeft.name, 0.3f);
        }
        else
        {
            animation.CrossFade(playerAnim.idle.name, 0.3f);
        }
    }

    /*
        OnCollisionEnter, ~Stay, ~Exit : Is Trigger 언체크 
        OnTriggerEnter, ~Stay, ~Exit   : Is Trigger 체크 , 관통
    */

    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    void PlayerDie()
    {
        //Debug.Log("Player Die");
        //모든 몬스터를 추출해서 배열에 저장
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");

        foreach (var monster in monsters)
        {
            monster.SendMessage("YouWin", SendMessageOptions.DontRequireReceiver);
        }
    }
}
