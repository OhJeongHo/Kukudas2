using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PuppyCleanState : MonoBehaviour
{

    public float puppyCleanValue = 50;
    float pvc;
    GameObject germ;
    public GameObject puppyCleanUi;

    //���� �������� ������ �浹�� �ϰԵǸ�
    //������ ��������
    //�������� CleanState�� ��ġ 1�� �پ���.
    //�������� CleanState�� 0�� �Ǹ�
    //"�������� �������ϴ�. ����� ��Ű����." ���� ���

    void Start()
    {
        //germ = GameObject.Find("Germ");
        //�ʱ� û�ᵵ ����
        pvc = puppyCleanValue;
        Slider slider = puppyCleanUi.GetComponent<Slider>();
        slider.value = 1;
    }

    void Update()
    {
        Slider slider = puppyCleanUi.GetComponent<Slider>();
        //slider.value = pvc / puppyCleanValue;
        slider.value -= Time.deltaTime * 0.003f;
        //print("�������� û�ᵵ : " + slider.value);

        if (slider.value <= 0)
        {
            print("�������� �������ϴ�. ����� ��Ű����.");
            slider.value = 0;
        }
    }

    public void DamagedAction(float damage)
    {
        pvc -= damage;
        print("���� û�ᵵ : " + pvc);
    }
}

