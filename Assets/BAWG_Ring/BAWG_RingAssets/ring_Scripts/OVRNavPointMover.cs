using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//DXR Ring Navigation System
//written for Fall 2019
//author: Nick Feffer
public class OVRNavPointMover : MonoBehaviour
{
    public Node startNode;
    private Node currNode;
    private Node possibleBackNode;
    private Node possibleLeftNode;
    private Node possibleRightNode;
    private Node selectedNextNode;
    public float moveSpeed;
    public float rotSpeed;
    Vector3 startPos;
    public List<Node> closestNodes;
    public Camera headset;
    private Vector3 yOffset;
    // Start is called before the first frame update
    void Start()
    {
        yOffset = new Vector3(0, 1, 0);
        this.transform.position = startNode.transform.position + yOffset;
        CheckNode();
    }

    // Update is called once per frame
    void Update()
    {
        CheckNode();
        Vector2 inputRight = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        float inputLeft = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick)[0];

        //Vector2 inputRight = new Vector2(0, 0); float inputLeft = 0;
        //if (Input.GetKeyDown(KeyCode.W))
        //    inputRight[1] = 10;
        //if (Input.GetKeyDown(KeyCode.S))
        //    inputRight[1] = -10;
        //if (Input.GetKeyDown(KeyCode.A))
        //    inputLeft = -15f;
        //if (Input.GetKeyDown(KeyCode.D))
            //inputLeft = 15f;

        float xDir = inputRight[0];
        float yDir = inputRight[1];
        

        if (yDir < 0)
        {
            selectedNextNode = possibleBackNode;
        }
        else
        {
            if (xDir < 0)
                selectedNextNode = possibleLeftNode;
            else
                selectedNextNode = possibleRightNode;
        }

        if (selectedNextNode == null)
            selectedNextNode = currNode;

        float step = inputRight.magnitude * moveSpeed / 50f;
        float rotStep = inputLeft * rotSpeed / 3f;
        if (selectedNextNode != null)
            this.transform.position = Vector3.MoveTowards(this.transform.position, selectedNextNode.transform.position + yOffset, step);
        //this.transform.rotation = new Quaternion(0, headset.transform.rotation.y, 0, this.transform.rotation.w);
        this.transform.Rotate(0, rotStep, 0);

    }

    void CheckNode()
    {
        startPos = this.transform.position;
        float minDist = Mathf.Infinity;
        foreach (Node n in closestNodes)
        {
            float currDist = Vector3.Distance(n.transform.position + yOffset, this.transform.position);
            if (currDist < minDist)
            {
                currNode = n;
                minDist = currDist;
            }
            
        }


        foreach (Node n in closestNodes)
        {
            Vector3 relativePoint = transform.InverseTransformPoint(n.transform.position + yOffset);
            if (n != currNode)
            {
                if (relativePoint.z < 0.0)
                    possibleBackNode = n;
                else if (relativePoint.x < 0.0)
                    possibleLeftNode = n;
                else if (relativePoint.x > 0.0)
                    possibleRightNode = n;
          
            }
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Node n = other.GetComponent<Node>();
        if (n != null)
        {
            closestNodes.Add(other.GetComponent<Node>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Node n = other.GetComponent<Node>();
        if (n != null)
        {
            closestNodes.Remove(other.GetComponent<Node>());
            if (possibleRightNode == n)
                possibleRightNode = null;
            if (possibleLeftNode == n)
                possibleLeftNode = null;
            if (possibleBackNode == n)
                possibleBackNode = null;
        }
    }
}
