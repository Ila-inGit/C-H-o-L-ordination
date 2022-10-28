using UnityEngine;
using System.Collections;

public class InstantiateMarkerPrefab : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeInstantiateCoroutine());
    }

    IEnumerator WaitBeforeInstantiateCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(10);
        gameObject.GetComponent<Microsoft.MixedReality.Toolkit.Experimental.SceneUnderstanding.SceneUnderstandingController>()
            .InstantiateMarkerOnFloor();
        // TODO(ilaria): forse da trovare un modo per stoppare il mapping
    }

}
