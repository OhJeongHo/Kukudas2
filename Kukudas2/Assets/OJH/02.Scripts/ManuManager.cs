using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManuManager : MonoBehaviour
{
    public GameObject menuPage;
    public GameObject developer;
    public GameObject status;
    public GameObject orders;
    public GameObject loves;
    public GameObject homepage;
    public GameObject gopage;
    public GameObject shop;


    public void OnClickMenu()
    {
        menuPage.SetActive(true);
    }

    public void OnClickStatus()
    {
        status.SetActive(true);
    }

    public void OnClickStatusReturn()
    {
        status.SetActive(false);
    }

    public void OnClickReturn()
    {
        menuPage.SetActive(false);
    }

    public void OnClickDeveloper()
    {
        developer.SetActive(true);
    }

    public void OnClickDevelopReturn()
    {
        developer.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickHome()
    {
        // 홈 씬으로 전환
        homepage.SetActive(true);
    }

    public void OnClickGo()
    {
        gopage.SetActive(true);
    }
    public void OnClickHomeGo()
    {
        SceneManager.LoadScene("OJH_HomeScene");
    }
    public void OnClickMiniGo()
    {
        SceneManager.LoadScene("MainHouseScene");
    }
    public void OnClickHomeNo()
    {
        homepage.SetActive(false);
    }
    public void OnClickGooutside()
    {
        SceneManager.LoadScene("OJH_GroundScene");
    }
    public void OnClickGoNo()
    {
        gopage.SetActive(false);
    }
    public void OnClickShop()
    {
        if (shop.activeSelf) shop.SetActive(false);
        else shop.SetActive(true);
    }

    public void OnShopReturn()
    {
        shop.SetActive(false);
    }

    public void OnClickOrder()
    {
        if (loves.activeSelf == true)
        {
            loves.SetActive(false);
        }
        // 명령어들 등장
        if (orders.activeSelf == true)
        {
            orders.SetActive(false);
        }
        else
        {
            orders.SetActive(true);
        }
    }


    public void OnClickLove()
    {
        if (orders.activeSelf == true)
        {
            orders.SetActive(false);
        }
        // 각종 상호작용 등장
        if (loves.activeSelf == true)
        {
            loves.SetActive(false);
        }
        else
        {
            loves.SetActive(true);
        }
    }
}
