using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObjsAfterPressStart : MonoBehaviour
{
    [SerializeField]
    List<GameObject> objsToHide;

    [SerializeField]
    bool deactivateSelf;
    [SerializeField]
    bool hideOnStart;

    private void Start()
    {
        if (hideOnStart)
        {
            HideObjectsAfterPress();
        }
    }
    public void HideObjectsAfterPress()
    {
        if (objsToHide != null)
        {
            foreach (var obj in objsToHide)
            {
                obj.SetActive(false);
            }
        }
        if (deactivateSelf)
            gameObject.SetActive(false);
    }
}
