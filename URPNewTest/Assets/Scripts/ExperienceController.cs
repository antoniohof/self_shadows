using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShadowType
{
    None,
    Avatar,
    Kinect
}


public class ExperienceController : MonoBehaviour
{
    public int subjectID = 0;

    public bool studyToggle = false;

    public GameObject leftEye;
    public GameObject rightEye;
    public LoggingSystem log;


    public bool isRunning = false;


    public float timestamp = 0.0f;

    public ShadowType shadowType;
    public bool isLookingAtShadow = false;
    public string lookingObjectName = "Nothing";

    private float timeToLog = 0.0f;
    public GameObject debugPrefab;


    // Start is called before the first frame update
    void Start()
    {
        shadowType = ShadowType.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (studyToggle && !isRunning)
        {
            initiateStudy();
        }

        if (isRunning)
        {
            timestamp += Time.deltaTime;

            if (!studyToggle)
            {
                finishStudy();
            }

            updateStudy();
        }
    }



    public void initiateStudy()
    {
        isRunning = true;
        timestamp = 0.0f;

        // start logging TODO
        log.writeMessageWithTimestampToLog("study with shadow type:" + shadowType.ToString());

    }


    public void updateStudy()
    {
        timeToLog += Time.deltaTime;

        RaycastHit hitLeft;
        // Does the ray intersect any objects excluding the player layer
        var dirLeft = leftEye.transform.TransformDirection(Vector3.left);
        // Vector3 dirLeft2 = rightEye.GetComponent<OVREyeGaze>().publicPose.orientation.ToEuler();

        dirLeft.y -= 0.18f;
        if (Physics.Raycast(leftEye.transform.position, dirLeft, out hitLeft, 1000))
        {
            Debug.DrawRay(leftEye.transform.position, dirLeft * hitLeft.distance, Color.green);
            var name = hitLeft.collider.gameObject.name;
            log.writeMessageWithTimestampToLog("looked at" + name);

            if (name == "Shadow")
            {
                isLookingAtShadow = true;
            } else
            {
                isLookingAtShadow = false;
            }
            lookingObjectName = name;

            if (timeToLog > 0.1f)
            {
                //Instantiate(debugPrefab, hitLeft.point, Quaternion.identity);
                timeToLog = 0;
            }
        } else
        {
        }
        
        // Vector3 dirRight2 = rightEye.GetComponent<OVREyeGaze>().publicPose.orientation.ToEuler();

        RaycastHit hitRight;
        // Does the ray intersect any objects excluding the player layer
        var dirRight = rightEye.transform.TransformDirection(Vector3.right);
        dirRight.y -= 0.18f;
        if (Physics.Raycast(rightEye.transform.position, dirRight, out hitRight, 1000, LayerMask.NameToLayer("collidable")))
        {
            Debug.DrawRay(rightEye.transform.position, dirRight * hitRight.distance, Color.green);
            var name = hitRight.collider.gameObject.name;
            if (name == "Shadow")
            {
                isLookingAtShadow = true;
            } else
            {
                isLookingAtShadow = false;
            }
            lookingObjectName = name;
            log.writeMessageWithTimestampToLog("looked at: " + name);

            if (timeToLog > 0.1f)
            {
                //Instantiate(debugPrefab, hitRight.point, Quaternion.identity);
                timeToLog = 0;
            }
        }
        else
        {
        }
    }


    public void finishStudy ()
    {
        isRunning = false;
    }

    private void OnDrawGizmos()
    {
        if (isRunning)
        {
            Debug.DrawRay(leftEye.transform.position, leftEye.transform.TransformDirection(Vector3.left) * 1000, Color.red);

            Debug.DrawRay(rightEye.transform.position, rightEye.transform.TransformDirection(Vector3.right) * 1000, Color.red);


        }

    }
}
