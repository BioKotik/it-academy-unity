using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchThree_Field : MonoBehaviour
{
    [SerializeField] private Camera m_MainCamera;
    [SerializeField] private GameObject m_Cell;
    [SerializeField] private float m_CellSize = 0.6f;
    [SerializeField] private int m_FieldWidht = 6;
    [SerializeField] private int m_FieldHeight = 8;

    private static readonly List<List<MatchThree_Cell>> GameField = new List<List<MatchThree_Cell>>();

    public static float CurrentCellSize;
    public void Init()
    {
        GenerateField(m_FieldWidht, m_FieldHeight);
        CurrentCellSize = m_CellSize;
    }

    private void GenerateField(int width, int height)
    {
        for (var x = 0; x < width; x++)
        {
            GameField.Add(new List<MatchThree_Cell>());
            for (var y = 0; y < height; y++)
            {
                var pos = new Vector3(x * m_CellSize, y * m_CellSize, 0f);
                var obj = Instantiate(m_Cell, pos, Quaternion.identity);
                obj.name = $"Cell {x} {y}";
                var cell = obj.AddComponent<MatchThree_Cell>();
                GameField[x].Add(cell);

                //настройка соседей по горизонтали
                //крайняя левая клетка
                if (x > 0)
                {
                    cell.SetNeighbour(Direction.Left, GameField[x - 1][y]);
                    //указываем новую клетку как правую для предыдущей
                    GameField[x - 1][y].SetNeighbour(Direction.Right, cell);
                }
                else
                    cell.SetNeighbour(Direction.Left, null);

                //крайняя правая клетка
                if (x == width - 1)
                    cell.SetNeighbour(Direction.Right, null);
                //настройка соседей по вертикали
                //крайняя нижняя клетка
                if (y > 0)
                {
                    cell.SetNeighbour(Direction.Down, GameField[x][y - 1]);
                    //указываем новую клетку как верхнюю для предыдущей
                    GameField[x][y - 1].SetNeighbour(Direction.Up, cell);
                }
                else
                    cell.SetNeighbour(Direction.Down, null);

                //крайняя верхняя клетка
                if (y == height - 1)
                    cell.SetNeighbour(Direction.Up, null);
            }
        }

        m_MainCamera.transform.position = new Vector3(width * m_CellSize * 0.5f, height * m_CellSize * 0.5f, -1f);
    }
    
    public static MatchThree_Cell GetCell(MatchThree_Candy candy)
    {
        foreach (var row in GameField)
        {
            foreach (var cell in row)
            {
                if (cell.Candy == candy)
                {
                    return cell;
                }
            }
        }
        return null;
    }

    public static MatchThree_Cell GetCell(int x, int y)
    {
        return GameField[x][y];
    }
}