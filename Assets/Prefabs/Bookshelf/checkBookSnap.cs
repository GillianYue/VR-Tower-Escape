using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBookSnap : MonoBehaviour
{
    public OVRGrabber grabber;
    private bool prevGrab;

    public bookshelf bShelf;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OVRGrabbable gb = null;
        bool currGrab = ((gb = grabber.grabbedObject) != null);
        if (!currGrab && prevGrab) //just let go of an obj
        {
           bShelf.checkSnapBook();
            ///////DEBUG.LOG
        }
        prevGrab = currGrab;
    }
}
