using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slotCollider : MonoBehaviour
{
    public bookshelf bShelf;
    public bool checkCollide;

    private int shelfIndex, bookIndex;
    public GameObject collidingBook;

    void Start()
    {
        bShelf = FindObjectOfType<bookshelf>();
    }

    void Update()
    {
        
    }

    public void setCheckCollide(bool c)
    {
        checkCollide = c;
    }

    public void assignIndices(int shelf_index, int book_index)
    {
        shelfIndex = shelf_index;
        bookIndex = book_index;
    }

    private void OnTriggerEnter(Collider other)
    {

        //if (checkCollide)
        //{
            if (other.CompareTag("book"))
            {
Debug.Log("slotCollider attached to: "+this.gameObject.name);
                //if (!other.transform.GetChild(0).GetComponent<bookBehav>().inShelf)
                //{
                other.transform.GetChild(0).GetComponent<bookBehav>().inShelf = true;
                bShelf.activateSlotHighlight(true, shelfIndex, bookIndex);
                    collidingBook = other.gameObject;
               // }
            }
      //  }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("book"))
        { //book taken away from slot
            if(collidingBook == other.gameObject)
            other.transform.GetChild(0).GetComponent<bookBehav>().inShelf = false;
            //  checkCollide = true;
            collidingBook = null;
            bShelf.activateSlotHighlight(false, shelfIndex, bookIndex);
            switch (shelfIndex)
            {
                case 0:
                    bShelf.topShelf[bookIndex] = ' ';
                    break;
                case 1:
                    bShelf.midShelf[bookIndex] = ' ';
                    break;
                case 2:
                    bShelf.botShelf[bookIndex] = ' ';
                    break;
            }
        }
    }
}
