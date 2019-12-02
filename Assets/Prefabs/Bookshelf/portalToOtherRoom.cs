using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class portalToOtherRoom : MonoBehaviour
{
    private bool teleported;
    public string ringSceneName;

    // Start is called before the first frame update
    void Start()
    {
        teleported = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (!teleported && other.gameObject.tag == "Player")
		{
            StartCoroutine(endTeleport());
		}
	}

    private IEnumerator endTeleport()
    {
        teleported = true;
        //can call ghost to say things here
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(ringSceneName, LoadSceneMode.Single); ///////will load ring scene, END of game
    }
}
