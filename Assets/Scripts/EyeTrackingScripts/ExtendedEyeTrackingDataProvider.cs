using System;
using Microsoft.MixedReality.EyeTracking;
using UnityEngine;
#if ENABLE_WINMD_SUPPORT
using System.Threading.Tasks;
using Windows.Perception;
using Windows.Perception.Spatial;
#endif

namespace EyeTrackingScripts
{
    /// <summary>
    /// This class provides access to the Extended Eye Gaze Tracking API 
    /// Values are given in Unity world space or relative to the main camera
    /// </summary>
    [DisallowMultipleComponent]
    public class ExtendedEyeTrackingDataProvider : MonoBehaviour
    {
        public enum GazeType
        {
            Left,
            Right,
            Combined
        }

        public class GazeReading
        {
            public Vector3 EyePosition;
            public Vector3 GazeDirection;

            public GazeReading()
            {
            }

            public GazeReading(Vector3 position, Vector3 direction)
            {
                EyePosition = position;
                GazeDirection = direction;
            }
        }

        private Camera _mainCamera;
        private GazeReading _gazeReading = new GazeReading();
        private GazeReading _transformedGazeReading = new GazeReading();
        private EyeGazeTrackerWatcher _watcher;
        private EyeGazeTracker _eyeGazeTracker;
        private EyeGazeTrackerReading _eyeGazeTrackerReading;
        private System.Numerics.Vector3 _trackerPosition;
        private System.Numerics.Quaternion _trackerOrientation;
        private System.Numerics.Matrix4x4 _trackerToUnityWorldSpaceMatrix;
        private System.Numerics.Vector3 _trackerSpaceGazeOrigin;
        private System.Numerics.Vector3 _trackerSpaceGazeDirection;
        private bool _gazePermissionEnabled;
        private bool _readingSucceeded;
#if ENABLE_WINMD_SUPPORT
    private SpatialLocator _trackerLocator;
    private SpatialLocation _trackerLocation;
    private SpatialCoordinateSystem _unityWorldSpaceSpatialCoordinateSystem;
    PerceptionTimestamp _perceptionTimestamp;
#endif


        /// <summary>
        /// Get the current reading for the requested GazeType, relative to the main camera
        /// Will return null if unable to return a valid reading
        /// </summary>
        /// <param name="gazeType"></param>
        /// <returns></returns>
        public GazeReading GetCameraSpaceGazeReading(GazeType gazeType)
        {
            return GetCameraSpaceGazeReading(gazeType, DateTime.Now);
        }

        /// <summary>
        /// Get the reading for the requested GazeType at the given TimeStamp, relative to the main camera
        /// Will return null if unable to return a valid reading
        /// </summary>
        /// <param name="gazeType"></param>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public GazeReading GetCameraSpaceGazeReading(GazeType gazeType, DateTime timeStamp)
        {
            _gazeReading = GetWorldSpaceGazeReading(gazeType, timeStamp);
            if (_gazeReading == null)
            {
                return null;
            }

            _transformedGazeReading.EyePosition = _mainCamera.transform.InverseTransformPoint(_gazeReading.EyePosition);
            _transformedGazeReading.GazeDirection =
                _mainCamera.transform.InverseTransformDirection(_gazeReading.GazeDirection).normalized;

            return _transformedGazeReading;
        }

        /// <summary>
        /// Get the current reading for the requested GazeType
        /// Will return null if unable to return a valid reading
        /// </summary>
        /// <param name="gazeType"></param>
        /// <returns></returns>
        public GazeReading GetWorldSpaceGazeReading(GazeType gazeType)
        {
            return GetWorldSpaceGazeReading(gazeType, DateTime.Now);
        }

        /// <summary>
        /// Get the reading for the requested GazeType at the given TimeStamp
        /// Will return null if unable to return a valid reading
        /// </summary>
        /// <param name="gazeType"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public GazeReading GetWorldSpaceGazeReading(GazeType gazeType, DateTime timestamp)
        {
            if (!_gazePermissionEnabled || _eyeGazeTracker == null)
            {
                return null;
            }

            _eyeGazeTrackerReading = _eyeGazeTracker.TryGetReadingAtTimestamp(timestamp);
            if (_eyeGazeTrackerReading == null)
            {
                Debug.LogWarning($"Unable to get eyeGazeTrackerReading at: {timestamp.ToLongTimeString()}");
                return null;
            }

            _gazeReading = null;

#if ENABLE_WINMD_SUPPORT
        // compute tracker to stationary coordinate system transform
        _perceptionTimestamp = PerceptionTimestampHelper.FromHistoricalTargetTime(timestamp);
        _trackerLocation =
 _trackerLocator.TryLocateAtTimestamp(_perceptionTimestamp, _unityWorldSpaceSpatialCoordinateSystem);

        if (_trackerLocation == null)
        {
            return null;
        }

        _trackerOrientation = _trackerLocation.Orientation;
        _trackerPosition = _trackerLocation.Position;
        _trackerToUnityWorldSpaceMatrix =
 System.Numerics.Matrix4x4.CreateFromQuaternion(_trackerOrientation) * System.Numerics.Matrix4x4.CreateTranslation(_trackerPosition);

        switch (gazeType)
        {
            case GazeType.Left:
            {
                // Get left eye gaze
                _readingSucceeded =
 _eyeGazeTrackerReading.TryGetLeftEyeGazeInTrackerSpace(out _trackerSpaceGazeOrigin, out _trackerSpaceGazeDirection);
                if (_readingSucceeded)
                {
                    // return the gaze reading in Unity world space
                    return new GazeReading(
                    // the gaze origin in world space
                    System.Numerics.Vector3.Transform(_trackerSpaceGazeOrigin, _trackerToUnityWorldSpaceMatrix).ToUnity(),

                    // the gaze direction in world space
                    System.Numerics.Vector3.TransformNormal(_trackerSpaceGazeDirection, _trackerToUnityWorldSpaceMatrix).ToUnity().normalized
                    );
                }

                // unable to get reading
                return null;
            }
            case GazeType.Right:
            {
                // Get right eye gaze
                _readingSucceeded =
 _eyeGazeTrackerReading.TryGetRightEyeGazeInTrackerSpace(out _trackerSpaceGazeOrigin, out _trackerSpaceGazeDirection);
                if (_readingSucceeded)
                {
                    // return the gaze reading in Unity world space
                    return new GazeReading(
                    // the gaze origin in world space
                    System.Numerics.Vector3.Transform(_trackerSpaceGazeOrigin, _trackerToUnityWorldSpaceMatrix).ToUnity(),

                    // the gaze direction in world space
                    System.Numerics.Vector3.TransformNormal(_trackerSpaceGazeDirection, _trackerToUnityWorldSpaceMatrix).ToUnity().normalized
                    );
                }

                // unable to get reading
                return null;
            }
            case GazeType.Combined:
            {
                // Get combined eye gaze
                _readingSucceeded =
 _eyeGazeTrackerReading.TryGetCombinedEyeGazeInTrackerSpace(out _trackerSpaceGazeOrigin, out _trackerSpaceGazeDirection);
                if (_readingSucceeded)
                {
                    // return the gaze reading in Unity world space
                    return new GazeReading(
                    // the gaze origin in world space
                    System.Numerics.Vector3.Transform(_trackerSpaceGazeOrigin, _trackerToUnityWorldSpaceMatrix).ToUnity(),

                    // the gaze direction in world space
                    System.Numerics.Vector3.TransformNormal(_trackerSpaceGazeDirection, _trackerToUnityWorldSpaceMatrix).ToUnity().normalized
                    );
                }

                // unable to get reading
                return null;
            }
        }
#endif

            return _gazeReading;
        }

        private async void Start()
        {
            _mainCamera = Camera.main;

            Debug.Log("Initializing MedicalEyeTracking");
#if ENABLE_WINMD_SUPPORT
#if UNITY_2020_1_OR_NEWER
        if ((bool)IsLoaderActive<UnityEngine.XR.OpenXR.OpenXRLoaderBase>())
        {
            _unityWorldSpaceSpatialCoordinateSystem =
 Microsoft.MixedReality.OpenXR.PerceptionInterop.GetSceneCoordinateSystem(Pose.identity) as SpatialCoordinateSystem;
        }
        else
        {
            _unityWorldSpaceSpatialCoordinateSystem =
 (SpatialCoordinateSystem)System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(UnityEngine.XR.WindowsMR.WindowsMREnvironment.OriginSpatialCoordinateSystem);
        }
#else
        _unityWorldSpaceSpatialCoordinateSystem =
 (SpatialCoordinateSystem)System.Runtime.InteropServices.Marshal.GetObjectForIUnknown(UnityEngine.XR.WSA.WorldManager.GetNativeISpatialCoordinateSystemPtr());
#endif
        Debug.Log("Triggering eye gaze permission request");
        _gazePermissionEnabled = await AskForEyePosePermission();
#endif

            if (!_gazePermissionEnabled)
            {
                Debug.LogError("Gaze is disabled");
                return;
            }

            _watcher = new Microsoft.MixedReality.EyeTracking.EyeGazeTrackerWatcher();
            _watcher.EyeGazeTrackerAdded += _watcher_EyeGazeTrackerAdded;
            _watcher.EyeGazeTrackerRemoved += _watcher_EyeGazeTrackerRemoved;
            await _watcher.StartAsync();
        }

        private void _watcher_EyeGazeTrackerRemoved(object sender, EyeGazeTracker e)
        {
            Debug.Log("EyeGazeTracker removed");
            _eyeGazeTracker = null;
        }

        private async void _watcher_EyeGazeTrackerAdded(object sender, EyeGazeTracker e)
        {
            Debug.Log("EyeGazeTracker added");
            try
            {
                await e.OpenAsync(true);
                _eyeGazeTracker = e;
                var supportedFrameRates = _eyeGazeTracker.SupportedTargetFrameRates;
                foreach (var frameRate in supportedFrameRates)
                {
                    Debug.Log($"  supportedFrameRate: {frameRate.FramesPerSecond}");
                }

                _eyeGazeTracker.SetTargetFrameRate(supportedFrameRates[supportedFrameRates.Count - 1]);

#if ENABLE_WINMD_SUPPORT
            // Get a spatial locator for the tracker
            var trackerNodeId = e.TrackerSpaceLocatorNodeId;
            _trackerLocator =
 Windows.Perception.Spatial.Preview.SpatialGraphInteropPreview.CreateLocatorForNode(trackerNodeId);
#endif

            }
            catch (Exception ex)
            {
                Debug.LogError("Unable to open EyeGazeTracker\r\n" + ex.ToString());
            }
        }
    }
}
#if ENABLE_WINMD_SUPPORT
    /// <summary>
    /// Triggers a prompt to let the user decide whether to permit using eye tracking 
    /// </summary>
    private async Task<bool> AskForEyePosePermission()
    {
        var accessStatus = await Windows.Perception.People.EyesPose.RequestAccessAsync();
        Debug.Log("Eye gaze access status: " + accessStatus.ToString());
        return accessStatus == Windows.UI.Input.GazeInputAccessStatus.Allowed;
        //return true;
    }

    /// <summary>
    /// Checks if the active loader is of a specific type. Used in cases where the loader class is accessible, like OculusLoader.
    /// </summary>
    /// <typeparam name="T">The loader class type to check against the active loader.</typeparam>
    /// <returns>True if the active loader is of the specified type. Null if there isn't an active loader.</returns>
    public static bool? IsLoaderActive<T>() where T : UnityEngine.XR.Management.XRLoader
    {
        if (UnityEngine.XR.Management.XRGeneralSettings.Instance != null
            && UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager != null
            && UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager.activeLoader != null)
        {
            return UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager.activeLoader is T;
        }

        return null;
    }
#endif // WINDOWS_UWP