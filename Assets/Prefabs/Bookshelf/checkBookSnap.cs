using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkBookSnap : MonoBehaviour
{
    public OVRGrabber grabber;
    private bool prevGrab;

    public bookshelf bShelf;
    public OVRInput.Controller rightController;

    public GameObject torch;

    void Start()
    {
        torch.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        OVRGrabbable gb = null;
        bool currGrab = ((gb = grabber.grabbedObject) != null);
        if (!currGrab && prevGrab) //just let go of an obj
        {
           bShelf.checkSnapBook();
        }
        if((!prevGrab) && currGrab) //just grabbed onto an obj
        {
            HapticManager.singleton.TriggerVibration(40, 2, 255, grabber.GetController());
            if (gb.tag.Equals("torch"))
            {
                torch.GetComponent<Rigidbody>().useGravity = true;
            }
        }


        prevGrab = currGrab;


    }
}
