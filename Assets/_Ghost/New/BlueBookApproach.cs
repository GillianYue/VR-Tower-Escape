using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This script is to be attached to the area where the blue book is in the room.
public class BlueBookApproach : MonoBehaviour {
    public GameObject ghost;
    public string[] blueBookText;
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
                ghost.GetComponent<GhostBehavior>().moveToAndSay(this.transform.position, moveTime, blueBookText);
                triggered = true;
            }
        }
    }

    IEnumerator pauseTrigger() {
        this.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSecondsRealtime(moveTime + 3);
        this.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerExit(Collider other) {
        
    }
}
