using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DogMove : MonoBehaviour
{
    enum DogState
    {
        Free,
        Idle,
        Traking,
        Follow,
        FollowStop,
        Toilet,
        Hungry
    }

    DogState state;
    public Transform player;
    public GameObject freeTarget;
    public GameObject poop;
    Animator anim;
    public GameObject content;
    public GameObject infoText;
    ARRaycastManager rayManager;
    public GameObject indicator;
    public GameObject ball;
    bool frisbee = false;

    #region 야외씬 거리설정
    // Free
    float setTime = 3;
    float currTime = 0;
    // Idle
    float trakingDistance = 5;
    float idleDistance = 0.5f;
    // Follow
    float stopDistance = 1f;
    float followDistance = 3;
    // forward
    bool forwardOn = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        state = DogState.Free;
        anim = GetComponentInChildren<Animator>();
        currTime = setTime;
        rayManager = GetComponent<ARRaycastManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case DogState.Free:
                Free();
                break;
            case DogState.Idle:
                Idle();
                break;
            case DogState.Traking:
                Traking();
                break;
            case DogState.Follow:
                Follow();
                break;
            case DogState.FollowStop:
                FollowStop();
                break;
            case DogState.Toilet:
                Toilet();
                break;
            case DogState.Hungry:
                Hungry();
                break;
            default:
                break;
        }

        if (frisbee == true)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            if (rayManager.Raycast(ray, hits, TrackableType.Planes))
            {
                DetectedGround(hits[0].pose.position);
            }
            else
            {
                // 3. 바닥과 부딪히지 않는다면 Indicator를 비활성화
                indicator.SetActive(false);
            }
            // 만약에 화면 터치를 했다면 
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
                    // 만약에 indicator가 활성화 되어있다면 
                    if (indicator.activeSelf)
                    {
                        // 인디케이터 방향으로 공이 날아가도록. 공을 추가해주고 인디케이터 방향 dir으로 이동시켜야 함.
                        // 인스탄티에이트로 넣을 것.
                        ball.SetActive(true);
                        ball.transform.position = player.transform.position;
                        Vector3 dir = indicator.transform.position - ball.transform.position;
                        ball.transform.position += dir * 1 * Time.deltaTime;

                        // 강아지 프리스비 행동관련 함수 실행
                        Frisbee();

                        // Indicator 비활성화
                        indicator.SetActive(false);
                        // ARManager 비활성화
                        enabled = false;
                        frisbee = false;
                    }
            }
        }
    }

    void Free()
    {
        freeTarget.SetActive(true);
        Vector3 dir = freeTarget.transform.position - transform.position;
        dir.Normalize();
        transform.forward = dir;
        transform.position += dir * 0.2f * Time.deltaTime;
        ToiletStep();
    }

    void Idle()
    {
        if (forwardOn)
        {
            Vector3 dir = new Vector3(player.position.x - gameObject.transform.position.x, 0, player.transform.position.z - transform.position.z);
            transform.forward = dir;
        }
        forwardOn = false;

        if (Vector3.Distance(player.position, transform.position) >= trakingDistance)
        {
            state = DogState.Traking;
            anim.SetTrigger("Traking");
        }
        ToiletStep();
    }

    void Traking()
    {
        Vector3 dir = new Vector3(player.position.x - gameObject.transform.position.x, 0, player.transform.position.z - transform.position.z);
        // Vector3 dir = player.position - gameObject.transform.position;
        transform.forward = dir;
        dir.Normalize();
        transform.position += dir * 1 * Time.deltaTime;
        ToiletStep();
        if (Vector3.Distance(player.position, transform.position) <= idleDistance)
        {
            state = DogState.Idle;
            anim.SetTrigger("Idle");
        }
    }

    void Follow()
    {
        Vector3 dir = new Vector3(player.position.x - gameObject.transform.position.x, 0, player.transform.position.z - transform.position.z);
        // Vector3 dir = player.position - gameObject.transform.position;
        transform.forward = dir;
        dir.Normalize();
        transform.position += dir * 1 * Time.deltaTime;
        ToiletStep();
        if (Vector3.Distance(player.position, transform.position) <= stopDistance)
        {
            state = DogState.FollowStop;
            anim.SetTrigger("FollowStop");
        }
    }

    public void OnClickFree()
    {
        state = DogState.Free;
        anim.SetTrigger("Free");
    }
    public void OnClickIdle()
    {
        state = DogState.Idle;
        anim.SetTrigger("Idle");
    }

    public void OnClickFollow()
    {
        
        state = DogState.Follow;
        anim.SetTrigger("Follow");
        
    }

    void FollowStop()
    {
        if (Vector3.Distance(player.position, transform.position) >= followDistance)
        {
            state = DogState.Follow;
            anim.SetTrigger("Follow");
        }
    }
    void Frisbee()
    {
        
    }
    public void OnClickFrisbee()
    {
        frisbee = true;
    }
    void DetectedGround(Vector3 hitPos)
    {
        indicator.SetActive(true);
        indicator.transform.position = hitPos;
        // hit.point + Vector3.up * 0.01f
        // 카메라가 보는 방향으로 회전
        indicator.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
    }

    void ToiletStep()
    {
        if (GameManager.toiletTime >= GameManager.toiletsetTime)
        {
            state = DogState.Toilet;
            anim.SetTrigger("Toilet");
        }
    }
    void Toilet()
    {
        GameObject poo = Instantiate(poop);
        poo.transform.position = transform.position;
        GameObject text = Instantiate(infoText);
        text.transform.parent = content.transform;
        text.GetComponent<Text>().text = "강아지가 볼일을 봤습니다. 배설물을 치우세요!";
        // Invoke("ToiletReset", 2f);
        ToiletReset();
    }

    void ToiletReset()
    {
        GameManager.toiletTime = 0;
        state = DogState.Follow;
        anim.SetTrigger("Follow");
    }
    void Hungry()
    {
        state = DogState.Hungry;
        anim.SetTrigger("Hungry");
        if (GameManager.hungryTime / GameManager.hungrysetTime <= 80f )
        {
            state = DogState.Free;
            anim.SetTrigger("Free");
        }
        
    }
}
