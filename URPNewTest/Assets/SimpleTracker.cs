using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTracker : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject rightKnee;
    public GameObject leftKnee;
    public GameObject rightFoot;
    public GameObject leftFoot;


    public PoseVisuallizer3D viz;
    public PoseVisuallizer viz2D;





    public static SimpleTracker simpleTracker;


    void Start()
    {
        simpleTracker = this;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("updating");
        if (viz2D != null)
        {

            rightFoot.transform.localPosition = viz2D.rightFoot;
            leftFoot.transform.localPosition = viz2D.leftFoot;
            rightKnee.transform.localPosition = viz2D.rightKnee;
            leftKnee.transform.localPosition = viz2D.leftKnee;
        } else
        {
            rightFoot.transform.localPosition = viz.rightFoot;
            leftFoot.transform.localPosition = viz.leftFoot;
            rightKnee.transform.localPosition = viz.rightKnee;
            leftKnee.transform.localPosition = viz.leftKnee;
        }


    }
}
