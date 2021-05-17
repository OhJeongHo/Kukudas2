//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class GameManagerR : MonoBehaviour
//{
//    public static GameManager instance;

//    public GameObject hungryUi;
//    public GameObject toiletUi;
//    public GameObject activityUi;

//    public static float toiletsetTime = 60f;
//    public static float hungrysetTime = 180f;
//    public static float activitysetTime = 1200f;
//    public static float toiletTime;
//    public static float hungryTime;
//    static float money;
//    public static float dogActivity;

//    void Awake()
//    {
//        if (instance == null)
//            instance = this;
//    }

//    void Update()
//    {
//        DogStatus();
//        HungryState();
//        ToiletState();
//        ActivityState();
//    }
//    public void DogStatus()
//    {
//        toiletTime += Time.deltaTime;
//        hungryTime += Time.deltaTime;
//    }

//    public void Activity(float index)
//    {
//        dogActivity += index;
//    }

//    public void InputMoney(int index)
//    {
//        money += index;
//    }
//    public void OutputMoney(int index)
//    {
//        money -= index;
//    }

//    public void HungryState()
//    {
//        Slider slider = hungryUi.GetComponent<Slider>();
//        slider.value = hungryTime / hungrysetTime;
//    }

//    public void ToiletState()
//    {
//        Slider slider = toiletUi.GetComponent<Slider>();
//        slider.value = toiletTime / toiletsetTime;
//    }

//    public void ActivityState()
//    {
//        Slider slider = activityUi.GetComponent<Slider>();
//        slider.value = dogActivity / activitysetTime;
//    }
//}
