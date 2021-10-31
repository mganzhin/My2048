using UnityEngine;
using UnityEngine.UI;

public class PanelView : MonoBehaviour
{
    private int distanceX = 0;
    private int distanceY = 0;
    float startX;
    float startY;
    Vector3 startPoint;
    float speed = 20;
    PanelView panelViewPutShift;
    private int numPutShift;
    private int deltaX;
    private int deltaY;

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
        deltaX = dx;
        deltaY = dy;
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
                Text panelText2 = transform.Find("Text2").GetComponent<Text>();
                if (panelText2 != null)
                {
                    panelText2.text = "";
                }
                if (panelViewPutShift != null)
                {
                    Text panelPutShiftText = panelViewPutShift.transform.Find("Text").GetComponent<Text>();
                    if (panelPutShiftText != null)
                    {
                        panelPutShiftText.text = numPutShift.ToString();
                    }
                    Text panelPutShiftText2 = panelViewPutShift.transform.Find("Text2").GetComponent<Text>();
                    if (panelPutShiftText2 != null)
                    {
                        panelPutShiftText2.text = "2";
                    }
                }
                CellDriver cellDriver = FindObjectOfType<CellDriver>();
                if (cellDriver != null)
                {
                    cellDriver.TryShift(deltaX, deltaY, 1);
                }
            }
        }
    }
}
