using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreTheyAllLit : MonoBehaviour {
    public List<GameObject> candleList;

    private bool allLit;

    // Start is called before the first frame update
    void Start(){
        allLit = false;
    }

    // Update is called once per frame
    void Update(){
        if (!allLit) {
            int litCount = 0;
            
            foreach (GameObject candle in candleList) {
                if (candle.transform.GetChild(0).GetChild(0).gameObject.activeSelf) {
                    litCount += 1;
                }
            }

            if (litCount == candleList.Count) {
                allLit = true;
            }
        }
    }
}
