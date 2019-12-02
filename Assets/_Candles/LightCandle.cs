using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandle : MonoBehaviour {

    public AudioClip lightFire;
    public OVRInput.Controller feedbackController;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Fire") {
            StartCoroutine(Ignition());
        }
    }

    private void OnTriggerExit(Collider other) {
        StopAllCoroutines();
    }

    private IEnumerator Ignition() {
        yield return new WaitForSecondsRealtime(2);
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(lightFire);
        HapticManager.singleton.TriggerVibration(lightFire, feedbackController);
    }
}