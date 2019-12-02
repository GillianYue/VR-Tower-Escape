using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class vbHover : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject vButtonObj;
    public VirtualButtonBehaviour vbb;
    public GameObject loadingBarObj;
    public UnityEngine.UI.Image loadBar;
    public SculptureBoard sb;

    private bool _onOff;

    private float startTime;
    private float holdTime;
    private bool isPressed;


    // Start is called before the first frame update
    void Start()
    {
        vbb = vButtonObj.GetComponent<VirtualButtonBehaviour>();

        _onOff = false;
        startTime = 0f;
        holdTime = 1f;
        isPressed = false;
        loadingBarObj.SetActive(false);

        vbb.RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == true)
        {
            startTime += Time.deltaTime;
            loadBar.fillAmount = startTime / holdTime;
            Debug.Log("fillAmount: "+loadBar.fillAmount);
            //change to >= for turning script
            if(startTime >= holdTime)
            {
                if (vbb.VirtualButtonName.Equals("next"))
                {
                    sb.nextSculpture();
                }else if (vbb.VirtualButtonName.Equals("description"))
                {
                    //call anim
                    sb.bringUpDescription();
                }
                _onOff = !_onOff;
                startTime = 0f;
                isPressed = false;
                loadingBarObj.SetActive(false);
            }
        }
    }

    public void OnButtonPressed(VirtualButtonBehaviour vButt)
    {
        isPressed = true;
        startTime = 0f;
        loadingBarObj.SetActive(true);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vButt)
    {
        startTime = 0f;
        isPressed = false;
        loadingBarObj.SetActive(false);
    }
}
