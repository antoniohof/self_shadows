using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogObject : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject box;

    public float distance = 0.0f;
    public float lastDistance = 0.0f;

    public LoggingSystem log;
    public int checker = 0;

    public int grabChecker = 0;


    public GameObject handR;
    public GameObject handL;

    bool inside = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, box.transform.position);

        grabChecker++;

        if (grabChecker > 20)
        {
            float distanceToHandR = Vector3.Distance(transform.position, handR.transform.position);
            float distanceToHandL = Vector3.Distance(transform.position, handL.transform.position);

            grabChecker = 0;
            if (distanceToHandR < 0.2f || distanceToHandL < 0.2f)
            {
                log.writeMessageWithTimestampToLog("object " + gameObject.name + " is being grabbed");
                Debug.Log("grabbed!"); 
            }
            lastDistance = distance;
        }


        if (distance < 2.5f)
        {
            checker++;
            if (!inside) {
                log.writeMessageWithTimestampToLog("object" + gameObject.name + " distance to box:" + distance.ToString());
            }

            if (checker > 20)
            {
                if (distance < 1.0f)
                {
                    log.writeMessageWithTimestampToLog("object" + gameObject.name + " inside box");
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
