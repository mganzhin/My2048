using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelBehaviourScript : MonoBehaviour
{
    public List<Text> textArray;
    public List<Text> text2Array;

    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    bool isGameOver;
    bool isShifting;

    private Touch theTouch;
    private Vector2 touchStartPosition, touchEndPosition;

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
    
    private void Shift(int dx, int dy)
    {
        viewModelCellDriver.clearCollapsed();
        isShifting = true;
        viewModelCellDriver.TryShift(dx, dy);
    }

    // Update is called once per frame
    void Update()
    {
        if ((!isGameOver) && (!isShifting))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Shift(0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Shift(0, -1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Shift(-1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Shift(1, 0);
            } else if (Input.touchCount > 0)
            {
                theTouch = Input.GetTouch(0);

                if (theTouch.phase == TouchPhase.Began)
                {
                    touchStartPosition = theTouch.position;
                }

                else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
                {
                    touchEndPosition = theTouch.position;

                    float x = touchEndPosition.x - touchStartPosition.x;
                    float y = touchEndPosition.y - touchStartPosition.y;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x > 10f)
                        {
                            Shift(1, 0);
                        }
                        else if (x < -10f)
                        {
                            Shift(-1, 0);
                        }
                    }
                    else
                    {
                        if (y > 10f)
                        {
                            Shift(0, -1);
                        } else if (y < -10f)
                        {
                            Shift(0, 1);
                        }
                    }
                }
            }
        }
    }
}
