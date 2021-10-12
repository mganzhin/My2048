using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PanelBehaviourScript : MonoBehaviour
{
    public Text[] textArray;

    private int[,] panels = new int[4, 4];
    struct pointPanel
    {
        public int x;
        public int y;
    }

    // Start is called before the first frame update
    void Start()
    {
        clearPanels();

        placeTwoFour();
        placeTwoFour();

        writePanels();
    }

    public void shiftRight()
    {
        for (int y = 0; y < 4; y++)
        {
            for (int i = 0; i < 3; i++)
                for (int x = 3; x >0 ; x--)
                {
                    if (panels[x, y] == 0)
                    {
                        panels[x, y] = panels[x - 1, y];
                        panels[x - 1, y] = 0;
                    }
                }
            for (int x = 3; x > 0; x--)
            {
                if (panels[x - 1, y] == panels[x, y])
                {
                    panels[x, y] = panels[x, y] + panels[x - 1, y];
                    panels[x - 1, y] = 0;
                }
            }
            for (int i = 0; i < 3; i++)
                for (int x = 3; x > 0; x--)
                {
                    if (panels[x, y] == 0)
                    {
                        panels[x, y] = panels[x - 1, y];
                        panels[x - 1, y] = 0;
                    }
                }
        }
        placeTwoFour();
        writePanels();
    }

    public void shiftDown()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int i = 0; i < 3; i++)
                for (int y = 3; y > 0; y--)
                {
                    if (panels[x, y] == 0)
                    {
                        panels[x, y] = panels[x, y - 1];
                        panels[x, y - 1] = 0;
                    }
                }
            for (int y = 3; y > 0; y--)
            {
                if (panels[x, y - 1] == panels[x, y])
                {
                    panels[x, y] = panels[x, y] + panels[x, y - 1];
                    panels[x, y - 1] = 0;
                }
            }
            for (int i = 0; i < 3; i++)
                for (int y = 3; y > 0; y--)
                {
                    if (panels[x, y] == 0)
                    {
                        panels[x, y] = panels[x, y - 1];
                        panels[x, y - 1] = 0;
                    }
                }
        }
        placeTwoFour();
        writePanels();
    }

    public void shiftLeft()
    {
        for (int y = 0; y < 4; y++)
        {
            for (int i = 0; i < 3; i++)
                for (int x = 0; x < 3; x++)
                {
                    if (panels[x, y] == 0)
                    {
                        panels[x, y] = panels[x + 1, y];
                        panels[x + 1, y] = 0;
                    }
                }
            for (int x = 0; x < 3; x++)
            {
                if (panels[x + 1, y] == panels[x, y])
                {
                    panels[x, y] = panels[x, y] + panels[x + 1, y];
                    panels[x + 1, y] = 0;
                }
            }
            for (int i = 0; i < 3; i++)
                for (int x = 0; x < 3; x++)
                {
                    if (panels[x, y] == 0)
                    {
                        panels[x, y] = panels[x + 1, y];
                        panels[x + 1, y] = 0;
                    }
                }
        }
        placeTwoFour();
        writePanels();
    }

    public void shiftUp()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int i = 0; i < 3; i++)
                for (int y = 0; y < 3; y++)
                {
                    if (panels[x, y] == 0)
                    {
                        panels[x, y] = panels[x, y + 1];
                        panels[x, y + 1] = 0;
                    }
                }
            for (int y = 0; y < 3; y++)
            {
                if (panels[x, y + 1] == panels[x, y])
                {
                    panels[x, y] = panels[x, y] + panels[x, y + 1];
                    panels[x, y + 1] = 0;
                }
            }
            for (int i = 0; i < 3; i++)
                for (int y = 0; y < 3; y++)
                {
                    if (panels[x, y] == 0)
                    {
                        panels[x, y] = panels[x, y + 1];
                        panels[x, y + 1] = 0;
                    }
                }
        }
        placeTwoFour();
        writePanels();
    }

    void placeTwoFour()
    {
        List<pointPanel> emptyPanels = getEmptyPanels();
        if (emptyPanels.Count > 0)
        {
            pointPanel emptyPoint = emptyPanels[UnityEngine.Random.Range(0, emptyPanels.Count)];
            setTwoFour(emptyPoint.x, emptyPoint.y);
        }
    }

    void writePanels()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                setPanel(x, y, panels[x, y]);
    }

    void clearPanels()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                panels[x, y] = 0;
    }

    List<pointPanel> getEmptyPanels()
    {
        List<pointPanel> lResult = new List<pointPanel>();
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                if (panels[x, y] == 0)
                {
                    pointPanel panelXY;
                    panelXY.x = x;
                    panelXY.y = y;
                    lResult.Add(panelXY);
                }
        return lResult;
    }

    void setTwoFour(int x, int y)
    {
        if (UnityEngine.Random.Range(0, 100) > 10)
        {
            panels[x, y] = 2;
        }
        else
        {
            panels[x, y] = 4;
        }
    }

    void setPanel(int x, int y, int value)
    {
        if ((x >= 0) && (x <= 3) && (y >= 0) && (y <= 3))
        {
            if (value > 0)
            {
                textArray[x + y * 4].text = value.ToString();
            }
            else
            {
                textArray[x + y * 4].text = "";
            }
        }
    }

    int getPanel(int x, int y)
    {
        if ((x >= 0) && (x <= 3) && (y >= 0) && (y <= 3))
        {
            return Convert.ToInt32(textArray[x + y * 4]);
        }
        else
        {
            return -1;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
