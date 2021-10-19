using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PanelBehaviourScript : MonoBehaviour
{
    public Text[] textArray;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    private int[,] panels = new int[4, 4];

    bool isGameOver;

    private AudioController gameAudio;

    struct pointPanel
    {
        public int x;
        public int y;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioController>();
        RestartGame();
    }

    public void RestartGame()
    {
        ClearPanels();
        PlaceTwoFour();
        PlaceTwoFour();
        isGameOver = false;
        ShowGameOver(isGameOver);
        WritePanels();
        gameAudio.PlayMusic();
    }

    private void ShiftXRight(int y)
    {
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

    private void ShiftXLeft(int y)
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
    }

    private void ShiftYDown(int x)
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
    }

    private void ShiftYUp(int x)
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
    }

    public void ShiftRight()
    {
        for (int y = 0; y < 4; y++)
        {
            ShiftXRight(y);
            for (int x = 3; x > 0; x--)
            {
                if (panels[x - 1, y] == panels[x, y])
                {
                    panels[x, y] = panels[x, y] + panels[x - 1, y];
                    panels[x - 1, y] = 0;
                }
            }
            ShiftXRight(y);
        }
        EndStep();
    }

    public void ShiftDown()
    {
        for (int x = 0; x < 4; x++)
        {
            ShiftYDown(x);
            for (int y = 3; y > 0; y--)
            {
                if (panels[x, y - 1] == panels[x, y])
                {
                    panels[x, y] = panels[x, y] + panels[x, y - 1];
                    panels[x, y - 1] = 0;
                }
            }
            ShiftYDown(x);
        }
        EndStep();
    }

    public void ShiftLeft()
    {
        for (int y = 0; y < 4; y++)
        {
            ShiftXLeft(y);
            for (int x = 0; x < 3; x++)
            {
                if (panels[x + 1, y] == panels[x, y])
                {
                    panels[x, y] = panels[x, y] + panels[x + 1, y];
                    panels[x + 1, y] = 0;
                }
            }
            ShiftXLeft(y);
        }
        EndStep();
    }

    public void ShiftUp()
    {
        for (int x = 0; x < 4; x++)
        {
            ShiftYUp(x);
            for (int y = 0; y < 3; y++)
            {
                if (panels[x, y + 1] == panels[x, y])
                {
                    panels[x, y] = panels[x, y] + panels[x, y + 1];
                    panels[x, y + 1] = 0;
                }
            }
            ShiftYUp(x);
        }
        EndStep();
    }

    void EndStep()
    {
        RemoveYellow();
        PlaceTwoFour();
        WritePanels();
    }

    void RemoveYellow()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                textArray[x + y * 4].color = Color.white;
    }

    void PlaceTwoFour()
    {
        List<pointPanel> emptyPanels = GetEmptyPanels();
        if (emptyPanels.Count > 0)
        {
            pointPanel emptyPoint = emptyPanels[UnityEngine.Random.Range(0, emptyPanels.Count)];
            SetTwoFour(emptyPoint.x, emptyPoint.y);
        } else
        {
            isGameOver = true;
            ShowGameOver(isGameOver);
            gameAudio.StopMusic();
        }
    }

    private void ShowGameOver(bool gameOver)
    {
        gameOverText.gameObject.SetActive(gameOver);
        restartButton.gameObject.SetActive(gameOver);
    }

    void WritePanels()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                SetPanel(x, y, panels[x, y]);
    }

    void ClearPanels()
    {
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
                panels[x, y] = 0;
    }

    List<pointPanel> GetEmptyPanels()
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

    void SetTwoFour(int x, int y)
    {
        if (UnityEngine.Random.Range(0, 100) > 10)
        {
            panels[x, y] = 2;
            textArray[x + y * 4].color = Color.yellow;
        }
        else
        {
            panels[x, y] = 4;
            textArray[x + y * 4].color = Color.yellow;
        }
    }

    void SetPanel(int x, int y, int value)
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
    
    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShiftDown();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ShiftUp();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ShiftLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ShiftRight();
            }
        }
    }
}
