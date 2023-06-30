using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBasketball : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject cart;

    public float distance = 0.0f;
    public LoggingSystem log;
    public int checker = 0;
    public int grabChecker = 0;

    public float lastDistance = 0.0f;


    public GameObject handR;
    public GameObject handL;

    bool inside = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, cart.transform.position);

        grabChecker++;

        if (grabChecker > 20)
        {
            float distanceToHandR = Vector3.Distance(transform.position, handR.transform.position);
            float distanceToHandL = Vector3.Distance(transform.position, handL.transform.position);

            grabChecker = 0;
            if (distanceToHandR < 0.2f || distanceToHandL < 0.2f)
            {
                log.writeMessageWithTimestampToLog("object " + gameObject.name + " was grabbed");
            }
            lastDistance = distance;
        }

        if (distance < 2.5f)
        {
            checker++;
            if (!inside)
            {
                log.writeMessageWithTimestampToLog("basketball distance:" + distance.ToString());
            }


            if (checker > 200)
            {
                if (distance < 1.7f)
                {
                    log.writeMessageWithTimestampToLog("basketball inside cart");
                    inside = true;
                }
                checker = 0;
            }
        }
        else
        {
            checker = 0;
        }
    }
}
