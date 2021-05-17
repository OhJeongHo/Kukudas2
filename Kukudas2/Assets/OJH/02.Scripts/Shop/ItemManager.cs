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
        coinText.GetComponent<Text>().text = "도지코인 = " + money + "원";
        cookieText.GetComponent<Text>().text = "쿠키 = " + cookie + "개";
        toiletText.GetComponent<Text>().text = "배변봉투 = " + toiletBag + "개";
    }

    public void OnClickBuyCookie()
    {
        if(money >= 100)
        {
            money -= 100;
            cookie++;
            shopText.GetComponent<Text>().text = "쿠키를 구입했습니다!";
        }
        else
        {
            shopText.GetComponent<Text>().text = "도지코인이 부족합니다!";
        }
    }

    public void OnClickBuyToiletBag()
    {
        if (money >= 100)
        {
            money -= 100;
            toiletBag++;
            shopText.GetComponent<Text>().text = "배변봉투를 구입했습니다!";
        }
        else
        {
            shopText.GetComponent<Text>().text = "도지코인이 부족합니다!";
        }
    }

    public void OnClickCookie()
    {
        if (cookie > 0)
        {
            cookie--;
            print("간식먹는 애니메이션 출력");
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            GameManager.loyalty += 10;
            GameManager.hungryTime += 10;
            text.GetComponent<Text>().text = "쿠키를 사용했습니다. 포만감+10 충성도+10";
        }
        else
        {
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            text.GetComponent<Text>().text = "쿠키가 부족합니다. 상점에서 구입하세요.";
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
                text.GetComponent<Text>().text = "배변봉투를 사용했습니다. 충성도+10";
            }
            else
            {
                GameObject text = Instantiate(infoText);
                text.transform.parent = content.transform;
                text.GetComponent<Text>().text = "배변봉투가 부족합니다. 상점에서 구입하세요.";
            }
        }
        else
        {
            GameObject text = Instantiate(infoText);
            text.transform.parent = content.transform;
            text.GetComponent<Text>().text = "배변봉투를 사용할 필요가 없습니다.";
        }

    }

}
