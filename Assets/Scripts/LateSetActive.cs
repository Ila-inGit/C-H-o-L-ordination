using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateSetActive : MonoBehaviour
{
    [SerializeField]
    GameObject objectToActive;
    [SerializeField]
    float numberOfSeconds;

    private void Start()
    {
        StartCoroutine(setObjectActive());
    }

    public IEnumerator setObjectActive()
    {
        objectToActive.SetActive(false);
        yield return new WaitForSeconds(numberOfSeconds);
        objectToActive.SetActive(true);
    }
}
