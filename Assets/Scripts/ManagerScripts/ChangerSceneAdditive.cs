using UnityEngine;


public class ChangerSceneAdditive : MonoBehaviour
{
    [SerializeField]
    public SceneNames sceneName;

    public void myChangeScene()
    {
        SoundManager.Instance.StopLoop();
        SceneChangerManager.Instance.changeScene(sceneName);
    }

    public void goToTransitionScene()
    {
        SoundManager.Instance.StopLoop();
        SceneChangerManager.Instance.goToTransitionScene(sceneName);
    }
}
