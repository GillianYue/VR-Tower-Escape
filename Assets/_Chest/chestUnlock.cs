using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestUnlock : MonoBehaviour
{

    public GameObject topChest;
    private bool opened;
    public AudioSource unlockSE, chestOpenSE;
    private MagicalNotes note;
    public OVRInput.Controller rightController;

    void Start()
    {
        note = GameObject.FindGameObjectWithTag("note").GetComponent<MagicalNotes>();
        rightController = OVRInput.Controller.RTouch;
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
                StartCoroutine(unlock());
            }
            opened = true;
        }

    }

    IEnumerator unlock()
    {
        unlockSE.Play();
        yield return new WaitForSeconds(1);
        topChest.GetComponent<Animator>().Play("openChest"); //unlock event
        chestOpenSE.Play();
        HapticManager.singleton.TriggerVibration(40, 2, 255, rightController);
        yield return new WaitForSeconds(2);
        changeNoteMatToCandle();
    }

    private void changeNoteMatToCandle()
    {
        note.changeMat(2);
    }
}
