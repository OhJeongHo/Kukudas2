using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyMove : MonoBehaviour
{
    //���¸� ���� ����
    public puppyState state;
    //Player�� Transform
    public Transform playerB;
    //Player �߰� ����
    public float findDistance = 0.1f;
    // �������ɹ���
    public float sitDistance = 0.5f;
    // �̵��ӷ�
    public float moveSpeed = 0.1f;
    // ���� �� �ִ� �Ÿ�
    public float lickDistance = 0.3f;
    // ���� ������ �ð�
    public float lickDelayTime = 5;
    //���� �ð�
    float currTime = 0;
    //�ʱ� ��ġ��
    Vector3 originPos;
    //�ִϸ�����
    Animator anim;
    //��
    public GameObject poop;
    Transform shitmanager;

    public enum puppyState
    {
        Idle,
        Move,
        Return,
        Sit,
        Wait,
        Lick,
        Follow,
        Shotted,
        Toilet,
        Hungry,

    }



    void Start()
    {

        playerB = Camera.main.transform;
        //ó�� ������ �� Idle����
        state = puppyState.Idle;
        //�ʱ� ��ġ���� ������ �ʱ� �ڸ��� ����
        originPos = transform.position;
        //Player�� Transform
        //playerB = GameObject.Find("PlayerB").transform;
        anim = GetComponentInChildren<Animator>();
        shitmanager = GetComponentInChildren<ShitManager>().transform;
    }

    void Update()
    {
        switch (state)
        {
            case puppyState.Idle:
                Idle();
                break;

            case puppyState.Move:
                Move();
                break;

            case puppyState.Return:
                Return();
                break;

            case puppyState.Sit:
                Sit();
                break;

            case puppyState.Wait:
                Wait();
                break;

            case puppyState.Lick:
                Lick();
                break;

            case puppyState.Shotted:
                Shotted();
                break;

            case puppyState.Follow:
                Follow();
                break;

            case puppyState.Toilet:
                Toilet();
                break;

            case puppyState.Hungry:
                Hungry();
                break;
                  


            default:
                break;
        }



    }


    public void Idle()
    {
        //���� �÷��̾���� �Ÿ��� �׼� ���� ������ ������ Move ���·� ��ȯ
        float dist = Vector3.Distance(transform.position, playerB.position);
        if (dist < findDistance)
        {
            state = puppyState.Move;
            print("���� ��ȯ : Idle -> Move");
            anim.SetTrigger("Move");
        }

        ToiletStep();
    }

    public void Move()
    {

        //�÷��̾���� �Ÿ�
        //���࿡ �÷��̾���� �Ÿ��� �������ɹ����� ���Դٸ�

        //�������� AR Camera(�÷��̾�)�� y���� �����
        Vector3 pos = playerB.position;
        pos.y = transform.position.y;
        if (Vector3.Distance(transform.position, pos) <= sitDistance)
        {
            //�������� ��ȯ
            state = puppyState.Sit;
            print("���� ��ȯ : Move -> Sit");
            anim.SetTrigger("Sit");
        }


        else
        {
            //�׷��� ������ �÷��̾ ���� ��������
            //p - puppy
            //1. ������ ���ϰ�(�÷��̾� <- ������)
            Vector3 dir = playerB.position - transform.position;
            dir.Normalize();
            dir.y = 0;
            //2. �� �������� ��������
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.forward = dir;
            //transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            print("���� ��ȯ : Move");
            //anim.SetTrigger("Move");
        }
    }

    void Lick()
    {

        if (Vector3.Distance(transform.position, playerB.position) < lickDistance)
        {
            anim.SetTrigger("Move");
            state = puppyState.Move;
        }

        //yield return new WaitForSeconds(1);


        //if (Vector3.Distance(transform.position, playerB.position) > lickDistance)
        //{
        //    state = puppyState.Move;
        //}

        //else
        //{
        //    StartCoroutine(Lick());
        //}


        //���� ���������� �Ÿ��� �÷��̾��� �Ÿ��� �־����ٸ�
        //�������� Move ���·� �Ѵ�.

        //�÷��̾���� �Ÿ�
        ////���� �÷��̾���� �Ÿ��� ���� �� �ִ� �Ÿ����
        //if (Vector3.Distance(transform.position, playerB.position) > lickDistance)
        //{
        //    state = puppyState.Move;
        //}
        //else
        //{
        //    state = puppyState.Lick;
        //}

        //currTime += Time.deltaTime;
        ////���� ������ 
        //if (currTime > lickDelayTime)
        //{
        //    print("���δ� �����~!");
        //    //�÷��̾ ���´�
        //    anim.SetTrigger("Lick");
        //    currTime = 0;
        //}
    }

    public void Sit()
    {
        //if (Vector3.Distance(transform.position, playerB.position) < sitDistance)
        //{
        //    Lick();
        //}
        //else
        //{
        //    state = puppyState.Move;
        //    print("���� ��ȯ : Sit -> Move");
        //    anim.SetTrigger("Move");
        //}
    }

    void Return()
    {

        //state = puppyState.Return;
        //��ư�� ������  
        //�������� ���¸� Return���� �ٲٰ�
        //���������� �Ÿ��� �ʱ���ġ�� �Ÿ��� ���ϰ�
        //�ʱ���ġ <- ������
        Vector3 dirt = originPos - transform.position;
        dirt.Normalize();
        dirt.y = 0;
        //�� �Ÿ��� ���� �̵��ϰ� �Ѵ�.
        transform.forward = dirt;
        transform.position += dirt * moveSpeed * Time.deltaTime;

        //transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        //�������� ���� ������ originPos�� ������ �Ѵ�.
        
        moveDist -= moveSpeed * Time.deltaTime;

        if (moveDist <= 0)

        //if (Vector3.Distance(originPos, transform.position) < 0.1f)
        {
            //state = puppyState.Idle;
            transform.position = originPos;
            moveDist -= moveSpeed * Time.deltaTime;
            
            
            print("���� ��ȯ : Return -> Idle");
            anim.SetTrigger("Idle");
        }
    }

    void Follow()
    {
        state = puppyState.Move;
        anim.SetTrigger("Move");
    }

    void Wait()
    {
        anim.SetTrigger("Wait");
        //��ư�� ������ ��ٸ��� ���·� �����.
        print("���� ��ȯ : Wait");
        //state = puppyState.Wait;
    }

    void Shotted()
    {
        anim.SetTrigger("Shotted");
        print("���� ��ȯ : Shotted : ����!");
    }

    float moveDist;
    public void OnClickReturn()
    {
        state = puppyState.Return;
        anim.SetTrigger("Move");
        moveDist = Vector3.Distance(originPos, transform.position);
    }
    public void OnClickWait()
    {
        state = puppyState.Wait;
    }
    public void OnClickShotted()
    {
        //StopAllCoroutines();
        state = puppyState.Shotted;
        //state = puppyState.Shotted;
        //anim.SetTrigger("Shotted");
    }
    public void OnClickFollow()
    {
        state = puppyState.Follow;
        anim.SetTrigger("Move");
        //playerB = GameObject.Find("PlayerB").transform;
        //Vector3 dir = playerB.transform.position - transform.position;
        //dir.Normalize();
        //transform.position += dir * moveSpeed * Time.deltaTime;
        //print("���� ��ȯ : �̸���");
    }
    public void OnClickSpray()
    {
        PlayerSanitizer ps = GetComponent<PlayerSanitizer>();
        ps.SanitizerSpray();
    }
    void ToiletStep()
    {
        if (GameManager.toiletTime >= GameManager.toiletsetTime)
        {
            state = puppyState.Toilet;
        }
    }

    void Toilet()
    {
        GameObject poo = Instantiate(poop);
        poo.transform.position = shitmanager.transform.position;
        Invoke("ToiletReset", 2f);

       
    }
    void ToiletReset()
    {
        GameManager.toiletTime = 0;
    }
    void Hungry()
    {
        state = puppyState.Hungry;
        anim.SetTrigger("Hungry");
        if (GameManager.hungryTime / GameManager.hungrysetTime <= 80f)
        {
            //state = DogState.Free;
            anim.SetTrigger("Idle");
        }
    }


}

