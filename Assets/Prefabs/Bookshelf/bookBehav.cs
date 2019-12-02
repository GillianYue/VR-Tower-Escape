using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookBehav : MonoBehaviour
{
    public string id;
    public bool inShelf;

    public IEnumerator bookMove()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(0, 0, 3);

        for (float i = 0; i <= 1; i += 0.01f)
        {
            //transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
