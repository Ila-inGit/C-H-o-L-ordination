using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkActivation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartUp.Instance.SetMarkActive();
    }


}
