using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARGroundPosition : MonoBehaviour
{
    public GameObject player;
    public Transform dog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, dog.position.y, player.transform.position.z);
    }
}
