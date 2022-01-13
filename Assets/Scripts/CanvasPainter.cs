using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPainter : MonoBehaviour
{
    [SerializeField] private GameObject panelPrefab;
    private readonly List<PanelView> panelList = new List<PanelView>();

    private void Start()
    {
        FindObjectOfType<CellDriver>().CellShiftEvent += OnCellShift;
    }

    public void OnCellShift(CellDriver cellDriver, int x, int y, int dx, int dy, int num)
    {
        ShiftPanel(x, y, dx, dy, num);
    }

    public void PaintPanels()
    {
        if (panelPrefab != null)
        {
            PanelBehaviourScript panelScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<PanelBehaviourScript>();
            GameObject panelKeeper = GameObject.FindGameObjectWithTag("PanelKeeper");
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    GameObject panel = Instantiate(panelPrefab, new Vector3(x * 110 - 114, -y * 110 + 114, 0), Quaternion.identity);
                    panel.transform.SetParent(panelKeeper.transform, false);
                    PanelView panelView = panel.GetComponent<PanelView>();
                    panelList.Add(panelView);
                    Text panelText = panel.transform.Find("Text").GetComponent<Text>();
                    if (panelText != null)
                    {
                        panelScript.textArray.Add(panelText);
                    }
                    Text panelText2 = panel.transform.Find("Text2").GetComponent<Text>();
                    if (panelText2 != null)
                    {
                        panelScript.text2Array.Add(panelText2);
                    }
                }
        }
    }

    private void ShiftPanel(int x, int y, int dx, int dy, int num)
    {
        PanelView destinationPanel = panelList[(y + dy) * 4 + (x + dx)];
        panelList[y * 4 + x].ShiftPanel(dx, dy, destinationPanel, num);
    }

    
}
