using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.Windows.Perception.Spatial.Preview;
namespace Microsoft.MixedReality.SceneUnderstanding
{
    public class SceneUnderstandingController : MonoBehaviour
    {
        public GameObject parentObject;
        public GameObject markerPrefab;
        public Material quadMaterial;

        Scene lastScene;

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

            if (this.lastScene != null)
            {
                var node = this.lastScene.OriginSpatialGraphNodeId;

                var sceneCoordSystem = SpatialGraphInteropPreview.CreateCoordinateSystemForNode(node);

                var unityCoordinateSystem = Microsoft.Windows.Perception.Spatial
                        .SpatialCoordinateSystem
                        .FromNativePtr(UnityEngine.XR.WindowsMR.WindowsMREnvironment.OriginSpatialCoordinateSystem);

                var transform = sceneCoordSystem.TryGetTransformTo(unityCoordinateSystem);

                if (transform.HasValue)
                {
                    var sceneToWorldUnity = transform.Value.ToUnity();

                    this.parentObject.transform.SetPositionAndRotation(
                        sceneToWorldUnity.GetColumn(3), sceneToWorldUnity.rotation);
                }
            }

        }

        // These 4 methods are wired to call by the user (start button in theory)
        public async void OnWalls()
        {
            await this.ComputeAsync(SceneObjectKind.Wall);
        }
        public async void OnFloor()
        {
            await this.ComputeAsync(SceneObjectKind.Floor);
        }
        public async void OnCeiling()
        {
            await this.ComputeAsync(SceneObjectKind.Ceiling);
        }
        public async void OnPlatform()
        {
            await this.ComputeAsync(SceneObjectKind.Platform);
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

                            // Get the quad
                            var quads = sceneObject.Quads;
                            if (quads.Count > 0 && this.markerPrefab)
                            {
                                // Find a good location for a 1mx1m object  
                                System.Numerics.Vector2 location;
                                if (quads[0].FindCentermostPlacement(new System.Numerics.Vector2(1.0f, 1.0f), out location))
                                {
                                    var prefab = Instantiate(this.markerPrefab);

                                    prefab.transform.SetPositionAndRotation(sceneObject.Position.ToUnityVector3(), sceneObject.Orientation.ToUnityQuaternion());
                                    float sx = sceneObject.Quads[0].Extents.X;
                                    float sy = sceneObject.Quads[0].Extents.Y;
                                    prefab.transform.localScale = new Vector3(sx, sy, .1f);
                                    if (parentObject)
                                    {
                                        prefab.transform.SetParent(this.parentObject.transform);
                                    }
                                    this.markers.Add(prefab);
                                }
                            }

                            // var marker = GameObject.Instantiate(this.markerPrefab);

                            // marker.transform.SetParent(this.parentObject.transform);

                            // marker.transform.localPosition = sceneObject.Position.ToUnity();
                            // marker.transform.localRotation = sceneObject.Orientation.ToUnity();

                            // this.markers.Add(marker);

                            // foreach (var sceneQuad in sceneObject.Quads)
                            // {
                            //     var quad = GameObject.CreatePrimitive(PrimitiveType.Cube);

                            //     quad.transform.SetParent(marker.transform, false);

                            //     quad.transform.localScale = new Vector3(
                            //         sceneQuad.Extents.X, sceneQuad.Extents.Y, 0.025f);

                            //     quad.GetComponent<Renderer>().material = this.quadMaterial;
                            // }
                        }
                    }
                }
            }
        }


    }
}

