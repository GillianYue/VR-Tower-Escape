using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPanimFunc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateChildren()
    {
        for(int c = 0;  c < transform.childCount; c++)
        {
            transform.GetChild(c).gameObject.SetActive(true);
        }
    }

    public void deactivateChildren()
    {
        for (int c = 0; c < transform.childCount; c++)
        {
            //if(!transform.GetChild(c).CompareTag("plane"))
            transform.GetChild(c).gameObject.SetActive(false);
        }
    }

    public void playAnim(string animName)
    {
        GetComponent<Animator>().Play(animName);
    }
}
