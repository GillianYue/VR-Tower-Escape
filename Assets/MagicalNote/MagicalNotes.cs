using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalNotes : MonoBehaviour
{
    public Material mat1, mat2, mat3, mat4;
    private Renderer rend;
    public AudioSource changeMatSound;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = mat1;
        //StartCoroutine(changeMatPeriodic());
    }

    public void changeMat(int index){
        changeMatSound.Play();
        switch (index){
            case 1:
            rend.material = mat1;
            break;
            case 2:
            rend.material = mat2;
            break;
            case 3:
            rend.material = mat3;
            break;
            case 4:
                rend.material = mat4;
                break;
        }
    }

    IEnumerator changeMatPeriodic()
    {
        yield return new WaitForSeconds(3);
        changeMat(2);
        
        yield return new WaitForSeconds(3);
        changeMat(3);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
