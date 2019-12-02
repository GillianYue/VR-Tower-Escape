using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestUnlock : MonoBehaviour
{

    public GameObject topChest;
    private bool opened;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("key"))
        {
            if (!opened)
            {
                topChest.GetComponent<Animator>().Play("openChest"); //unlock event
            }
            opened = true;
        }

    }
}
