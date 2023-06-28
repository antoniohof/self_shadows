using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject gameObjectToFollow;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(gameObjectToFollow.transform.position.x, 0, gameObjectToFollow.transform.position.z);
    }
}
