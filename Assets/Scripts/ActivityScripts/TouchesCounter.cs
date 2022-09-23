using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchesCounter : MonoBehaviour
{
    [HideInInspector] public int totalTouches = 0;
    private int _topCounter, _bottomCounter, _leftCounter, _rightCounter = 0;

    private int _topCounterOutside, _bottomCounterOutside, _leftCounterOutside, _rightCounterOutside = 0;
    [HideInInspector] public bool isInsideBox = false;
    [HideInInspector] public bool isInsideAngle = false;
    [HideInInspector] public string boxTag = "";
    [HideInInspector] public string angleTag = "";

    public void IncrementBoxCounter()
    {
        ControlBoxInteraction();
        totalTouches = _bottomCounter + _topCounter + _leftCounter + _rightCounter;
    }


    public void ControlBoxInteraction()
    {

        if (isInsideBox)
        {
            switch (boxTag)
            {
                case Constants.TOP_BOX:
                    {
                        _topCounter++;
                        //da vedere come gestire valori subject, timestamp, 
                        // Debug.Log("top: " + _topCounter + "by " + boxTag);
                        DataCollector.Instance.addToFile(new MyData(Constants.TOP_BOX, 1, "-1", gameObject.scene.name, "11", "-1", "11", -1, -1, "111", _topCounter));
                    }
                    break;
                case Constants.BOTTOM_BOX:
                    {
                        _bottomCounter++;
                        // Debug.Log("bottom: " + _bottomCounter + "by " + boxTag);
                        DataCollector.Instance.addToFile(new MyData(Constants.BOTTOM_BOX, 1, "-1", gameObject.scene.name, "_00", "-1", "11", -1, -1, "100", _bottomCounter));
                    }
                    break;
                case Constants.LEFT_BOX:
                    {
                        _leftCounter++;
                        // Debug.Log("left: " + _leftCounter + "by " + boxTag);
                        DataCollector.Instance.addToFile(new MyData(Constants.LEFT_BOX, 1, "-1", gameObject.scene.name, "10", "-1", "11", -1, -1, "110", _leftCounter));
                    }
                    break;
                case Constants.RIGHT_BOX:
                    {
                        _rightCounter++;
                        // Debug.Log("right: " + _rightCounter + "by " + boxTag);

                        DataCollector.Instance.addToFile(new MyData(Constants.RIGHT_BOX, 1, "-1", gameObject.scene.name, "_01", "-1", "11", -1, -1, "101", _rightCounter));
                    }
                    break;
            }
        }
        else if (isInsideAngle)
        {
            switch (angleTag)
            {
                case Constants.TOP_ANGLE:
                    {
                        _topCounterOutside++;
                        //Debug.Log("top: " + _topCounterOutside + "by " + angleTag);
                        DataCollector.Instance.addToFile(new MyData(Constants.TOP_ANGLE, 1, "-1", gameObject.scene.name, "-1", "11", "_01", -1, -1, "_011", _topCounterOutside));
                    }
                    break;
                case Constants.BOTTOM_ANGLE:
                    {
                        _bottomCounterOutside++;
                        //Debug.Log("bottom: " + _bottomCounterOutside + "by " + angleTag);
                        DataCollector.Instance.addToFile(new MyData(Constants.BOTTOM_ANGLE, 1, "-1", gameObject.scene.name, "-1", "_00", "_01", -1, -1, "_000", _bottomCounterOutside));
                    }
                    break;
                case Constants.LEFT_ANGLE:
                    {
                        _leftCounterOutside++;
                        //Debug.Log("left: " + _leftCounterOutside + "by " + angleTag);
                        DataCollector.Instance.addToFile(new MyData(Constants.LEFT_ANGLE, 1, "-1", gameObject.scene.name, "-1", "10", "_01", -1, -1, "_010", _leftCounterOutside));
                    }
                    break;
                case Constants.RIGHT_ANGLE:
                    {
                        _rightCounterOutside++;
                        //Debug.Log("right: " + _rightCounterOutside + "by " + angleTag);
                        DataCollector.Instance.addToFile(new MyData(Constants.RIGHT_ANGLE, 1, "-1", gameObject.scene.name, "-1", "_01", "_01", -1, -1, "_001", _rightCounterOutside));
                    }
                    break;
            }
        }
    }

    public void SetIsInsideBox(bool insideBox, string tag)
    {
        isInsideBox = insideBox;
        boxTag = tag;
        //Debug.Log("set is inside box from touches counter");
    }
    public void SetIsInsideAngle(bool insideAngle, string tag)
    {
        isInsideAngle = insideAngle;
        angleTag = tag;
        //Debug.Log("set is inside angle from touches counter");
    }
}
