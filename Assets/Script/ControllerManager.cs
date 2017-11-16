using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ControllerManager : MonoBehaviour
{

    public GameObject Model;
    public VRTK_BezierPointer VrtkBezierPointer;

	// Use this for initialization
	void Start ()
	{
	    VrtkBezierPointer = GetComponent<VRTK_BezierPointer>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Model.activeInHierarchy == true)
	    {
	        VrtkBezierPointer.enabled = true;
	    }
	    else
	    {
	        VrtkBezierPointer.enabled = false;
	    }
	}
}
