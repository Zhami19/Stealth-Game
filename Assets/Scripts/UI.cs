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

    public void YouLosePanelActive()
    {
        youLosePanel.SetActive(true);
    }
}
