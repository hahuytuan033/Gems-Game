using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Match3 : MonoBehaviour
{
    public ArrayLayout boardLayout;
    int width= 8;
    int height= 8;
    Node[, ] board;

    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void StartGame()
    {
        board= new Node[width, height];

        string seed= getRandomSeed();
        random= new System.Random(seed.GetHashCode());
    }

    void InitializeBoard()
    {
        for(int y= 0; y< height; y++)
        {
            for(int x= 0; x< width; x++)
            {
                board[x, y]= new Node(-1, new Point(x, y));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string getRandomSeed()
    {
        string seed= "";
        string acceptableChars= "ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqstuvxyz1234567890";
        for(int i= 0; i< 20; i++)
        {
            seed+= acceptableChars[Random.Range(0, acceptableChars.Length)];
        }
        return seed;
    }

}

[System.Serializable]
public class Node
{
    public int value; //0= bluecube, 1= greencube, 2= rhombus , 3= circle 
    public Point index;

    public Node (int v, Point i)
    {
        value= v;
        index= i;
    }
}
