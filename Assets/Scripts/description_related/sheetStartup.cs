using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class sheetStartup : MonoBehaviour {

    public List<GameObject> activate;
    public List<GameObject> deactivate;

    public SculptureBoard sb;
    public int index;
    
    // Start is called before the first frame update
    void Start() {
        sb = GameObject.FindWithTag("sculptureBoard").GetComponent<SculptureBoard>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void setup() {
        foreach (GameObject item in activate) {
            if (item.activeInHierarchy == false) {
                item.SetActive(true);
            }
        }

        Debug.Log("setting sculpture " + index + " status to enabled!");
        sb.changeSculptureStatus(index, true);
    }

    public void shutdown() {
        Debug.Log("CHECKPOINT");
        foreach (GameObject item in deactivate) {
            if (item.activeInHierarchy == true) {
                item.SetActive(false);
            }
        }
    }
}
