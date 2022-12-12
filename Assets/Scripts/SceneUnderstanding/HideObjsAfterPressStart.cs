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
    [HideInInspector]
    private bool done = false;

    private void Start()
    {
        if (hideOnStart)
        {
            HideObjectsAfterPress();
        }
    }
    public void HideObjectsAfterPress()
    {
        if (deactivateSelf) StartCoroutine(DeactivateCorutine());
        if (objsToHide != null)
        {
            foreach (var obj in objsToHide)
            {
                obj.SetActive(false);
            }
        }
        done = true;
    }

    IEnumerator DeactivateCorutine()
    {

        while (!done)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
