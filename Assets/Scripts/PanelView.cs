using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelView : MonoBehaviour
{
    private int distanceX = 0;
    private int distanceY = 0;
    float startX;
    float startY;
    Vector3 startPoint;
    float speed = 10;
    PanelView panelViewPutShift;
    int numPutShift;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    public void ShiftPanel(int dx, int dy, PanelView desinationPanelView, int num)
    {
        startX = transform.position.x;
        startY = transform.position.y;
        startPoint = transform.position;
        distanceX = dx * 25;
        distanceY = -dy * 25;
        panelViewPutShift = desinationPanelView;
        numPutShift = num;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Mathf.Abs(startX - transform.position.x) < Mathf.Abs(distanceX)) || (Mathf.Abs(startY - transform.position.y) < Mathf.Abs(distanceY)))
        {
            transform.Translate(distanceX * Time.deltaTime * speed, distanceY * Time.deltaTime * speed, 0);
        }
        else
        {
            if ((Mathf.Abs(distanceX) > 0) || (Mathf.Abs(distanceY) > 0))
            {
                transform.position = startPoint;
                distanceX = 0;
                distanceY = 0;
                Text panelText = transform.Find("Text").GetComponent<Text>();
                if (panelText != null)
                {
                    panelText.text = "";
                }
                if (panelViewPutShift != null)
                {
                    Text panelPutShiftText = panelViewPutShift.transform.Find("Text").GetComponent<Text>();
                    if (panelPutShiftText != null)
                    {
                        panelPutShiftText.text = numPutShift.ToString();
                    }
                }
                CellDriver cellDriver = FindObjectOfType<CellDriver>();
                if (cellDriver != null)
                {
                    cellDriver.TryShift(cellDriver.dxForShift, cellDriver.dyForShift);
                }
            }
        }
    }
}
