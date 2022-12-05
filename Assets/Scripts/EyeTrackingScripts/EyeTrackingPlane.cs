using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;

public class EyeTrackingPlane : MonoBehaviour
{
    private Vector3 gazeOrigin, gazeDir; // (HeadOrigin.x, HeadOrigin.y, HeadOrigin.z)
    private Vector3 headOrigin, headDir; // (HeadDir.x, HeadDir.y, HeadDir.z)

    // Smoothed eye gaze tracking 
    private Vector3 eyeOrigin; // (EyeOrigin.x, EyeOrigin.y, EyeOrigin.z)
    private Vector3 eyeDir; // (EyeDir.x, EyeDir.y, EyeDir.z)   
    private Vector3 eyeHitPos; // (EyeHitPos.x, EyeHitPos.y, EyeHitPos.z)

    void Start(){
        // Cam / Head tracking
        headOrigin = CameraCache.Main.transform.position;
        headDir = new Vector3(0.0f, 0.0f, 0.0f);

        // Smoothed eye gaze signal 
        gazeOrigin = new Vector3(0.0f, 0.0f, 0.0f);
        gazeDir = new Vector3(0.0f, 0.0f, 0.0f);
        eyeHitPos = new Vector3(float.NaN, float.NaN, float.NaN);

    }

    // Update is called once per frame
    void Update()
    {

        if ((CoreServices.InputSystem != null) && (CoreServices.InputSystem.EyeGazeProvider != null) &&
            CoreServices.InputSystem.EyeGazeProvider.IsEyeTrackingEnabled &&
            CoreServices.InputSystem.EyeGazeProvider.IsEyeTrackingDataValid &&
            CoreServices.InputSystem.EyeGazeProvider.GazeTarget == this.gameObject){ //if the gaze target is the eyePlane

            // Origin of the gaze ray - return the head gaze origin if 'IsEyeGazeValid' is false.
            gazeOrigin = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin; 

            // Normal of the gaze - vector3
            gazeDir = CoreServices.InputSystem.EyeGazeProvider.GazeDirection;

            // The current head movement direction - vector3
            headDir = CoreServices.InputSystem.EyeGazeProvider.HeadMovementDirection; 

            // The origin point and direction of the eye ray.
            eyeOrigin = CoreServices.InputSystem.EyeGazeProvider.LatestEyeGaze.origin; 
            eyeDir = CoreServices.InputSystem.EyeGazeProvider.LatestEyeGaze.direction;

            // Position (point) at which the gaze manager hit an object -> user is looking at it
            eyeHitPos = CoreServices.InputSystem.EyeGazeProvider.HitPosition;

            EyeTrackerDataCollector.Instance.addToFile(new MyEyeTrackerData(Constants.EYETRACKING_PLANE, ParseQRInfoManager.Instance.setUpInfo.patientID, 
            "-1", gameObject.scene.name, headOrigin, headDir, gazeOrigin, gazeDir, eyeOrigin, eyeDir, eyeHitPos));

        }        
        
    }
}
