using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPainter : MonoBehaviour
{
    public GameObject panelPrefab;
    public List<GameObject> panelList = new List<GameObject>();

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
            PanelBehaviourScript panelScript = GameObject.FindGameObjectWithTag("GameController").GetComponent("PanelBehaviourScript") as PanelBehaviourScript;
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    GameObject panel = Instantiate(panelPrefab, new Vector3(x * 110 - 114, -y * 110 + 114, 0), Quaternion.identity);
                    panel.transform.SetParent(GameObject.FindGameObjectWithTag("PanelKeeper").transform, false);
                    panelList.Add(panel);
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

    void ShiftPanel(int x, int y, int dx, int dy, int num)
    {
        PanelView destinationPanel = panelList[(y + dy) * 4 + (x + dx)].GetComponent<PanelView>();
        panelList[y * 4 + x].GetComponent<PanelView>().ShiftPanel(dx, dy, destinationPanel, num);
    }

    
}
