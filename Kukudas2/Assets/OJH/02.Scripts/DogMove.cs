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
        Hungry,
        Frisbee,
        FrisbeeBack
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
    Rigidbody rb;
    GameObject shotball;
    public GameObject dogBall;
    bool frisbeeActive = true;
    bool hungryinfo; // Ʈ�翴�µ� ���� �ٲ�
    bool toiletinfo; // Ʈ�翴�µ� ���� �ٲ�

    public AudioSource dogBark;
    public AudioClip dogSound;

    #region �߿ܾ� �Ÿ�����
    // Free
    float setTime = 3;
    float currTime = 0;
    // Idle
    const float trakingDistance = 5;
    const float idleDistance = 1.5f;
    // Follow
    const float stopDistance = 1.5f;
    const float followDistance = 3;
    // forward
    bool forwardOn = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        state = DogState.Free;
        anim = GetComponentInChildren<Animator>();
        currTime = setTime;
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
            case DogState.Frisbee:
                Frisbee();
                break;
            case DogState.FrisbeeBack:
                FrisbeeBack();
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

        if (GameManager.hungryTime <= GameManager.hungrysetTime / 2 && hungryinfo == true)
        {
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            text.GetComponent<Text>().text = "�������� ������մϴ�. ������ �ּ���!";
            BarkSound();
            hungryinfo = false;
            
        }

        if (GameManager.hungryTime >= GameManager.hungrysetTime / 2)
        {
            hungryinfo = true;
        }

        if (frisbee == true && frisbeeActive == true)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hit;
            int layer = 1 << LayerMask.NameToLayer("Ground");
            if (Physics.Raycast(ray, out hit, 100, layer))
            {
                DetectedGround(hit.point);
            }
            else
            {
                // 3. �ٴڰ� �ε����� �ʴ´ٸ� Indicator�� ��Ȱ��ȭ
                indicator.SetActive(false);
            }
            // ���࿡ ȭ�� ��ġ�� �ߴٸ� 
            if (Input.GetMouseButtonDown(0))
            {
#if UNITY_EDITOR
                if (EventSystem.current.IsPointerOverGameObject() == false)
#else
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
#endif
                    if (indicator.activeSelf)
                    {
                        // �ε������� �������� ���� ���ư����� - ���� �����ϰ� �ε������� ���� dir���� �̵����Ѿ� ��.
                        GameObject balls = Instantiate(ball);
                        shotball = balls;
                        balls.transform.position = Camera.main.transform.position;
                        Vector3 dir = indicator.transform.position - balls.transform.position;
                        dir.Normalize();
                        balls.transform.forward = dir;

                        rb = balls.gameObject.GetComponent<Rigidbody>();
                        rb.AddForce(balls.transform.forward * 600);

                        state = DogState.Frisbee;

                        indicator.SetActive(false);
                        anim.SetTrigger("Frisbee");
                        frisbee = false;
                        frisbeeActive = false;
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
        HungryStep();
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
        HungryStep();
    }

    void Traking()
    {
        Vector3 dir = new Vector3(player.position.x - gameObject.transform.position.x, 0, player.transform.position.z - transform.position.z);
        transform.forward = dir;
        dir.Normalize();
        transform.position += dir * 1 * Time.deltaTime;
        ToiletStep();
        HungryStep();
        if (Vector3.Distance(player.position, transform.position) <= idleDistance)
        {
            state = DogState.Idle;
            anim.SetTrigger("Idle");
        }
    }

    void Follow()
    {
        Vector3 dir = new Vector3(player.position.x - gameObject.transform.position.x, 0, player.transform.position.z - transform.position.z);
        transform.forward = dir;
        dir.Normalize();
        transform.position += dir * 1.5f * Time.deltaTime;
        ToiletStep();
        HungryStep();
        if (Vector3.Distance(player.position, transform.position) <= stopDistance)
        {
            state = DogState.FollowStop;
            anim.SetTrigger("FollowStop");
        }
    }

    public void OnClickFree()
    {
        if (state == DogState.Hungry)
        {
            return;
        }
        state = DogState.Free;
        anim.SetTrigger("Free");
    }

    public void OnClickIdle()
    {
        if (state == DogState.Hungry)
        {
            return;
        }
        state = DogState.Idle;
        anim.SetTrigger("Idle");
    }

    public void OnClickFollow()
    {
        if (state == DogState.Hungry)
        {
            return;
        }
        state = DogState.Follow;
        anim.SetTrigger("Follow");
        
    }

    void FollowStop()
    {
        ToiletStep();
        HungryStep();
        if (Vector3.Distance(player.position, transform.position) >= followDistance)
        {
            state = DogState.Follow;
            anim.SetTrigger("Follow");
        }
    }

    void Frisbee()
    {
        if (state == DogState.Hungry)
        {
            return;
        }
        Vector3 dir = new Vector3(shotball.transform.position.x - gameObject.transform.position.x, 0, shotball.transform.position.z - gameObject.transform.position.z);
        dir.Normalize();
        gameObject.transform.forward = dir;
        transform.position += dir * 2 * Time.deltaTime;
        
    }
    void FrisbeeBack()
    {
        Vector3 dir = new Vector3(player.position.x - gameObject.transform.position.x, 0, player.transform.position.z - transform.position.z);
        // Vector3 dir = player.position - gameObject.transform.position;
        transform.forward = dir;
        dir.Normalize();
        transform.position += dir * 2 * Time.deltaTime;
        if (Vector3.Distance(player.position, transform.position) <= stopDistance)
        {
            dogBall.SetActive(false);
            frisbeeActive = true;
            state = DogState.FollowStop;
            anim.SetTrigger("FollowStop");
        }
    }
    public void OnClickFrisbee()
    {
        if (state == DogState.Hungry)
        {
            return;
        }
        frisbee = true;
    }
    void DetectedGround(Vector3 hitPos)
    {
        indicator.SetActive(true);
        indicator.transform.position = hitPos;
        indicator.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
    }

    void ToiletStep()
    {
        if (GameManager.toiletTime >= GameManager.toiletsetTime)
        {
            GameObject poo = Instantiate(poop);
            poo.transform.position = transform.position;
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            text.GetComponent<Text>().text = "�������� ������ �ý��ϴ�. �輳���� ġ�켼��!";
            state = DogState.Toilet;
            anim.SetTrigger("Toilet");
        }
    }
    void Toilet()
    {
        //GameObject poo = Instantiate(poop);
        //poo.transform.position = transform.position;
        //GameObject text = Instantiate(infoText);
        //text.transform.parent = content.transform;
        //text.GetComponent<Text>().text = "�������� ������ �ý��ϴ�. �輳���� ġ�켼��!";
        if (toiletinfo)
        {
            toiletinfo = false;
            Invoke("ToiletReset", 2f);
        }
        // ToiletReset();
    }

    void ToiletReset()
    {
        GameManager.toiletTime = 0;
        toiletinfo = true;
        state = DogState.Follow;
        anim.SetTrigger("Follow");
    }

    void HungryStep()
    {
        if (GameManager.hungryTime <= GameManager.hungrysetTime / 4)
        {
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            text.GetComponent<Text>().text = "�������� �ʹ� ����ļ� ������ �� �����ϴ�!";
            BarkSound();
            state = DogState.Hungry;
            anim.SetTrigger("Hungry");
        }
    }
    void Hungry()
    {
        if (GameManager.hungryTime >= GameManager.hungrysetTime / 2 )
        {
            state = DogState.Free;
            anim.SetTrigger("Free");
        }
        
    }

    public void BarkSound()
    {
        dogBark.PlayOneShot(dogSound);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            // �� ������Ʈ ��������, ������ �Կ� �ִ� �� �Ҵ�.
            Destroy(other.gameObject);
            dogBall.SetActive(true);
            state = DogState.FrisbeeBack;
            anim.SetTrigger("FrisbeeBack");
        }
    }
}
