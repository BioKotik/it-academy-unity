using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchThree_Controller : MonoBehaviour
{
    [SerializeField] private MatchThree_Field m_Field;
    [SerializeField] private MatchThree_Types m_Types;

    public static Camera MainCamera;
    
    /// <summary>
    ///проверяем можем ли мы поместить в данную ячейку такую кофету -  нужно избегать ситуаций с 3 и более в ряд на старте игры
    /// </summary>
    /// <param name="targetCell">Стартовая клетка</param>
    /// <param name="id">Id конфеты</param>
    /// <returns></returns>
    public static bool IsFreeCandyPlacement(MatchThree_Cell targetCell, int id)
    {
        //Direction от 0 до 4 соответсвует enum
        for (int i = 0; i < 4; i++)
        {
            Direction direction = (Direction) i;
            int counter = 0;
            int repeated = 0;

            MatchThree_Cell cell = targetCell;
            
            while (counter < 2)
            {
                cell = cell.GetNeighbour(direction);
                if (!cell || !cell.Candy)
                {
                    break;
                }

                if (cell.Candy.CandyData.Id == id)
                {
                    repeated++;
                }

                counter++;
            }

            if (repeated >= 2)
            {
                return false;
            }
        }

        return true;
    }
    
    private void Start()
    {
        MainCamera = Camera.main;
        
        m_Field.Init();

        var firstCell = MatchThree_Field.GetCell(0, 0);

        MatchThree_Cell cell = firstCell;
        while (cell)
        {
            SetupCandiesLine(cell, Direction.Right);
            cell = cell.GetNeighbour(Direction.Up);
        }
    }

    private void SetupCandiesLine(MatchThree_Cell firstCell, Direction direction)
    {
        MatchThree_Cell cell = firstCell;
        while (cell)
        {
            MatchThree_Candy newCandy = m_Types.GetRandomCandy();
            //пробуем генерить пока не получим разрешенную кoнефетку
            //типов конфеток должно быть 5 или более - иначе возможен вариант вечного цикла
            while (!IsFreeCandyPlacement(cell, newCandy.CandyData.Id))
            {
                Destroy(newCandy.gameObject);
                newCandy = m_Types.GetRandomCandy();
            }

            cell.Candy = newCandy;
            cell.Candy.transform.position = cell.transform.position;
            cell = cell.GetNeighbour(direction);
        }
    }
}