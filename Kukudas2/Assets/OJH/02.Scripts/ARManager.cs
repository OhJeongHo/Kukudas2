using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
    public GameObject ground;
    public GameObject testCam;
    public GameObject arSession;
    public GameObject arSessionOrigin;
    public GameObject arground;

    public GameObject indicator;
    public GameObject target1, target2;
    public GameObject ui;

    // AR Raycast Manager
    ARRaycastManager rayManager;

    private void Awake()
    {
#if UNITY_EDITOR
        arSession.SetActive(false);
        arSessionOrigin.SetActive(false);
        testCam.SetActive(true);
        ground.SetActive(true);
#else
        arSession.SetActive(true);
        arSessionOrigin.SetActive(true);
        testCam.SetActive(false);
        ground.SetActive(false);
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        rayManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1. 카메라의 위치, 보는 방향으로 Ray를 만들어서
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

#if UNITY_EDITOR
        RaycastHit hit;
        int layer = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            DetectedGround(hit.point);
        }
#else
        // 2. 바닥과 닿는 지점에 Indicator를 놓고 싶다.
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if(rayManager.Raycast(ray, hits, TrackableType.Planes))
        {
            DetectedGround(hits[0].pose.position);
        }
#endif

        else
        {
            // 3. 바닥과 부딪히지 않는다면 Indicator를 비활성화
            indicator.SetActive(false);
        }

        // 만약에 화면 터치를 했다면 
        if (Input.GetMouseButtonDown(0))
        {
#if UNITY_EDITOR
            if(EventSystem.current.IsPointerOverGameObject()==false)
#else
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
#endif
                // 만약에 indicator가 활성화 되어있다면 
                if (indicator.activeSelf)
            {
                // dog 활성화
                target1.SetActive(true);
                if(target2 != null) target2.SetActive(true);
                if (arground != null) arground.SetActive(true);
                // 자동차의 위치를 indicator위치로, 회전값을 동일하게 해야함.
                target1.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                
                //// UI 활성화
                //ui.SetActive(true);
                // Indicator 비활성화
                indicator.SetActive(false);
                // ARManager 비활성화
                enabled = false;
            }
        }
    }

    void DetectedGround(Vector3 hitPos)
    {
        indicator.SetActive(true);
        indicator.transform.position = hitPos;
        // hit.point + Vector3.up * 0.01f
        // 카메라가 보는 방향으로 회전
        indicator.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
