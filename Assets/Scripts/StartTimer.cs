using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    private float timeBeforeClick = 0.0f;

    private bool clicked = false;

    private bool start = false;

    private string currentTag;


    private void FixedUpdate()
    {

        if (start)
        {
            if (!clicked)
            {
                timeBeforeClick += Time.deltaTime;
            }
        }
    }

    public void StartTimerForClick(string currentTag)
    {
        start = true;
        this.currentTag = currentTag;
    }

    public void ResetTimer()
    {
        if (!clicked && currentTag != Constants.TUTORIAL_BOX)
        {
            // Debug.Log("time taken to pass through box: " + timeBeforeClick);
            DataCollector.Instance.addToFile(
                new MyData(Constants.RESET_TIMER, ParseQRInfoManager.Instance.setUpInfo.patientID,
                "-1", gameObject.scene.name, currentTag, "-1", "_00", -1, timeBeforeClick, "-1", -1,
                SceneChangerManager.Instance.isMusicSynch(), SceneChangerManager.Instance.isRhythmSynch(), SceneChangerManager.Instance.isRhythmNotSynch(), SceneChangerManager.Instance.isMusicNotSynch()));
        }

        timeBeforeClick = 0;
        clicked = false;
        start = false;
        currentTag = "";
    }

    public void StopTimer()
    {
        // Debug.Log("time taken before click: " + timeBeforeClick);
        DataCollector.Instance.addToFile(
            new MyData(Constants.TIME_BEFORE_CLICK, ParseQRInfoManager.Instance.setUpInfo.patientID,
            "-1", gameObject.scene.name, currentTag, "-1", "11", timeBeforeClick, -1, "-1", -1,
            SceneChangerManager.Instance.isMusicSynch(), SceneChangerManager.Instance.isRhythmSynch(), SceneChangerManager.Instance.isRhythmNotSynch(), SceneChangerManager.Instance.isMusicNotSynch()));
        clicked = true;
        start = false;
    }

}
