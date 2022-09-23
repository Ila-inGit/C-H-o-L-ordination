using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SetCamera : MonoBehaviour
{
    private bool firstPressed, secondPressed, thirdPressed, forthPressed = false;
    private bool setted = false;
    public GameObject firstButton, secondButton, thirdButton, forthButton;
    public Material material2;


    public void pressButton(GameObject currentButton)
    {

        if (currentButton.name == firstButton.name && !firstPressed)
        {

            firstPressed = true;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform currChild = currentButton.transform.GetChild(i);
                if (currChild.name == "CompressableButtonVisuals")
                {
                    currChild.transform.GetChild(0).GetComponent<MeshRenderer>().material = material2;
                }
            }
        }
        else if (currentButton.name == secondButton.name && firstPressed && !secondPressed)
        {

            secondPressed = true;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform currChild = currentButton.transform.GetChild(i);
                if (currChild.name == "CompressableButtonVisuals")
                {
                    currChild.transform.GetChild(0).GetComponent<MeshRenderer>().material = material2;
                }
            }
            //set mesh
        }
        else if (currentButton.name == thirdButton.name && firstPressed && secondPressed && !thirdPressed)
        {

            thirdPressed = true;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform currChild = currentButton.transform.GetChild(i);
                if (currChild.name == "CompressableButtonVisuals")
                {
                    currChild.transform.GetChild(0).GetComponent<MeshRenderer>().material = material2;
                }
            }
        }
        else if (currentButton.name == forthButton.name && firstPressed && secondPressed && thirdPressed && !forthPressed)
        {
            // Debug.Log(currentButton.name);
            forthPressed = true;
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform currChild = currentButton.transform.GetChild(i);
                if (currChild.name == "CompressableButtonVisuals")
                {
                    currChild.transform.GetChild(0).GetComponent<MeshRenderer>().material = material2;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (firstPressed && secondPressed && thirdPressed && forthPressed && !setted)
        {
            // StartUp.Instance.setPositionCamera(Camera.main.transform);
            DataCollector.Instance.addToFileCamera(Camera.main.transform.position);
            StartUp.Instance.SetMarkActive();
            setted = true;
            StartCoroutine(transitionScene());
        }
    }
    private IEnumerator transitionScene()
    {
        yield return new WaitForSeconds(1.5f);
        // transtion to start scene
        int index = SceneManager.GetSceneByName(Constants.SETTING_SCENE).buildIndex;
        SceneManager.UnloadSceneAsync(index);
        SceneManager.LoadScene(index + 1, LoadSceneMode.Additive);

    }


}
