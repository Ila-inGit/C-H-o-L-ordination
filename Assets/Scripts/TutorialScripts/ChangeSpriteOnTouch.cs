using UnityEngine;

public class ChangeSpriteOnTouch : MonoBehaviour
{

    public Material material1;

    public Material material2;

    private bool changed = false;

    private MeshRenderer meshRenderer;

    private void Start()
    {

        meshRenderer = GetComponent<MeshRenderer>(); // we are accessing the meshRenderer that is attached to the Gameobject
        if (meshRenderer != null && meshRenderer.material != null)
            meshRenderer.material = material1;
    }

    public void ChangePlanetMesh()
    {
        TouchesCounter touchesCounter = FindObjectOfType<TouchesCounter>();
        MakePlanetInteractableTutorial mpit = FindObjectOfType<MakePlanetInteractableTutorial>();
        if ((touchesCounter == null && mpit == null) || (touchesCounter != null && touchesCounter.isInsideBox) || (mpit != null && mpit.gameObject.tag == "TutorialBox"))
        {
            if (!changed)
            {
                meshRenderer.material = material2;
                changed = true;
            }
            else
            {
                meshRenderer.material = material1;
                changed = false;
            }
        }

    }

    public void ResetMesh()
    {
        if (changed)
        {
            meshRenderer.material = material1;
            changed = false;
        }

    }
}
