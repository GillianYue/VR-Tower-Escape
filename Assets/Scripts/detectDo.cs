using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class detectDo : MonoBehaviour, ITrackableEventHandler
{
    public SculptureBoard sb;

    // Start is called before the first frame update
    void Start()
    {
        TrackableBehaviour theTrackable = GetComponent<TrackableBehaviour>();

        if (theTrackable)
        {
            theTrackable.RegisterTrackableEventHandler(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
             newStatus == TrackableBehaviour.Status.TRACKED ||
             newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            sb.gameObject.SetActive(true);
            sb.setActivity(true);
            sb.changeSculptureStatus(2, true);
            StartCoroutine(sb.riseUpSculpture());

        }
        else
        {
            //sb.gameObject.SetActive(false);
            sb.setActivity(false);
        }
    }



}
