using System.Collections.Generic;
using UnityEngine;

public class CanvasPainter : MonoBehaviour
{
    [SerializeField] private PanelView panelViewPrefab;
    [SerializeField] private CellDriver cellDriver;
    [SerializeField] private PanelBehaviourScript panelScript;
    [SerializeField] private GameObject panelKeeper;
    private readonly List<PanelView> panelList = new();

    private void Start()
    {
        cellDriver.CellShiftEvent += OnCellShift;
    }

    public void OnCellShift(CellDriver cellDriver, int x, int y, int dx, int dy, int num)
    {
        ShiftPanel(x, y, dx, dy, num);
    }

    private void OnTryShift(int dx, int dy, int iteration)
    {
        if (cellDriver != null)
        {
            cellDriver.TryShift(dx, dy, iteration);
        }
    }

    public void PaintPanels()
    {
        if (panelViewPrefab != null)
        {
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    PanelView panelView = Instantiate(panelViewPrefab, 
                        new Vector3(x * 25 - 37, -y * 25 + 25, 0), 
                        Quaternion.identity, 
                        panelKeeper.transform);
                    panelView.TryShiftEvent += OnTryShift;
                    panelList.Add(panelView);
                    if (panelView.PanelText != null)
                    {
                        panelScript.textArray.Add(panelView.PanelText);
                    }
                    if (panelView.PanelText2 != null)
                    {
                        panelScript.text2Array.Add(panelView.PanelText2);
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
