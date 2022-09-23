using UnityEngine;
using UnityEngine.SceneManagement;

namespace ActivityScripts
{
    public class FindNextSceneToLoad: MonoBehaviour
    { 
        private int nextSceneBuildIndex;

        public void findNextSceneToLoad()
        {
            nextSceneBuildIndex = PlayerPrefs.GetInt("nextSceneBuildIndex");
            SceneManager.UnloadSceneAsync("TransitionScene");
            SceneManager.LoadScene(nextSceneBuildIndex, LoadSceneMode.Additive);
        }
    
    }
}