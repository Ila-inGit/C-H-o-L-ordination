using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjsAfterPressStart : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objsToHide;

    [SerializeField]
    bool deactivateSelf;

    public void HideObjectsAfterPress()
    {
        foreach (var obj in objsToHide)
        {
            obj.SetActive(false);
        }
        if (deactivateSelf)
            gameObject.SetActive(false);
    }
}
