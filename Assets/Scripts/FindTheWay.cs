using DG.Tweening;
using Sirenix.Reflection.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindTheWay : Singleton<FindTheWay>
{
    private bool[,] commonPoints;
    private int rowIndex, colIndex;
    private int boardWidth, boardHeight;
    private int maxLength;
 
    public static int[,] pathLength;
    public static bool alreadySelectedArea;

    private void Start()
    {
        boardWidth = 5;
        boardHeight = 7;
        alreadySelectedArea = false;
        maxLength = 0;
    }
    private bool CheckPoint(int row, int col)
    {
        if(row >= 0 && row <= 6 && col >= 0 && col <= 4)
            return true;
        return false;
    }
    public void FindInit(int row, int col)
    {
        commonPoints = new bool[boardHeight, boardWidth];
        pathLength = new int[boardHeight, boardWidth];
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                commonPoints[i, j] = false;
                pathLength[i, j] = -1;
            }
        }
        rowIndex = row;
        colIndex = col;
        pathLength[rowIndex, colIndex] = 0;
        MarkTheTile(rowIndex, colIndex, -1);
    }

    private void MarkTheTile(int row, int col, int length)
    {
        if (CheckPoint(row, col) && BoardGenerate.eggsType[row, col] == BoardGenerate.eggsType[rowIndex, colIndex] && commonPoints[row, col] == false)
        {
            Messenger.Broadcast(EventKey.SelectCommonTile, row, col);
            commonPoints[row, col] = true;
            pathLength[row, col] = length + 1;
            maxLength = Mathf.Max(maxLength, pathLength[row, col]);
            MarkTheTile(row + 1, col, pathLength[row, col]);
            MarkTheTile(row - 1, col, pathLength[row, col]);
            MarkTheTile(row, col + 1, pathLength[row, col]);
            MarkTheTile(row, col - 1, pathLength[row, col]);
        }
    }

    public void MergeEggs()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        for (int k = maxLength; k > 0; k--)
        {
            for (int i = 0; i < boardHeight; i++)
            {
                for (int j = 0; j < boardWidth; j++)
                {
                    if (pathLength[i, j] == k)
                    {
                        Debug.Log(i + " " + j + " " + k);
                        if (CheckPoint(i + 1, j) && pathLength[i + 1, j] == k - 1)
                        {
                            BoardGenerate.allEggs[i, j].GetComponent<EggController>().
                            MoveY(BoardGenerate.allEggs[i + 1, j].transform.position.y);

                        }
                        if (CheckPoint(i - 1, j) && pathLength[i - 1, j] == k - 1)
                        {
                            BoardGenerate.allEggs[i, j].GetComponent<EggController>().
                            MoveY(BoardGenerate.allEggs[i - 1, j].transform.position.y);
                        }
                        if (CheckPoint(i, j + 1) && pathLength[i, j + 1] == k - 1)
                        {
                            BoardGenerate.allEggs[i, j].GetComponent<EggController>().
                            MoveX(BoardGenerate.allEggs[i, j + 1].transform.position.x);
                        }
                        if (CheckPoint(i, j - 1) && pathLength[i, j - 1] == k - 1)
                        {
                            BoardGenerate.allEggs[i, j].GetComponent<EggController>().
                            MoveX(BoardGenerate.allEggs[i, j - 1].transform.position.x);
                        }
                        pathLength[i, j] = -1;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        BoardGenerate.allEggs[rowIndex, colIndex].
        GetComponent<EggController>().ChangeEggType(BoardGenerate.eggsType[rowIndex, colIndex] + 1);
        yield return new WaitForSeconds(0.2f);
        SetAllTileOrigin();
        alreadySelectedArea = false;
    }

    public void SetAllTileOrigin()
    {
        for (int i = 0; i < boardHeight; i++)
        {
            for (int j = 0; j < boardWidth; j++)
            {
                Messenger.Broadcast(EventKey.SetAllTileOrigin, i, j);
            }
        }
    }
}
