using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    enum TEST_STATE
    {
        IDLE,
        MOVE,
        ATTACK
    }

    TEST_STATE state;
    bool isA;
    // Start is called before the first frame update
    void Start()
    {
        state = TEST_STATE.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeState(TEST_STATE.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeState(TEST_STATE.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeState(TEST_STATE.IDLE);
        }
    }

    void ChangeState(TEST_STATE s)
    {
        state = s;

        switch(state)
        {
            case TEST_STATE.IDLE:
                isA = true;
                break;
            case TEST_STATE.MOVE:
                break;

        }

    }
}
