using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// This script is to be attached to the ghost NPC object
public class GhostBehavior : MonoBehaviour {

    public Animator anim;                       // The ghost's animator, "Ghost"
    public GameObject dialogueSystem;           // The empty object

    // The text that the ghost will say
    public string[] introText;
    public GameObject playerCam;

    private bool isAppeared;                    // Used to check if the ghost has appeared yet
    private GameObject UI;
    private TextMeshProUGUI dialogueText;
    private AudioSource source;
    private GameObject emptyParent;             // Instantiated to position the ghost properly
    private float setHeight;                    // Sets the height the ghost floats at

    // Start is called before the first frame update
    void Start() {
        setHeight = -0.8f;
        isAppeared = false;
        UI = dialogueSystem.transform.Find("Dialogue UI").gameObject;
        dialogueText = UI.GetComponentInChildren<TextMeshProUGUI>();
        source = this.GetComponent<AudioSource>();

        emptyParent = Instantiate(new GameObject());
        emptyParent.transform.position = new Vector3(transform.position.x, setHeight, transform.position.z);
        transform.parent = emptyParent.transform;
        transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() {
        this.transform.LookAt(playerCam.transform, Vector3.up);
    }


    // Moves the ghost to "loc" in "sec" seconds & says some text
    public void moveToAndSay(Vector3 loc, float sec, string[] text) {
        StopAllCoroutines();
        UI.SetActive(false);
        if (Vector3.Distance(loc, emptyParent.transform.position) > 0.5f) {
            StartCoroutine(moveToAndSayIE(loc, sec, text));
        }
    }

    IEnumerator moveToAndSayIE(Vector3 loc, float sec, string[] text) {
        Vector3 startPos = emptyParent.transform.position;
        float amount = sec / 100;

        for (float i=0; i<=1; i += 0.01f) {
            Vector3 l = Vector3.Lerp(startPos, loc, i);
            emptyParent.transform.position = new Vector3(l.x, setHeight, l.z);
            yield return new WaitForSecondsRealtime(amount);
        }

        ReadText(text);
    }

    /*
    // Just moves the ghost, doesn't say anything
    public void moveTo(Vector3 loc, float sec) {
        Debug.Log("THE FUNCTION WAS CALLED.");
        StopAllCoroutines();
        UI.SetActive(false);
        if (Vector3.Distance(loc, emptyParent.transform.position) > 0.5f) {
            StartCoroutine(moveToIE(loc, sec));
        }
    }

    IEnumerator moveToIE(Vector3 loc, float sec) {
        Vector3 startPos = emptyParent.transform.position;
        float amount = sec / 100;

        for (float i = 0; i <= 1; i += 0.01f) {
            Vector3 l = Vector3.Lerp(startPos, loc, i);
            emptyParent.transform.position = new Vector3(l.x, setHeight, l.z);
            yield return new WaitForSecondsRealtime(amount);
        }
    }
    */

    // Intro dialogue (Keept due to being called from the animation state machine)
    public void Intro() {
        StopAllCoroutines();
        StartCoroutine(TimedText(introText));
    }

    // Called from other functions
    public void ReadText(string[] text) {
        StopAllCoroutines();
        StartCoroutine(TimedText(text));
    }

    // Reads out each line of text
    private IEnumerator TimedText(string[] text) {
        UI.SetActive(true);

        foreach (string line in text) {
            string lineR = line.Replace("\\n", "\n");
            StartCoroutine(TextLine(lineR));
            yield return new WaitForSecondsRealtime((0.05f * lineR.Length) + 3);
        }

        UI.SetActive(false);
    }

    // The character-by-character printing of text
    private IEnumerator TextLine(string inputText) {
        // We need to instantiate these variables for later
        int totalVisChars;
        int counter;
        int visibleCount;

        // Put the input text into the TextMesh Pro
        dialogueText.text = inputText;

        // The below block causes each letter to appear one at a time.
        totalVisChars = inputText.Length;
        counter = 0;
        visibleCount = counter % (totalVisChars + 1);
        while (visibleCount < totalVisChars) {
            visibleCount = counter % (totalVisChars + 1);
            dialogueText.maxVisibleCharacters = visibleCount;
            source.Play();
            counter += 1;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        // Wait a bit to let player read
        yield return new WaitForSecondsRealtime(2);
    }

    // Trigger to fade the ghost in
    private void OnTriggerEnter(Collider other) {
        if (!isAppeared && other.tag == "Player") {
            anim.SetTrigger("ApproachGhost");
            isAppeared = true;
        }
    }

    private void OnTriggerExit(Collider other) {

    }
}