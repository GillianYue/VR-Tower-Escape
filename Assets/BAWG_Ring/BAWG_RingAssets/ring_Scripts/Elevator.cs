using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{

    public GameObject leftDoor;
    public GameObject rightDoor;

    private Vector3 permaStartLeft;
    private Vector3 permaEndLeft;

    private Vector3 startPosLeft;
    private Vector3 endPosLeft;

    private Vector3 permaStartRight;
    private Vector3 permaEndRight;

    private Vector3 startPosRight;
    private Vector3 endPosRight;

    public float scaleAmount;
    public float scaleSpeed;

    private float startTime;
    private bool go;

    public GameObject UI;
    public float fadeTime;
    Color originalColor;
    bool playerInside;

    public string nextSceneName;



    void Start()
    {
        originalColor = UI.GetComponent<Text>().color;
        permaStartLeft = leftDoor.transform.localScale;
        permaEndLeft = new Vector3(permaStartLeft.x, permaStartLeft.y , permaStartLeft.z - scaleAmount);
        startPosLeft = permaStartLeft;
        endPosLeft = permaEndLeft;

        permaStartRight = rightDoor.transform.localScale;
        permaEndRight = new Vector3(permaStartRight.x, permaStartRight.y , permaStartRight.z - scaleAmount);
        startPosRight = permaStartRight;
        endPosRight = permaEndRight;
    }

    void Update()
    {
        if (go)
        {
            float distCovered = (Time.time - startTime) * scaleSpeed;

            float fracJourney = distCovered / scaleAmount;

            rightDoor.transform.localScale = Vector3.Lerp(startPosRight, endPosRight, fracJourney);
            leftDoor.transform.localScale = Vector3.Lerp(startPosLeft, endPosLeft, fracJourney);
            if (fracJourney > 0.99)
                go = false;
        }
        if (playerInside)
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                //notice this line!
                SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        startPosLeft = leftDoor.transform.localScale;
        endPosLeft = permaEndLeft;
        startPosRight = rightDoor.transform.localScale;
        endPosRight = permaEndRight;
        startTime = Time.time;
        if (other.tag == "Player")
        {
            go = true;
            FadeIn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        startTime = Time.time;
        startPosLeft = leftDoor.transform.localScale;
        endPosLeft = permaStartLeft;
        startPosRight = rightDoor.transform.localScale;
        endPosRight = permaStartRight;
        if (other.tag == "Player")
        {
            go = true;
            FadeOut();
        }
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }
    private IEnumerator FadeOutRoutine()
    {
        playerInside = false;
        Text text = UI.GetComponent<Text>();
        for (float t = 0.01f; t < fadeTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeTime));
            yield return null;
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        yield return new WaitForSeconds(3f);
        Text text = UI.GetComponent<Text>();
        text.color = Color.clear;
        for (float t = 0.01f; t < fadeTime; t += Time.deltaTime)
        {
            text.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t / fadeTime));
        }
        playerInside = true;
    }
}
