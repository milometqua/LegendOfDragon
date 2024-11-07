using UnityEngine;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    private int rowIndex;
    private int colIndex;
    private bool clicked;
    private bool firstClicked;
    //private Color color;

    private void Start()
    {
        clicked = false;
        firstClicked = false;
    }

    private void OnEnable()
    {
        Messenger.AddListener<int, int>(EventKey.SelectCommonTile, SelectTile);
        Messenger.AddListener<int, int>(EventKey.SetAllTileOrigin, SetTileOrigin);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<int, int>(EventKey.SelectCommonTile, SelectTile);
        Messenger.RemoveListener<int, int>(EventKey.SetAllTileOrigin, SetTileOrigin);
    }
    public void SetPositionInArray(int row, int col)
    {
        rowIndex = row;
        colIndex = col;
    }

    public void OnClick()
    {
        if (!clicked)
        {
            Debug.Log(rowIndex + " " + colIndex);
            FindTheWay.Instance.SetAllTileOrigin();
            if (FindTheWay.alreadySelectedArea == false)
            {
                firstClicked = true;
                FindTheWay.Instance.FindInit(rowIndex, colIndex);
                FindTheWay.alreadySelectedArea = true;
            }
            else
            {
                FindTheWay.alreadySelectedArea = false;
                firstClicked = false;
            }
        }
        else
        {
            if (firstClicked)
            {
                FindTheWay.Instance.MergeEggs();
            }
            else
            {
                FindTheWay.Instance.FindInit(rowIndex, colIndex);
                FindTheWay.Instance.MergeEggs();
            }
        }
    }

    private void SelectTile(int row, int col)
    {
        if(rowIndex == row && colIndex == col && clicked == false)
        {
            transform.position += new Vector3(0f, 0.1f);
            GetComponent<Image>().color = new Color(0f, 225f, 0f);
            clicked = true;
        }
    }

    private void SetTileOrigin(int row, int col)
    {
        if (clicked)
        {
            transform.position -= new Vector3(0f, 0.1f);
            GetComponent<Image>().color = new Color(255f, 255f, 255f);
            clicked = false;
        }
    }
}
