using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardGenerate : MonoBehaviour
{
    [SerializeField] private GameObject greenTilePrefab;
    [SerializeField] private GameObject yellowTilePrefab;
    [SerializeField] private GameObject[] eggsCollection;
    [SerializeField] private Transform boardView;
    public static int[,] eggsType;
    public static GameObject[,] allEggs;
    private int width, height;
    private int eggLimit;
    [Button]
    public void CountTile()
    {
        Debug.Log(eggsCollection.Length);
    }    
    private void Start()
    {
        width = 5;
        height = 7;
        eggsType = new int[height, width];
        allEggs = new GameObject[height, width];
        eggLimit = 4;
        GenerateTile();
    }

    private void GenerateTile()
    {
        int k = 0;
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                int eggRandom = Random.Range(0, eggLimit);
                GameObject tile;
                if (k == 0)
                    tile = Instantiate(greenTilePrefab, boardView);
                else
                    tile = Instantiate(yellowTilePrefab, boardView);

                tile.GetComponent<TileController>().SetPositionInArray(i, j);
                GameObject egg = Instantiate(eggsCollection[eggRandom], tile.transform);
                egg.name = i.ToString() + " " + j.ToString();
                egg.GetComponent<EggController>().SetPositionInArray(i, j);
                allEggs[i, j] = egg;
                eggsType[i, j] = eggRandom;
                k = 1 - k;
            }
        }
    }
}
