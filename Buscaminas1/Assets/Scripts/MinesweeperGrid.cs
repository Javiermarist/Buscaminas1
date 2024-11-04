using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesweeperGrid : MonoBehaviour
{
    public int rows = 10;
    public int cols = 10;
    public GameObject cellPrefab;
    public Camera mainCamera; // Agrega una referencia a la cámara

    private Cell[,] grid;

    void Start()
    {
        CreateGrid();
        PlaceMines();
        CalculateNumbers();
        CenterCamera(); // Llama al método para centrar la cámara
    }

    void CreateGrid()
    {
        grid = new Cell[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector2 position = new Vector2(col, row);
                GameObject cellObject = Instantiate(cellPrefab, position, Quaternion.identity);
                cellObject.transform.parent = transform;

                Cell cell = cellObject.GetComponent<Cell>();
                cell.SetPosition(row, col, this); // Asigna la referencia del grid a la celda
                grid[row, col] = cell;
            }
        }
    }

    void PlaceMines()
    {
        int mineCount = (rows * cols) / 5;

        for (int i = 0; i < mineCount; i++)
        {
            int row = Random.Range(0, rows);
            int col = Random.Range(0, cols);

            if (!grid[row, col].tieneMina)
            {
                grid[row, col].tieneMina = true;
            }
            else
            {
                i--;
            }
        }
    }

    void CalculateNumbers()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (grid[row, col].tieneMina) continue;

                int mineCount = CountAdjacentMines(row, col);
                grid[row, col].minasAlrededor = mineCount;
            }
        }
    }

    public int CountAdjacentMines(int row, int col)
    {
        int count = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newRow = row + i;
                int newCol = col + j;

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && grid[newRow, newCol].tieneMina)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public void RevealAdjacentCells(int row, int col)
    {
        for (int r = row - 1; r <= row + 1; r++)
        {
            for (int c = col - 1; c <= col + 1; c++)
            {
                // Asegúrate de que no salgas del grid y que no cuentes la celda misma
                if (r >= 0 && r < grid.GetLength(0) && c >= 0 && c < grid.GetLength(1) && (r != row || c != col))
                {
                    grid[r, c].Mostrar(); // Llama al método Reveal de la celda adyacente
                }
            }
        }
    }

    void CenterCamera()
    {
        // Calcula la posición central del mapa
        Vector3 centerPosition = new Vector3(cols / 2f - 0.5f, rows / 2f - 0.5f, -10); // Ajusta el Z según la distancia de la cámara
        mainCamera.transform.position = centerPosition;
    }
}