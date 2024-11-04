using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    public bool tieneMina = false;
    public int minasAlrededor = 0;
    public bool revelado = false;
    public bool tieneBandera = false;

    private SpriteRenderer spriteRenderer;
    private MinesweeperGrid grid;
    private int row, col;

    public TextMeshProUGUI bombsText; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        // Click Derecho
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Clic derecho detectado en " + gameObject.name);
            Bandera();
            return; // Ignorar clic izquierdo
        }
        
        // Clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clic izquierdo detectado en " + gameObject.name);
            Mostrar();
        }
    }

    public void SetPosition(int r, int c, MinesweeperGrid g)
    {
        row = r;
        col = c;
        grid = g;
        gameObject.name = $"Cell {row},{col}";
    }

    public void Mostrar()
    {
        if (revelado || tieneBandera) return;

        revelado = true;

        if (tieneMina)
        {
            spriteRenderer.color = Color.red;
            Debug.Log("Has muerto!");
        }
        else
        {
            spriteRenderer.color = Color.gray;

            if (minasAlrededor > 0)
            {
                // Mostrar la cantidad de minas en el texto Bombs
                bombsText.text = minasAlrededor.ToString();
                bombsText.color = Color.black;
            }
            else
            {
                // Si no hay minas alrededor, revela las celdas adyacentes
                grid.RevealAdjacentCells(row, col);
            }
        }
    }

    public void Bandera()
    {
        if (revelado) return;

        tieneBandera = !tieneBandera; // Alterna el estado de la bandera

        if (tieneBandera)
        {
            spriteRenderer.color = Color.green;
            Debug.Log("Bandera puesta");
        }
        else
        {
            spriteRenderer.color = Color.white;
            Debug.Log("Bandera quitada");
        }
    }
}