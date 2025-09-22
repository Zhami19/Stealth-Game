using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellView : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    int col;
    int row;

    public void SetColumnAndRow(int x, int y)
    {
        col = x;
        row = y;
    }

    public void SetText(string s)
    {
        text.text = s;
    }

    public void Click()
    {
        FindObjectOfType<TTT>().ChooseSpace(col, row);
    }
}
