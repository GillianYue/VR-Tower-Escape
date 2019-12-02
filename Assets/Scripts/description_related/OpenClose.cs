using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class OpenClose : MonoBehaviour, ITrackableEventHandler {

    public Animator anim;

    // Use this for initialization
    void Start() {
        //anim = theObject.GetComponent<Animator>();
        TrackableBehaviour theTrackable = GetComponent<TrackableBehaviour>();
        if (theTrackable) {
            //Debug.Log("********************************** setting event handler");
            theTrackable.RegisterTrackableEventHandler(this);
        }

        anim.SetBool("Reco", false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
        // Debug.Log("********************************** state change");
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
            //Debug.Log(">>>>>>>>>>>>>>>>>>>> playing opening animation");
            // Play open animation
            anim.SetBool("Reco", true);

        } else {
            //Debug.Log(">>>>>>>>>>>>>>>>>>>> playing closing animation");
            // image target lost
            anim.SetBool("Reco", false);
        }
    }
}