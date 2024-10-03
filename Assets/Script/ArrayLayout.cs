using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayLayout 
{
    [System.Serializable]
    public struct rowData
    {
        public bool[] row;
    }

    public Grid grid;
    public rowData[] rows= new rowData[16];// 8x8
}
