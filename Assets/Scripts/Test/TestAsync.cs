using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
public class TestAsync : MonoBehaviour
{
    public GameObject buttonRef;
    [Button]
    public async void GenerateButton()
    {
        var obj = Instantiate(buttonRef, transform);
        ObligateButton obligateBtn = obj.GetComponent<ObligateButton>();

        await obligateBtn.WaitForClick();

        Debug.Log("Button Clicked");
    }
}

