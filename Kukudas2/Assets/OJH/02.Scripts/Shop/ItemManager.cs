using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    public int cookie;
    public int toiletBag;
    public int money = 1000;
    public Text coinText, cookieText, toiletText, shopText;
    public GameObject poo;
    public GameObject content;
    public GameObject infoText;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        coinText.GetComponent<Text>().text = "�������� = " + money + "��";
        cookieText.GetComponent<Text>().text = "��Ű = " + cookie + "��";
        toiletText.GetComponent<Text>().text = "�躯���� = " + toiletBag + "��";
    }

    public void OnClickBuyCookie()
    {
        if(money >= 100)
        {
            money -= 100;
            cookie++;
            shopText.GetComponent<Text>().text = "��Ű�� �����߽��ϴ�!";
        }
        else
        {
            shopText.GetComponent<Text>().text = "���������� �����մϴ�!";
        }
    }

    public void OnClickBuyToiletBag()
    {
        if (money >= 100)
        {
            money -= 100;
            toiletBag++;
            shopText.GetComponent<Text>().text = "�躯������ �����߽��ϴ�!";
        }
        else
        {
            shopText.GetComponent<Text>().text = "���������� �����մϴ�!";
        }
    }

    public void OnClickCookie()
    {
        if (cookie > 0)
        {
            cookie--;
            print("���ĸԴ� �ִϸ��̼� ���");
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            GameManager.loyalty += 10;
            GameManager.hungryTime += 10;
            text.GetComponent<Text>().text = "��Ű�� ����߽��ϴ�. ������+10 �漺��+10";
        }
        else
        {
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            text.GetComponent<Text>().text = "��Ű�� �����մϴ�. �������� �����ϼ���.";
        }
    }


    public void OnClickToilet()
    {
        if (GameObject.FindWithTag("poop"))
        {
            if (toiletBag > 0)
            {
                Destroy(GameObject.FindWithTag("poop"));
                toiletBag--;
                GameObject text = Instantiate(infoText);
                text.transform.parent = content.transform;
                GameManager.loyalty += 10;
                text.GetComponent<Text>().text = "�躯������ ����߽��ϴ�. �漺��+10";
            }
            else
            {
                GameObject text = Instantiate(infoText);
                text.transform.parent = content.transform;
                text.GetComponent<Text>().text = "�躯������ �����մϴ�. �������� �����ϼ���.";
            }
        }
        else
        {
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            text.GetComponent<Text>().text = "�躯������ ����� �ʿ䰡 �����ϴ�.";
        }

    }

}
