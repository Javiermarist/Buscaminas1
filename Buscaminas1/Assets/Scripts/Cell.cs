using UnityEngine;
using TMPro;

public class Cell : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MinesweeperGrid grid;
    public TextMeshProUGUI bombsText;

    /*public Sprite spriteBomb;
    public Sprite spriteExplosion;
    public Sprite spriteFlag;
    public Sprite pieceSprite;
    public Sprite[] spriteNumbers;*/

    private int row, col;
    public int bombsAround = 0;

    public bool hasBomb = false;
    public bool revealed = false;
    public bool hasFlag = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseOver()
    {
        // Click derecho
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Clic derecho detectado en " + gameObject.name);
            Flag();
        }
        
        // Clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            if (!hasFlag && !revealed)
            {
                Debug.Log("Clic izquierdo detectado en " + gameObject.name);
                Reveal();
            }
        }
    }

    public void SetPosition(int r, int c, MinesweeperGrid g)
    {
        row = r;
        col = c;
        grid = g;
        gameObject.name = $"Cell {row},{col}";
    }

    public void Flag()
    {
        if (revealed) return;

        if (!revealed)
        {
            hasFlag = !hasFlag;
        }

        if (hasFlag)
        {
            //GetComponent<SpriteRenderer>().sprite = flagSprite;
            spriteRenderer.color = Color.green;
            Debug.Log("Bandera puesta");
        }
        else
        {
            //GetComponent<SpriteRenderer>().sprite = normalPieceSprite;
            spriteRenderer.color = Color.grey;
            Debug.Log("Bandera quitada");
        }
    }

    public void Reveal()
    {
        if (revealed || hasFlag) return;

        revealed = true;

        if (hasBomb)
        {
            spriteRenderer.color = Color.red;
            Debug.Log("Has muerto!");
        }
        else
        {
            if (bombsAround > 0)
            {
                bombsText.text = bombsAround.ToString();
                bombsText.color = Color.black;
                bombsText.gameObject.SetActive(true);
                spriteRenderer.color = Color.white;
            }
            else
            {
                // Si no hay minas alrededor, revela las celdas adyacentes
                spriteRenderer.color = Color.white;
                grid.RevealAdjacentCells(row, col);
            }
        }
    }
}
