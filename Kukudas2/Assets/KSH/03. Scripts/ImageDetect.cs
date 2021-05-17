using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[System.Serializable]
public class MarkerInfo
{
    public string imgName;
    public GameObject targetObj;
}

public class ImageDetect : MonoBehaviour
{
    //�̹����̸�, �ش� �̹����� ��Ÿ�� ������Ʈ
    public MarkerInfo[] markerinfos;

    //AR Tracked Image Manager
    ARTrackedImageManager trackedManager;

    void Start()
    {

        trackedManager = GetComponent<ARTrackedImageManager>();
        //������ȭ(�̹��� �νĿ���)�� ������ ȣ��Ǵ� �Լ� ���
        trackedManager.trackedImagesChanged += OnTrackedImageChanged;

    }

    private void OnDestroy()
    {
        //������ȭ(�̹��� �νĿ���)�� ������ ȣ��Ǵ� �Լ� ����
        trackedManager.trackedImagesChanged -= OnTrackedImageChanged;
    }



    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs events)
    {


        //����� ������ŭ ���Ѵ�
        for (int i = 0; i < events.updated.Count; i++)
        {

            ARTrackedImage trImage = events.updated[i];

            for (int j = 0; j < markerinfos.Length; j++)
            {
                //�νĵ� �̹���(1000won)�� MarkerInfos[0].imgName �� ���ٸ�
                if (trImage.referenceImage.name == markerinfos[j].imgName)
                {
                    //���࿡ �νĵ� �̹����� Ʈ��ŷ���̶��
                    if (trImage.trackingState == TrackingState.Tracking)
                    {
                        //MarkerInfos[0].targetObj�� Ȱ��ȭ
                        markerinfos[j].targetObj.SetActive(true);
                        //�̹����� ����ٴϰ�
                        markerinfos[j].targetObj.transform.position = trImage.transform.position;
                        //�̹����� ���ܳ��� ������ ���� ���ϰ� �����ش�.
                        markerinfos[j].targetObj.transform.up = trImage.transform.up;
                    }
                    else
                    {
                        //MarkerInfos[0].targetObj�� ��Ȱ��ȭ
                        markerinfos[j].targetObj.SetActive(false);
                    }
                }
            }
        }
    }

    void Update()
    {



    }
}
