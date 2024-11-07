using DG.Tweening;
using UnityEngine;

public class EggController : MonoBehaviour
{
    private int rowIndex;
    private int colIndex;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject[] eggsCollection;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetPositionInArray(int row, int col)
    {
        rowIndex = row;
        colIndex = col;
    }
    
    public void MoveX(float x)
    {
        transform.DOMoveX(x, 0.1f).SetEase(Ease.Linear).OnComplete(Fade);
    }

    public void MoveY(float y)
    {
        transform.DOMoveY(y, 0.1f).SetEase(Ease.Linear).OnComplete(Fade);
    }

    private void Fade()
    {
        spriteRenderer.DOFade(0f, 0.1f);
        spriteRenderer.enabled = false;
    }

    public void ChangeEggType(int type)
    {
        spriteRenderer.sprite = eggsCollection[type].GetComponent<SpriteRenderer>().sprite;
        BoardGenerate.eggsType[rowIndex, colIndex] = type;
    }

}
