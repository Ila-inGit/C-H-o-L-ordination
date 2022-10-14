using System.Collections.Generic;
class Constants
{
    Dictionary<int, string> indexToNames = new Dictionary<int, string>();

    public const string MAIN_CAMERA_TAG = "MainCamera";
    public const string MY_MANAGER_SCENE = "MyManagerScene";
    public const string SETTING_SCENE = "SettingScene";
    public const string START_SCENE = "StartScene";
    public const string TUTORIAL_FIRST_PART = "TutorialFirstPart";
    public const string TUTORIAL_SECOND_PART = "TutorialSecondPart";
    public const string ACTIVITY_SCENE_CONSTANT = "ActivitySceneConstant";
    public const string ACTIVITY_SCENE_NATURAL = "ActivitySceneNatural";
    public const string ACTIVITY_SCENE_FIGURE_EIGHT = "ActivitySceneFigureEight";
    public const string ACTIVITY_SCENE_HARMONIC = "ActivitySceneHarmonic";
    public const string TRANSITION_SCENE = "TransitionScene";
    public const string TOP_BOX = "TopBox";
    public const string BOTTOM_BOX = "BottomBox";
    public const string LEFT_BOX = "LeftBox";
    public const string RIGHT_BOX = "RightBox";
    public const string TUTORIAL_BOX = "TutorialBox";
    public const string TOP_ANGLE = "TopAngle";
    public const string BOTTOM_ANGLE = "BottomAngle";
    public const string LEFT_ANGLE = "LeftAngle";
    public const string RIGHT_ANGLE = "RightAngle";
    public const string ANGLE = "Angle";
    public const string PLANET = "Planet";
    public const string CONTINUE_BUTTON = "ContinueButton";
    public const string POINTS_COUNTER = "PointsCounter";


    public void init()
    {
        indexToNames.Add(0, MY_MANAGER_SCENE);
        indexToNames.Add(2, SETTING_SCENE);
        indexToNames.Add(3, START_SCENE);
        indexToNames.Add(4, TUTORIAL_FIRST_PART);
        indexToNames.Add(5, TUTORIAL_SECOND_PART);
        indexToNames.Add(6, ACTIVITY_SCENE_CONSTANT);
        indexToNames.Add(7, ACTIVITY_SCENE_NATURAL);
        indexToNames.Add(8, ACTIVITY_SCENE_FIGURE_EIGHT);
        indexToNames.Add(9, ACTIVITY_SCENE_HARMONIC);
        indexToNames.Add(10, TRANSITION_SCENE);
    }

    public string getName(int index)
    {
        return indexToNames[index];
    }


}
