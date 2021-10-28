using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    private int x;
    private int y;
    private int number;

    public int X
    {
        get
        {
            return x;
        }
        set
        {
            if ((value >= 0) && (value < CellDriver.maxDimension))
            {
                x = value;
            }
        }
    }
    public int Y
    {
        get
        {
            return y;
        }
        set
        {
            if ((value >= 0) && (value < CellDriver.maxDimension))
            {
                y = value;
            }
        }
    }

    public int Number
    {
        get
        {
            return number;
        }
        set
        {
            if (number >= 0)
            {
                number = value;
            }
        }
    }
    
    public Cell(int x, int y, int n)
    {
        X = x;
        Y = y;
        Number = n;
    }
}
