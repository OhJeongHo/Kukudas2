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
        // 1. ī�޶��� ��ġ, ���� �������� Ray�� ����
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

#if UNITY_EDITOR
        RaycastHit hit;
        int layer = 1 << LayerMask.NameToLayer("Ground");
        if (Physics.Raycast(ray, out hit, 100, layer))
        {
            DetectedGround(hit.point);
        }
#else
        // 2. �ٴڰ� ��� ������ Indicator�� ���� �ʹ�.
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if(rayManager.Raycast(ray, hits, TrackableType.Planes))
        {
            DetectedGround(hits[0].pose.position);
        }
#endif

        else
        {
            // 3. �ٴڰ� �ε����� �ʴ´ٸ� Indicator�� ��Ȱ��ȭ
            indicator.SetActive(false);
        }

        // ���࿡ ȭ�� ��ġ�� �ߴٸ� 
        if (Input.GetMouseButtonDown(0))
        {
#if UNITY_EDITOR
            if(EventSystem.current.IsPointerOverGameObject()==false)
#else
            if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)
#endif
                // ���࿡ indicator�� Ȱ��ȭ �Ǿ��ִٸ� 
                if (indicator.activeSelf)
            {
                // dog Ȱ��ȭ
                target1.SetActive(true);
                if(target2 != null) target2.SetActive(true);
                if (arground != null) arground.SetActive(true);
                // �ڵ����� ��ġ�� indicator��ġ��, ȸ������ �����ϰ� �ؾ���.
                target1.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                
                //// UI Ȱ��ȭ
                //ui.SetActive(true);
                // Indicator ��Ȱ��ȭ
                indicator.SetActive(false);
                // ARManager ��Ȱ��ȭ
                enabled = false;
            }
        }
    }

    void DetectedGround(Vector3 hitPos)
    {
        indicator.SetActive(true);
        indicator.transform.position = hitPos;
        // hit.point + Vector3.up * 0.01f
        // ī�޶� ���� �������� ȸ��
        indicator.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
