using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Germs : MonoBehaviour
{
    public float gravity = 0.01f;
    float attactDistance = 1.5f;
    float attackDelay = 2;
    public float attackPower = 1;
    GameObject pcv;
    Transform Target;
    float currTime = 0;


    //세균과 강아지와의 거리를 구하고
    //만약 강아지와의 거리가 공격가능 범위가 되면
    //AttactAction을 실행하여 강아지의 청결도를 깎자
    void Start()
    {
        //1. PlayerMove 컴포넌트 가져오자.
        //PlayerMove pm = player.GetComponent<PlayerMove>();
        Target = GameObject.Find("Puppy").transform;
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.AddForce((transform.forward + transform.up) * 300);
    }

    void Update()
    {
        attackAction();

    }
    void attackAction()
    {
        float dist = Vector3.Distance(transform.position, Target.position);
        if (dist < attactDistance)
        {
            currTime += Time.deltaTime;
            //일정시간이 지나면 공격
            if (currTime > attackDelay)
            {
                print("세균 공격");
                currTime = 0;
                PuppyCleanState puppy = Target.GetComponent<PuppyCleanState>();
                puppy.DamagedAction(attackPower);

                //slider 컴포넌트 가져와서 사용하자

            }
        }
    }
}

