using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPortal : MonoBehaviour
{

    bool playerInside;
    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {
        if (nextSceneName.Equals(""))
        {
            nextSceneName = "SampleScene";
        }
    }

    void Update()
    {

        if (playerInside)
        {
            if (OVRInput.Get(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.T))
            {
                //notice this line!
                SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "PlayerBody")
        {
            //FadeIn();
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "PlayerBody")
        {
            // FadeOut();
            playerInside = false;
        }
    }

    //public void FadeOut()
    //{
    //    StartCoroutine(FadeOutRoutine());
    //}
    //private IEnumerator FadeOutRoutine()
    //{
    //    playerInside = false;
    //    Text text = UI.GetComponent<Text>();
    //    for (float t = 0.01f; t < fadeTime; t += Time.deltaTime)
    //    {
    //        text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeTime));
    //        yield return null;
    //    }
    //}

    //public void FadeIn()
    //{
    //    StartCoroutine(FadeInRoutine());
    //}

    //private IEnumerator FadeInRoutine()
    //{
    //    yield return new WaitForSeconds(3f);
    //    Text text = UI.GetComponent<Text>();
    //    text.color = Color.clear;
    //    for (float t = 0.01f; t < fadeTime; t += Time.deltaTime)
    //    {
    //        text.color = Color.Lerp(Color.clear, originalColor, Mathf.Min(1, t / fadeTime));
    //    }
    //    playerInside = true;
    //}
}
