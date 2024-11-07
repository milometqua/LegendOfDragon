using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObligateButton : MonoBehaviour
{
    UniTaskCompletionSource closeUcs=new();
    public void OnClick()
    {
        closeUcs.TrySetResult();
    }
    public UniTask WaitForClick()
    {
        return closeUcs.Task;
    }
}
