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


    //���հ� ���������� �Ÿ��� ���ϰ�
    //���� ���������� �Ÿ��� ���ݰ��� ������ �Ǹ�
    //AttactAction�� �����Ͽ� �������� û�ᵵ�� ����
    void Start()
    {
        //1. PlayerMove ������Ʈ ��������.
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
            //�����ð��� ������ ����
            if (currTime > attackDelay)
            {
                print("���� ����");
                currTime = 0;
                PuppyCleanState puppy = Target.GetComponent<PuppyCleanState>();
                puppy.DamagedAction(attackPower);

                //slider ������Ʈ �����ͼ� �������

            }
        }
    }
}

