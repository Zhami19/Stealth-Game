using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    CellView[,] cells;

    [SerializeField] GameObject rowPrefab;
    [SerializeField] GameObject cellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeBoard(int cols, int rows)
    {
        cells = new CellView[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            GameObject row = Instantiate(rowPrefab, transform);
            for (int j = 0; j < cols; j++)
            {
                GameObject cell = Instantiate(cellPrefab, row.transform);

                cells[j, i] = cell.GetComponent<CellView>();
                cells[j, i].SetColumnAndRow(j, i);
            }
        }
    }

    public void UpdateCellVisual(int c, int r, PlayerOption player)
    {
        string symbol = "";
        if (player == PlayerOption.X)
            symbol = "X";
        else if (player == PlayerOption.O)
            symbol = "O";
        cells[c, r].SetText(symbol);
    }
}
