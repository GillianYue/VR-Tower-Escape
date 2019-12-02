using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSection : MonoBehaviour
{
    public Node[] nodes;
    public bool doorSection;
    private bool activeStretch;
    public Material activeMat;
    public Material inactiveMat;

    // Start is called before the first frame update
    void Start()
    {
        nodes = transform.GetComponentsInChildren<Node>();
        if (!doorSection)
            this.ActivateStretch();
    }

    public void ActivateStretch()
    {
        foreach (Node n in nodes)
        {
            n.gameObject.GetComponent<MeshRenderer>().material = activeMat;
        }
        this.activeStretch = true;
    }

    public void DeactivateStretch()
    {
        foreach (Node n in nodes)
        {
            n.gameObject.GetComponent<MeshRenderer>().material = inactiveMat;
        }
        this.activeStretch = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
