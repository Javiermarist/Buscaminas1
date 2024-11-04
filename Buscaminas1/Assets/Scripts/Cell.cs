using UnityEngine;
using TMPro; // Asegúrate de incluir este espacio de nombres

public class Cell : MonoBehaviour
{
    public bool isMine = false;
    public int mineCount = 0;
    public bool isRevealed = false;
    public bool isFlagged = false;

    private SpriteRenderer spriteRenderer;
    private MinesweeperGrid grid;
    private int row, col;

    // Cambia el tipo a TextMeshProUGUI
    private TextMeshProUGUI bombsText; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Obtener el componente TextMeshPro del objeto Bombs
        Transform bombsTransform = transform.Find("Canvas/Bombs"); // Cambia la ruta para buscar en el Canvas
        if (bombsTransform != null)
        {
            bombsText = bombsTransform.GetComponent<TextMeshProUGUI>();
            bombsText.text = ""; // Ocultar texto al inicio
        }
        else
        {
            Debug.LogError("No se encontró el objeto hijo 'Bombs'. Asegúrate de que existe y tiene un componente TextMeshPro.");
        }
    }

    void OnMouseDown()
    {
        // Verifica si el clic derecho se detecta
        if (Input.GetMouseButtonDown(1)) // Clic derecho
        {
            Debug.Log("Clic derecho detectado en " + gameObject.name);
            ToggleFlag();
            return; // Ignorar clic izquierdo
        }

        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            Debug.Log("Clic izquierdo detectado en " + gameObject.name);
            Reveal();
        }
    }

    public void SetPosition(int r, int c, MinesweeperGrid g)
    {
        row = r;
        col = c;
        grid = g;
        gameObject.name = $"Cell {row},{col}";
    }

    public void Reveal()
    {
        if (isRevealed || isFlagged) return;

        isRevealed = true;

        if (isMine)
        {
            spriteRenderer.color = Color.red;
            Debug.Log("Game Over!");
        }
        else
        {
            spriteRenderer.color = Color.gray;

            if (mineCount > 0)
            {
                // Mostrar la cantidad de minas en el texto Bombs
                bombsText.text = mineCount.ToString();
                bombsText.color = Color.black; // Cambia el color del texto para que sea visible
            }
            else
            {
                // Si no hay minas alrededor, revela las celdas adyacentes
                grid.RevealAdjacentCells(row, col);
            }
        }
    }

    public void ToggleFlag()
    {
        if (isRevealed) return;

        isFlagged = !isFlagged;

        // Cambiar el color para simular una bandera
        if (isFlagged)
        {
            Debug.Log("Bandera puesta");
            spriteRenderer.color = Color.green; // Cambia el color a verde
        }
        else
        {
            Debug.Log("Bandera quitada");
            spriteRenderer.color = Color.white; // Restablecer a blanco si se quita la bandera
        }
    }
}