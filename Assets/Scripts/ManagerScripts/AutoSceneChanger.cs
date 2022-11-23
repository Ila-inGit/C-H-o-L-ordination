using UnityEngine;
public class AutoSceneChanger : MonoBehaviour
{
    [SerializeField]
    public SceneNames sceneName;
    private float timerBeforeChange = 0.0f;
    private int totalTouches = 0;
    private bool changed = false;
    private TouchesCounter touchesCounter;

    private void FixedUpdate()
    {
        touchesCounter = gameObject.GetComponent<TouchesCounter>();
        totalTouches = touchesCounter.totalTouches;

        timerBeforeChange += Time.deltaTime;
        if (!changed)
        {
            if (timerBeforeChange >= ParseQRInfoManager.Instance.setUpInfo.maxTimeForActivity)
            {
                // Debug.Log("4 min expired");
                changed = true;
                NeedToChange();
            }
            else if (totalTouches == ParseQRInfoManager.Instance.setUpInfo.numberRightAttempts)
            {
                // Debug.Log("15 total touches done");
                changed = true;
                NeedToChange();
            }
            else if (totalTouches == ParseQRInfoManager.Instance.setUpInfo.numberTotalAttempts)
            {
                // Debug.Log("Max total touches done");
                changed = true;
                NeedToChange();
            }
        }
    }


    private void NeedToChange()
    {
        SoundManager.Instance.StopLoop();
        SceneChangerManager.Instance.goToTransitionScene(sceneName);
    }

}