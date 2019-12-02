using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookshelf : MonoBehaviour
{
    public char[] topShelf, midShelf, botShelf; //updates when book is filled or taken from a slot
    Vector3[] topShelfPos, midShelfPos, botShelfPos; //where the books should be instantiated
    public Vector3 topShelfStartPivot, midShelfStartPivot, botShelfStartPivot; //used to calc pos[] when slot width is given
    private float slotWidth; 
    public int topShelfNumBooks, midShelfNumBooks, botShelfNumBooks;
    private MeshRenderer[] topShelfSlots, midShelfSlots, botShelfSlots;
    public string topShelfPsw, midShelfPsw, botShelfPsw;

    public GameObject slotPrefab, bookPrefab_green, bookPrefab_red, bookPrefab_blue;
    private MeshRenderer slotHighlight; private int slotShelfIndex, slotBookIndex; 
    private bool canSnap;

    public AudioSource bookSnapSE, bookshelfSlideSE;
	public GameObject portal;

    void Start()
    {
        slotWidth = slotPrefab.transform.localScale.x * slotPrefab.transform.GetChild(0).localScale.x + 0.01f;

        topShelf = new char[topShelfNumBooks]; midShelf = new char[midShelfNumBooks];
        botShelf = new char[botShelfNumBooks]; 
        topShelfPos = new Vector3[topShelfNumBooks]; midShelfPos = new Vector3[midShelfNumBooks];
        botShelfPos = new Vector3[botShelfNumBooks];


        topShelfSlots = new MeshRenderer[topShelfNumBooks]; 
        midShelfSlots = new MeshRenderer[midShelfNumBooks]; 
        botShelfSlots = new MeshRenderer[botShelfNumBooks];

        for (int t=0; t< topShelfNumBooks; t++)
        {
            topShelfPos[t] = topShelfStartPivot + new Vector3(-slotWidth * t, 0, 0);
        }

        for (int m = 0; m < midShelfNumBooks; m++)
        {
            midShelfPos[m] = midShelfStartPivot + new Vector3(-slotWidth * m, 0, 0);
        }

        for (int b = 0; b < botShelfNumBooks; b++)
        {
            botShelfPos[b] = botShelfStartPivot + new Vector3(-slotWidth * b, 0, 0);
        }

        genSlotHighlights();

		portal.SetActive(false);

        //char[] topParams = new char[3], midParams = new char[3], botParams = new char[4];
        //topParams[0] = 'r'; topParams[1] = 'b'; topParams[2] = 'g'; midParams[0] = 'g';
        //midParams[1] = 'r'; midParams[2] = 'b'; botParams[0] = 'b'; botParams[1] = 'g';
        //botParams[2] = 'r'; botParams[3] = 'b';
        //genBooks(topParams, midParams, botParams);


    }

    public void checkSnapBook()
    {
        if (canSnap)
        {
            slotCollider sc = slotHighlight.GetComponent<slotCollider>();
            bookBehav book = sc.collidingBook.transform.GetChild(0).GetComponent<bookBehav>();
            sc.collidingBook.transform.rotation = Quaternion.Euler(0, 180, 0);

            switch (slotShelfIndex)
            {
                case 0:
                    sc.collidingBook.transform.position = topShelfPos[slotBookIndex];
                    topShelf[slotBookIndex] = book.id[0];
                    book.inShelf = true;
                    break;
                case 1:
                    sc.collidingBook.transform.position = midShelfPos[slotBookIndex];
                    midShelf[slotBookIndex] = book.id[0];
                    book.inShelf = true;
                    break;
                case 2:
                    sc.collidingBook.transform.position = botShelfPos[slotBookIndex];
                    botShelf[slotBookIndex] = book.id[0];
                    book.inShelf = true;
                    break;
            }

            bookSnapSE.Play();

            sc.setCheckCollide(false);
            slotHighlight.enabled = false;
            checkForBookshelfUnlock();
        }
    }

    public void checkForBookshelfUnlock()
    {

        string top = new string(topShelf), mid = new string(midShelf), bot = new string(botShelf);
        Debug.Log("comparing password... " + top + " " + mid + " " + bot);
        if(top.Equals(topShelfPsw) && mid.Equals(midShelfPsw) && bot.Equals(botShelfPsw))
        {
            Debug.Log("password fit, moving bookshelf");
            foreach(bookBehav bb in FindObjectsOfType<bookBehav>())
            {
                StartCoroutine(bb.bookMove());
            }
            StartCoroutine(bookshelfMove());
            bookshelfSlideSE.Play();
        }

    }

    IEnumerator bookshelfMove()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(-0.61f, 0, 0);

		portal.SetActive(true);

        for(float i=0; i<=1; i += 0.01f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, i);
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void activateSlotHighlight(bool status, int shelfIndex, int bookIndex)
    {
        //if (shelfIndex != slotShelfIndex || bookIndex != slotBookIndex)
        //{
            if (slotHighlight != null)
            {
                slotHighlight.enabled = false;
            }

            switch (shelfIndex)
            {
                case 0:
                    slotHighlight = topShelfSlots[bookIndex];
                    break;
                case 1:
                    slotHighlight = midShelfSlots[bookIndex];
                    break;
                case 2:
                    slotHighlight = botShelfSlots[bookIndex];
                    break;
                default:
                    break;
            }

            slotHighlight.enabled = status;
            canSnap = status;
            slotShelfIndex = shelfIndex;
            slotBookIndex = bookIndex;
        //}
    }

    void genSlotHighlights()
    {
        for (int t = 0; t < topShelfNumBooks; t++)
        {
            GameObject hl = Instantiate(slotPrefab);
            hl.transform.position = topShelfPos[t];
            hl.transform.rotation = Quaternion.Euler(0, 180, 0);
            hl.transform.parent = transform;
            topShelfSlots[t] = hl.transform.GetChild(0).GetComponent<MeshRenderer>();
            topShelfSlots[t].enabled = false;
            slotCollider sc = hl.transform.GetChild(0).GetComponent<slotCollider>();
            sc.assignIndices(0, t);
            sc.setCheckCollide(true);
    }

        for (int m = 0; m < midShelfNumBooks; m++)
        {
            GameObject hl = Instantiate(slotPrefab);
            hl.transform.position = midShelfPos[m];
            hl.transform.rotation = Quaternion.Euler(0, 180, 0);
            hl.transform.parent = transform;
            midShelfSlots[m] = hl.transform.GetChild(0).GetComponent<MeshRenderer>();
            midShelfSlots[m].enabled = false;
            slotCollider sc = hl.transform.GetChild(0).GetComponent<slotCollider>();
            sc.assignIndices(1, m);
            sc.setCheckCollide(true);
        }

        for (int b = 0; b < botShelfNumBooks; b++)
        {
            GameObject hl = Instantiate(slotPrefab);
            hl.transform.position = botShelfPos[b];
            hl.transform.rotation = Quaternion.Euler(0, 180, 0);
            hl.transform.parent = transform;
            botShelfSlots[b] = hl.transform.GetChild(0).GetComponent<MeshRenderer>();
            botShelfSlots[b].enabled = false;
            slotCollider sc = hl.transform.GetChild(0).GetComponent<slotCollider>();
            sc.assignIndices(2, b);
            sc.setCheckCollide(true);
        }
    }

    void genBooks(char[] top, char[] mid, char[] bot)
    {
        for (int t = 0; t < topShelfNumBooks; t++)
        {
            GameObject book = null;
            switch (top[t])
            {
                case 'r':
                    book = Instantiate(bookPrefab_red);
                    break;
                case 'g':
                    book = Instantiate(bookPrefab_green);
                    break;
                case 'b':
                    book = Instantiate(bookPrefab_blue);
                    break;
                default:
                    break;
            }
            if (book != null)
            {
                topShelfSlots[t].gameObject.GetComponent<slotCollider>().setCheckCollide(false);
                book.transform.position = topShelfPos[t];
                topShelf[t] = book.transform.GetChild(0).GetComponent<bookBehav>().id[0];
            }
        }

        for (int m = 0; m < midShelfNumBooks; m++)
        {
            GameObject book = null;
            switch (mid[m])
            {
                case 'r':
                    book = Instantiate(bookPrefab_red);
                    break;
                case 'g':
                    book = Instantiate(bookPrefab_green);
                    break;
                case 'b':
                    book = Instantiate(bookPrefab_blue);
                    break;
                default:
                    break;
            }
            if (book != null)
            {
                midShelfSlots[m].gameObject.GetComponent<slotCollider>().setCheckCollide(false);
                book.transform.position = midShelfPos[m];
                midShelf[m] = book.transform.GetChild(0).GetComponent<bookBehav>().id[0];
            }
        }

        for (int b = 0; b < botShelfNumBooks; b++)
        {
            GameObject book = null;
            switch (bot[b])
            {
                case 'r':
                    book = Instantiate(bookPrefab_red);
                    break;
                case 'g':
                    book = Instantiate(bookPrefab_green);
                    break;
                case 'b':
                    book = Instantiate(bookPrefab_blue);
                    break;
                default:
                    break;
            }
            if (book != null)
            {
                botShelfSlots[b].gameObject.GetComponent<slotCollider>().setCheckCollide(false);
                book.transform.position = botShelfPos[b];
                botShelf[b] = book.transform.GetChild(0).GetComponent<bookBehav>().id[0];
            }
        }


    }


    void Update()
    {
        
    }
}
