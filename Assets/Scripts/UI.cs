using UnityEngine;
using Unity.UI;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject youWinPanel;
    [SerializeField] GameObject youLosePanel;
    public void YouWinPanelActive()
    {
        youWinPanel.SetActive(true);
    }

    public void YouWinPanelInactive()
    {
        youWinPanel.SetActive(false);
    }

    public void YouLosePanelActive()
    {
        youLosePanel.SetActive(true);
    }

    public void YouLosePanellInactive()
    {
        youLosePanel.SetActive(false);
    }
}
