using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script is to be attached to an empty object in the middle of the candles room
public class CandlesApproach : MonoBehaviour {
    public GameObject ghost;
    public string[] candlesText;
    public float moveTime;

    private bool triggered;

    // Start is called before the first frame update
    void Start() {
        triggered = false;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            if (!triggered) {
                ghost.GetComponent<GhostBehavior>().moveToAndSay(this.transform.position, moveTime, candlesText);
                triggered = true;
            }
        }
    }

    IEnumerator pauseTrigger() {
        this.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSecondsRealtime(moveTime + 3);
        this.GetComponent<CapsuleCollider>().enabled = true;
    }

    private void OnTriggerExit(Collider other) {

    }
}
