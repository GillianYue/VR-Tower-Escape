using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SculptureBoard : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject currSculpture, pedestal;
    public GameObject[] sculptures, sculpture_info; //prefabs of sculptures
    private bool[] sculpture_status;
    public int currSculptureIndex;
    private int maxIndex;
    private GameObject currInfo;

    private float yBeforeRise = -12f, yAfterRise = 0; //local y values for the pedestal prefab in rise animations
    private float rotateSpd; //normal range is around 40-120

    public VirtualButtonBehaviour[] virtualButtons;
    private bool canRotate, sbActive;

    // Start is called before the first frame update
    void Start()
    {
        sculpture_status = new bool[sculptures.Length]; //all false on start

        currSculptureIndex = 0;
        maxIndex = sculptures.Length - 1;

        foreach (VirtualButtonBehaviour vbb in virtualButtons)
        {
            vbb.RegisterEventHandler(this);
        }

        rotateSpd = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(canRotate && currSculpture != null)
        {
            currSculpture.transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotateSpd);
        }
    }

    public void nextSculpture()
    {
        if (currSculptureIndex < maxIndex)
        {
            currSculptureIndex++;
        }
        else if (currSculptureIndex == maxIndex)
        {
            currSculptureIndex = 0;
        }
        else
        {
            Debug.Log("sculpture index out of bound");
        }

        updateSculpture();
    }

    public void bringUpDescription()
    {
        if (currInfo != null) Destroy(currInfo);
        currInfo = Instantiate(sculpture_info[currSculptureIndex]);
        currInfo.transform.parent = transform;
    }

    public void updateSculpture()
    {

        Debug.Log("updating sculpture");

        if (currSculpture != null)
            Destroy(currSculpture);
        if (sculpture_status[currSculptureIndex])
        {
            Debug.Log("instantiating sculpture");
            currSculpture = Instantiate(sculptures[currSculptureIndex]);
            currSculpture.transform.parent = pedestal.transform;
            currSculpture.transform.localRotation = Quaternion.Euler(0, 0, 0);
            currSculpture.transform.localPosition = new Vector3(0, 0, 0);

            if (currInfo != null) Destroy(currInfo);
        }
    }

    public void setSculptureRotationSpd(float spd)
    {
        if(currSculpture != null)
        {
            rotateSpd = spd;
        }
    }

    public void setActivity(bool b)
    {
        sbActive = b;
    }

    public void changeSculptureStatus(int index, bool status)
    {
        if(index <= maxIndex)
        sculpture_status[index] = status;
        Debug.Log("setting sculp status of index " + index + " to " + sculpture_status[index]);

        if(currSculptureIndex == index && sbActive)
        {
            updateSculpture(); //this way just setting the status will make sure GO & bool synchronicity
        }
    }

    public IEnumerator riseUpSculpture()
    {
        if (currSculpture == null) updateSculpture();


            Vector3 start = new Vector3(0, yBeforeRise, 0),
                end = new Vector3(0, yAfterRise, 0);
            canRotate = false;

            pedestal.transform.localPosition = start;
           
             for (float i = 0; i < 1; i += 0.01f)
            {
                pedestal.transform.localPosition = Vector3.Lerp(start, end, i);
              
                yield return new WaitForSeconds(0.01f); 
            }

            canRotate = true;

    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "rotate_cw":
                Debug.Log("start rotating cw");
                setSculptureRotationSpd(30f);
                break;
            case "rotate_ccw":
                Debug.Log("start rotating ccw");
                setSculptureRotationSpd(-30f);
                break;
            default:
                Debug.Log("error finding the virtualButton with name " + vb.VirtualButtonName);
                break;
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        switch (vb.VirtualButtonName)
        {
            case "rotate_cw":
                Debug.Log("stop rotating cw");
                setSculptureRotationSpd(0);
                break;
            case "rotate_ccw":
                Debug.Log("stop rotating ccw");
                setSculptureRotationSpd(0);
                break;
            default:
                Debug.Log("error finding the virtualButton with name " + vb.VirtualButtonName);
                break;
        }

        StartCoroutine(virtualButtonCoolOff(vb));
    }

    private IEnumerator virtualButtonCoolOff(VirtualButtonBehaviour vb)
    {
        canRotate = false;
        vb.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        vb.gameObject.SetActive(true);
        canRotate = true;
    }
}
