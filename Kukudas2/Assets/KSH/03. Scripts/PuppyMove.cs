using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyMove : MonoBehaviour
{
    //상태를 담을 변수
    public puppyState state;
    //Player의 Transform
    public Transform playerB;
    //Player 발견 범위
    public float findDistance = 0.1f;
    // 앉음가능범위
    public float sitDistance = 0.5f;
    // 이동속력
    public float moveSpeed = 0.1f;
    // 핢을 수 있는 거리
    public float lickDistance = 0.3f;
    // 핢는 딜레이 시간
    public float lickDelayTime = 5;
    //현재 시간
    float currTime = 0;
    //초기 위치값
    Vector3 originPos;
    //애니메이터
    Animator anim;
    //똥
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
        //처음 시작할 땐 Idle상태
        state = puppyState.Idle;
        //초기 위치값을 강아지 초기 자리로 설정
        originPos = transform.position;
        //Player의 Transform
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
        //만일 플레이어와의 거리가 액션 시작 범위에 들어오면 Move 상태로 전환
        float dist = Vector3.Distance(transform.position, playerB.position);
        if (dist < findDistance)
        {
            state = puppyState.Move;
            print("상태 전환 : Idle -> Move");
            anim.SetTrigger("Move");
        }

        ToiletStep();
    }

    public void Move()
    {

        //플레이어와의 거리
        //만약에 플레이어와의 거리가 앉음가능범위에 들어왔다면

        //강아지와 AR Camera(플레이어)의 y값을 맞춰라
        Vector3 pos = playerB.position;
        pos.y = transform.position.y;
        if (Vector3.Distance(transform.position, pos) <= sitDistance)
        {
            //앉음으로 전환
            state = puppyState.Sit;
            print("상태 전환 : Move -> Sit");
            anim.SetTrigger("Sit");
        }


        else
        {
            //그렇지 않으면 플레이어를 향해 움직이자
            //p - puppy
            //1. 방향을 구하고(플레이어 <- 강아지)
            Vector3 dir = playerB.position - transform.position;
            dir.Normalize();
            dir.y = 0;
            //2. 그 방향으로 움직이자
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.forward = dir;
            //transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            print("상태 전환 : Move");
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


        //만약 강아지와의 거리와 플레이어의 거리가 멀어진다면
        //강아지를 Move 상태로 한다.

        //플레이어와의 거리
        ////만약 플레이어와의 거리가 앉을 수 있는 거리라면
        //if (Vector3.Distance(transform.position, playerB.position) > lickDistance)
        //{
        //    state = puppyState.Move;
        //}
        //else
        //{
        //    state = puppyState.Lick;
        //}

        //currTime += Time.deltaTime;
        ////앉은 다음에 
        //if (currTime > lickDelayTime)
        //{
        //    print("주인님 놀아줘~!");
        //    //플레이어를 핣는다
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
        //    print("상태 전환 : Sit -> Move");
        //    anim.SetTrigger("Move");
        //}
    }

    void Return()
    {

        //state = puppyState.Return;
        //버튼을 누르면  
        //강아지의 상태를 Return으로 바꾸고
        //강아지와의 거리와 초기위치의 거리를 구하고
        //초기위치 <- 강아지
        Vector3 dirt = originPos - transform.position;
        dirt.Normalize();
        dirt.y = 0;
        //그 거리를 향해 이동하게 한다.
        transform.forward = dirt;
        transform.position += dirt * moveSpeed * Time.deltaTime;

        //transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        //강아지의 정면 방향을 originPos로 돌려야 한다.
        
        moveDist -= moveSpeed * Time.deltaTime;

        if (moveDist <= 0)

        //if (Vector3.Distance(originPos, transform.position) < 0.1f)
        {
            //state = puppyState.Idle;
            transform.position = originPos;
            moveDist -= moveSpeed * Time.deltaTime;
            
            
            print("상태 전환 : Return -> Idle");
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
        //버튼을 누르면 기다리는 상태로 만든다.
        print("상태 전환 : Wait");
        //state = puppyState.Wait;
    }

    void Shotted()
    {
        anim.SetTrigger("Shotted");
        print("상태 전환 : Shotted : 으악!");
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
        //print("상태 전환 : 이리와");
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

