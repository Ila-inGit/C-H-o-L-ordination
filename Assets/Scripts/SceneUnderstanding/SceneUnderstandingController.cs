using System.Collections.Generic;
using UnityEngine;
#if ENABLE_WINMD_SUPPORT
using Microsoft.MixedReality.SceneUnderstanding;
using Windows.Perception.Spatial;
using Windows.Perception.Spatial.Preview;
using UnityEngine.XR.WSA;
#endif

public class SceneUnderstandingController : MonoBehaviour
{
    public GameObject parentObject;
    public GameObject markerPrefab;
    public Material quadMaterial;

#if ENABLE_WINMD_SUPPORT
    Scene lastScene;
#endif

    List<GameObject> markers;
    List<GameObject> quads;
    bool initialised;
    static readonly float searchRadius = 5.0f;

    public SceneUnderstandingController()
    {
        this.markers = new List<GameObject>();
        this.quads = new List<GameObject>();
        this.initialised = false;
    }


    void Update()
    {
#if ENABLE_WINMD_SUPPORT
        if (this.lastScene != null)
        {
            var node = this.lastScene.OriginSpatialGraphNodeId;
 
            var sceneCoordSystem = SpatialGraphInteropPreview.CreateCoordinateSystemForNode(node);
 
            var unityCoordSystem =
                (SpatialCoordinateSystem)System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(
                    WorldManager.GetNativeISpatialCoordinateSystemPtr());
 
            var transform = sceneCoordSystem.TryGetTransformTo(unityCoordSystem);
 
            if (transform.HasValue)
            {
                var sceneToWorldUnity = transform.Value.ToUnity();
 
                this.parentObject.transform.SetPositionAndRotation(
                    sceneToWorldUnity.GetColumn(3), sceneToWorldUnity.rotation);
            }
        }
#endif
    }


    // These 4 methods are wired to call by the user (start button in theory)
    public async void OnWalls()
    {
#if ENABLE_WINMD_SUPPORT
        await this.ComputeAsync(SceneObjectKind.Wall);
#endif
    }
    public async void OnFloor()
    {
#if ENABLE_WINMD_SUPPORT
        await this.ComputeAsync(SceneObjectKind.Floor);
#endif
    }
    public async void OnCeiling()
    {
#if ENABLE_WINMD_SUPPORT
        await this.ComputeAsync(SceneObjectKind.Ceiling);
#endif
    }
    public async void OnPlatform()
    {
#if ENABLE_WINMD_SUPPORT
        await this.ComputeAsync(SceneObjectKind.Platform);
#endif
    }


    void ClearChildren()
    {
        foreach (var child in this.markers)
        {
            Destroy(child);
        }
        foreach (var child in this.quads)
        {
            Destroy(child);
        }
        this.markers.Clear();
        this.quads.Clear();
    }


#if ENABLE_WINMD_SUPPORT
    // called by ComputeAsync
    async Task InitialiseAsync()
    {
        if (!this.initialised)
        {
            if (SceneObserver.IsSupported())
            {
                var access = await SceneObserver.RequestAccessAsync();
 
                if (access == SceneObserverAccessStatus.Allowed)
                {
                    this.initialised = true;
                }
            }
        }
    }

    // called by one of the 4 methods before to retrive the scenObject
    async Task ComputeAsync(SceneObjectKind sceneObjectKind)
    {
        this.ClearChildren();
 
        await this.InitialiseAsync();
 
        if (this.initialised)
        {
            var querySettings = new SceneQuerySettings()
            {
                EnableWorldMesh = false,
                EnableSceneObjectQuads = true,
                EnableSceneObjectMeshes = false,
                EnableOnlyObservedSceneObjects = false
            };
            this.lastScene = await SceneObserver.ComputeAsync(querySettings, searchRadius);
 
            if (this.lastScene != null)
            {
                foreach (var sceneObject in this.lastScene.SceneObjects)
                {
                    if (sceneObject.Kind == sceneObjectKind)
                    {
                        var marker = GameObject.Instantiate(this.markerPrefab);
 
                        marker.transform.SetParent(this.parentObject.transform);
 
                        marker.transform.localPosition = sceneObject.Position.ToUnity();
                        marker.transform.localRotation = sceneObject.Orientation.ToUnity();
 
                        this.markers.Add(marker);

                        // // Get the quad
                        // var quads = sceneObject.Quads;
                        // if (quads.Count > 0)
                        // {
                        //     // Find a good location for a 1mx1m object  
                        //     System.Numerics.Vector2 location;
                        //     if (quads[0].FindCentermostPlacement(new System.Numerics.Vector2(1.0f, 1.0f), out location))
                        //     {
                        //         // We found one, anchor something to the transform
                        //         // Step 1: Create a new game object for the quad itself as a child of the scene root
                        //         // Step 2: Set the local transform from quads[0].Position and quads[0].Orientation
                        //         // Step 3: Create your hologram and set it as a child of the quad's game object
                        //         // Step 4: Set the hologram's local transform to a translation (location.x, location.y, 0)
                        //     }
                        // }
 
                        foreach (var sceneQuad in sceneObject.Quads)
                        {
                            var quad = GameObject.CreatePrimitive(PrimitiveType.Cube);
 
                            quad.transform.SetParent(marker.transform, false);
 
                            quad.transform.localScale = new Vector3(
                                sceneQuad.Extents.X, sceneQuad.Extents.Y, 0.025f);
 
                            quad.GetComponent<Renderer>().material = this.quadMaterial;
                        }
                    }
                }
            }
        }
    }
#endif

}