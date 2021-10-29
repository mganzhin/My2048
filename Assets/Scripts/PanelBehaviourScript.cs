using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class PanelBehaviourScript : MonoBehaviour
{
    public List<Text> textArray;
    public List<Text> text2Array;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    bool isGameOver;
    bool isShifting;

    private AudioController gameAudio;

    public CellDriver viewModelCellDriver;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<CellDriver>().CellsReadyEvent += OnCellsReady;
        FindObjectOfType<CellDriver>().GameOverEvent += OnGameOver;
        CanvasPainter canvasScript = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent("CanvasPainter") as CanvasPainter;
        canvasScript.PaintPanels();
        RestartGame();
    }

    public void OnCellsReady(CellDriver cellDriver)
    {
        int cellValue;
        for (int x = 0; x < 4; x++)
            for (int y = 0; y < 4; y++)
            {
                cellValue = cellDriver.GetCell(x, y).Number;
                if (cellValue > 0)
                {
                    text2Array[x + y * 4].text = "2";
                    textArray[x + y * 4].text = cellValue.ToString();
                    textArray[x + y * 4].color = Color.Lerp(Color.white, Color.red, cellValue/25f);
                }
                else
                {
                    text2Array[x + y * 4].text = "";
                    textArray[x + y * 4].text = "";
                }
            }
        isShifting = false;
    }

    public void OnGameOver(CellDriver cellDriver)
    {
        isGameOver = true;
        ShowGameOver(isGameOver);
        gameAudio.StopMusic();
    }

    public void RestartGame()
    {
        viewModelCellDriver.InitCells();
        viewModelCellDriver.PlaceTwoFour();
        viewModelCellDriver.PlaceTwoFour();
        isGameOver = false;
        isShifting = false;
        ShowGameOver(isGameOver);
        gameAudio = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioController>();
        gameAudio.PlayMusic();
    }
        
    private void ShowGameOver(bool gameOver)
    {
        gameOverText.gameObject.SetActive(gameOver);
        restartButton.gameObject.SetActive(gameOver);
    }
    
    // Update is called once per frame
    void Update()
    {
        if ((!isGameOver) && (!isShifting))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //ShiftDown();
                viewModelCellDriver.clearCollapsed();
                isShifting = true;
                viewModelCellDriver.TryShift(0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //ShiftUp();
                viewModelCellDriver.clearCollapsed();
                isShifting = true;
                viewModelCellDriver.TryShift(0, -1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //ShiftLeft();
                viewModelCellDriver.clearCollapsed();
                isShifting = true;
                viewModelCellDriver.TryShift(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //ShiftRight();
                viewModelCellDriver.clearCollapsed();
                isShifting = true;
                viewModelCellDriver.TryShift(1, 0);
            }
        }
    }
}
