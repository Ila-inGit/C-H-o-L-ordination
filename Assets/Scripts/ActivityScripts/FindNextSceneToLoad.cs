using UnityEngine;
using UnityEngine.SceneManagement;

namespace ActivityScripts
{
    public class FindNextSceneToLoad : MonoBehaviour
    {
        private int nextSceneIntEnum;
        private Constants nameOfScenes = new Constants();

        public void findNextSceneToLoad()
        {
            nameOfScenes.init();
            nextSceneIntEnum = PlayerPrefs.GetInt("nextSceneEnum");
            SceneNames nextSceneNameEnum = (SceneNames)nextSceneIntEnum;
            string nextSceneName = nameOfScenes.getCurrentName(nextSceneNameEnum);
            SceneManager.UnloadSceneAsync(Constants.TRANSITION_SCENE);
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
        }

    }
}