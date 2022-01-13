using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelBehaviourScript : MonoBehaviour
{
    public List<Text> textArray;
    public List<Text> text2Array;

    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;

    private bool isGameOver;
    private bool isShifting;

    private Touch theTouch;
    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;

    private readonly float swipeLength = 200;

    private AudioController gameAudio;

    [SerializeField] private CellDriver viewModelCellDriver;

    // Start is called before the first frame update
    void Start()
    {
        viewModelCellDriver.CellsReadyEvent += OnCellsReady;
        viewModelCellDriver.GameOverEvent += OnGameOver;
        viewModelCellDriver.NothingToShift += OnNothingToShift;
        CanvasPainter canvasScript = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<CanvasPainter>();
        canvasScript.PaintPanels();
        RestartGame();
    }

    public void OnNothingToShift(CellDriver cellDriver)
    {
        isShifting = false;
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
        viewModelCellDriver.ClearCollapsed();
        isShifting = true;
        viewModelCellDriver.TryShift(dx, dy, 0);
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
                        if (x > swipeLength)
                        {
                            Shift(1, 0);
                        }
                        else if (x < -swipeLength)
                        {
                            Shift(-1, 0);
                        }
                    }
                    else
                    {
                        if (y > swipeLength)
                        {
                            Shift(0, -1);
                        } else if (y < -swipeLength)
                        {
                            Shift(0, 1);
                        }
                    }
                }
            }
        }
    }
}
