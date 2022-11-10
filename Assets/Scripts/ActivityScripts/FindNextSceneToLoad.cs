using UnityEngine;

namespace ActivityScripts
{
    public class FindNextSceneToLoad : MonoBehaviour
    {

        public void findNextSceneToLoad()
        {
            SceneChangerManager.Instance.findNextSceneToLoad();
        }

    }
}