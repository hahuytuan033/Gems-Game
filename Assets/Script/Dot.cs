using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public int col;
    public int row;
    public int targetX;
    public int targetY;
    public GameObject otherDot;
    private Board board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    public Vector2 tempPosition;
    public float swipeAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        col = targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = col;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //move toward the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            //Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allDots[col, row] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(firstTouchPosition);    
    }

    private void OnMouseUp()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        //Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && col < board.width)
        {
            //right swipe
            otherDot = board.allDots[col + 1, row];
            otherDot.GetComponent<Dot>().col -= 1;
            col += 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height)
        {
            //up swipe
            otherDot = board.allDots[col, row + 1];
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && col > 0)
        {
            //left swipe
            otherDot = board.allDots[col - 1, row];
            otherDot.GetComponent<Dot>().col += 1;
            col -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //down swipe
            otherDot = board.allDots[col, row - 1];
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
    }
}
