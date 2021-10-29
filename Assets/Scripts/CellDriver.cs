using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDriver : MonoBehaviour
{
    public static readonly int maxDimension = 4;
    private Cell[,] cells = new Cell[maxDimension, maxDimension];
    private bool[,] collapsedCells = new bool[maxDimension, maxDimension];

    public delegate void cellsReady(CellDriver cellDriver);
    public event cellsReady CellsReadyEvent;

    public delegate void cellShift(CellDriver cellDriver, int x, int y, int dx, int dy, int num);
    public event cellShift CellShiftEvent;

    public delegate void gameOver(CellDriver cellDriver);
    public event gameOver GameOverEvent;

    /*public int xForShift;
    public int yForShift;
    public int dxForShift;
    public int dyForShift;
    public int numForShift;*/

    private void Start()
    {
        
    }

    public void clearCollapsed()
    {
        for (int x = 0; x < maxDimension; x++)
            for (int y = 0; y < maxDimension; y++)
            {
                collapsedCells[x, y] = false;
            }
    }

    public Cell GetCell(int x, int y)
    {
        if ((x >= 0) && (x < maxDimension) && (y >= 0) && (y < maxDimension))
        {
            return cells[x, y];
        }
        else
        {
            return null;
        }
    }

    public void InitCells()
    {
        for (int x = 0; x < maxDimension; x++)
            for (int y = 0; y < maxDimension; y++)
            {
                cells[x, y] = new Cell(x, y, 0);
            }
        CellsReadyEvent?.Invoke(this);
    }

    public void ClearCells()
    {
        for (int x = 0; x < maxDimension; x++)
            for (int y = 0; y < maxDimension; y++)
            {
                if (cells[x, y] != null)
                {
                    cells[x, y].Number = 0;
                }
            }
        CellsReadyEvent?.Invoke(this);
    }

    private void StartShift(int x, int y, int dx, int dy, int num)
    {
        /*xForShift = x;
        yForShift = y;
        dxForShift = dx;
        dyForShift = dy;
        numForShift = num;*/
        CellShiftEvent?.Invoke(this, x, y, dx, dy, num);
    }

    public void TryShift(int dx, int dy)
    {
        bool foundShiftable = false;
        if ((dx == -1) && (dy == 0))
        {
            int iy = 0;
            while ((iy < maxDimension) && (!foundShiftable))
            {
                int ix = 1;
                while ((ix < maxDimension) && (!foundShiftable))
                {
                    if (cells[ix, iy].Number > 0)
                    {
                        if (cells[ix - 1, iy].Number == 0)
                        {
                            cells[ix - 1, iy].Number = cells[ix, iy].Number;
                            cells[ix, iy].Number = 0;
                            foundShiftable = true;
                            //Call event to shift
                            StartShift(ix, iy, dx, dy, cells[ix - 1, iy].Number);
                        }
                        else
                        {
                            if ((cells[ix - 1, iy].Number == cells[ix, iy].Number) && (!collapsedCells[ix, iy]))
                            {
                                cells[ix - 1, iy].Number++;
                                cells[ix, iy].Number = 0;
                                collapsedCells[ix - 1, iy] = true;
                                collapsedCells[ix, iy] = true;
                                foundShiftable = true;
                                //Call event to shift
                                StartShift(ix, iy, dx, dy, cells[ix - 1, iy].Number);
                            }
                        }
                    }
                    ix++;
                }
                iy++;
            }

        }
        else if ((dx == 1) && (dy == 0))
        {
            int iy = 0;
            while ((iy < maxDimension) && (!foundShiftable))
            {
                int ix = maxDimension - 2;
                while ((ix >= 0) && (!foundShiftable))
                {
                    if (cells[ix, iy].Number > 0)
                    {
                        if (cells[ix + 1, iy].Number == 0)
                        {
                            cells[ix + 1, iy].Number = cells[ix, iy].Number;
                            cells[ix, iy].Number = 0;
                            foundShiftable = true;
                            //Call event to shift
                            StartShift(ix, iy, dx, dy, cells[ix + 1, iy].Number);
                        }
                        else
                        {
                            if ((cells[ix + 1, iy].Number == cells[ix, iy].Number) && (!collapsedCells[ix, iy]))
                            {
                                cells[ix + 1, iy].Number++;
                                cells[ix, iy].Number = 0;
                                collapsedCells[ix + 1, iy] = true;
                                collapsedCells[ix, iy] = true;
                                foundShiftable = true;
                                //Call event to shift
                                StartShift(ix, iy, dx, dy, cells[ix + 1, iy].Number);
                            }
                        }
                    }
                    ix--;
                }
                iy++;
            }
        }
        else if ((dx == 0) && (dy == -1))
        {
            int ix = 0;
            while ((ix < maxDimension) && (!foundShiftable))
            {
                int iy = 1;
                while ((iy < maxDimension) && (!foundShiftable))
                {
                    if (cells[ix, iy].Number > 0)
                    {
                        if (cells[ix, iy - 1].Number == 0)
                        {
                            cells[ix, iy - 1].Number = cells[ix, iy].Number;
                            cells[ix, iy].Number = 0;
                            foundShiftable = true;
                            //Call event to shift
                            StartShift(ix, iy, dx, dy, cells[ix, iy - 1].Number);
                        }
                        else
                        {
                            if ((cells[ix, iy - 1].Number == cells[ix, iy].Number) && (!collapsedCells[ix, iy]))
                            {
                                cells[ix, iy - 1].Number++;
                                cells[ix, iy].Number = 0;
                                collapsedCells[ix, iy - 1] = true;
                                collapsedCells[ix, iy] = true;
                                foundShiftable = true;
                                //Call event to shift
                                StartShift(ix, iy, dx, dy, cells[ix, iy - 1].Number);
                            }
                        }
                    }
                    iy++;
                }
                ix++;
            }
        }
        else if ((dx == 0) && (dy == 1))
        {
            int ix = 0;
            while ((ix < maxDimension) && (!foundShiftable))
            {
                int iy = maxDimension - 2;
                while ((iy >= 0) && (!foundShiftable))
                {
                    if (cells[ix, iy].Number > 0)
                    {
                        if (cells[ix, iy + 1].Number == 0)
                        {
                            cells[ix, iy + 1].Number = cells[ix, iy].Number;
                            cells[ix, iy].Number = 0;
                            foundShiftable = true;
                            //Call event to shift
                            StartShift(ix, iy, dx, dy, cells[ix, iy + 1].Number);
                        }
                        else
                        {
                            if ((cells[ix, iy + 1].Number == cells[ix, iy].Number) && (!collapsedCells[ix, iy]))
                            {
                                cells[ix, iy + 1].Number++;
                                cells[ix, iy].Number = 0;
                                collapsedCells[ix, iy + 1] = true;
                                collapsedCells[ix, iy] = true;
                                foundShiftable = true;
                                //Call event to shift
                                StartShift(ix, iy, dx, dy, cells[ix, iy + 1].Number);
                            }
                        }
                    }
                    iy--;
                }
                ix++;
            }
        }
        //Nothing to shift
        if (!foundShiftable)
        {
            PlaceTwoFour();
        }
    }

    public List<Cell> GetEmptyCells()
    {
        List<Cell> cellList = new List<Cell>();
        for (int x = 0; x < maxDimension; x++)
            for (int y = 0; y < maxDimension; y++)
                if (cells[x, y].Number == 0)
                {
                    cellList.Add(cells[x,y]);
                }
        return cellList;
    }

    public void PlaceTwoFour()
    {
        List<Cell> emptyCells = GetEmptyCells();
        if (emptyCells.Count > 0)
        {
            Cell emptyPoint = emptyCells[Random.Range(0, emptyCells.Count)];
            SetTwoFour(emptyPoint.X, emptyPoint.Y);
        }
        else
        {
            //Event for game over
            GameOverEvent?.Invoke(this);
        }
    }

    void SetTwoFour(int x, int y)
    {
        if (Random.Range(0, 100) > 10)
        {
            cells[x, y].Number = 1;
        }
        else
        {
            cells[x, y].Number = 2;
        }
        //Event placed new number
        CellsReadyEvent?.Invoke(this);
    }
}
