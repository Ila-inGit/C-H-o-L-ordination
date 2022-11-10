using UnityEngine;


public class ChangerSceneAdditive : MonoBehaviour
{
    [SerializeField]
    public SceneNames sceneName;

    public void myChangeScene()
    {
        SceneChangerManager.Instance.changeScene(sceneName);
    }

    public void goToTransitionScene()
    {
        SceneChangerManager.Instance.goToTransitionScene(sceneName);
    }
}
