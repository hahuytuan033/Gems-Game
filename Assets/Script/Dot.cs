using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [Header("Board Variables")]
    public int col;
    public int row;
    public int previousCol;
    public int previousRow;
    public bool isMatched = false;
    public int targetX;
    public int targetY;

    private Board board;
    private GameObject otherDot;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;
    public float swipeResist = 1f;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        col = targetX;
        previousCol = col;
        previousRow = row;
    }

    // Update is called once per frame
    void Update()
    {
        // find matches
        FindMatches();

        // matched
        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
        }

        targetX = col;
        targetY = row;
        // move to horizontal
        if (Mathf.Abs(targetX - transform.position.x) > 1)
        {
            // move towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if(board.allDots[col, row]= this.gameObject)
            {
                board.allDots[col, row]= this.gameObject;
            }
        }
        else
        {
            //directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            // board.allDots[col, row] = this.gameObject;
        }

        // move to vertical
        if (Mathf.Abs(targetY - transform.position.y) > 1)
        {
            // move towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if(board.allDots[col, row]= this.gameObject)
            {
                board.allDots[col, row]= this.gameObject;
            }
        }
        else
        {
            //directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            // board.allDots[col, row] = this.gameObject;
        }
    }

    public IEnumerator CheckMove()
    {
        yield return new WaitForSeconds(.5f);
        if (otherDot != null)
        {
            if (!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().col = col;
                row = previousRow;
                col = previousCol;
            }
            else
            {
                board.DestroyMatches();
            }
            otherDot = null;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(firstTouchPosition);
    }

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist
        || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            // Debug.Log(swipeAngle);
            MovePieces();
        }
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && col < board.width - 1)
        {
            // right swipe
            otherDot = board.allDots[col + 1, row];
            otherDot.GetComponent<Dot>().col -= 1;
            col += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            // up swipe
            otherDot = board.allDots[col, row + 1];
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && col > 0)
        {
            // left swipe
            otherDot = board.allDots[col - 1, row];
            otherDot.GetComponent<Dot>().col += 1;
            col -= 1;
        }
        else if (swipeAngle > -45 && swipeAngle >= -135 && row > 0)
        {
            // down swipe
            otherDot = board.allDots[col, row - 1];
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMove());
    }

    void FindMatches()
    {
        // column
        if (col > 0 && col < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[col - 1, row];
            GameObject rightDot1 = board.allDots[col + 1, row];

            if (leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }

        // row
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allDots[col, row + 1];
            GameObject downDot1 = board.allDots[col, row - 1];
            if (upDot1 != null && downDot1 != null)
            {
                if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }
}
