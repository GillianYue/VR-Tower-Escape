using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPointsManager : MonoBehaviour
{
    public NodeSection[] allSections;
    List<NodeSection> path;
    // Start is called before the first frame update
    void Start()
    {
        allSections = FindObjectsOfType<NodeSection>();
        path = new List<NodeSection>();
        foreach (NodeSection section in allSections)
        {
            
            section.ActivateStretch();

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
