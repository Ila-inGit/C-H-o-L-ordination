using System.Collections.Generic;
class Constants
{
    Dictionary<SceneNames, string> enumToCurrentScene = new Dictionary<SceneNames, string>();
    Dictionary<SceneNames, string> enumToNextScene = new Dictionary<SceneNames, string>();

    #region Scene Names
    public const string MAIN_CAMERA_TAG = "MainCamera";
    public const string MY_MANAGER_SCENE = "0-MyManagerScene";
    public const string SETTING_SCENE = "SettingScene";
    public const string TUTORIAL_FIRST_PART = "3-TutorialFirstPart";
    public const string TUTORIAL_SECOND_PART = "4-TutorialSecondPart";
    public const string ACTIVITY_SCENE_CONSTANT = "5-ActivitySceneConstant";
    public const string ACTIVITY_SCENE_NATURAL = "6-ActivitySceneNatural";
    public const string ACTIVITY_SCENE_FIGURE_EIGHT = "7-ActivitySceneFigureEight";
    public const string ACTIVITY_SCENE_HARMONIC = "8-ActivitySceneHarmonic";
    public const string TRANSITION_SCENE = "TransitionScene";
    #endregion Scene Names

    #region Tag names
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
    public const string ACTIVATION_TUTORIAL = "ActivationTutorial";

    #endregion Tag names

    public void init()
    {
        enumToCurrentScene.Add(SceneNames.MY_MANAGER_SCENE, MY_MANAGER_SCENE);
        enumToCurrentScene.Add(SceneNames.SETTING_SCENE, SETTING_SCENE);
        enumToCurrentScene.Add(SceneNames.TUTORIAL_FIRST_PART, TUTORIAL_FIRST_PART);
        enumToCurrentScene.Add(SceneNames.TUTORIAL_SECOND_PART, TUTORIAL_SECOND_PART);
        enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_CONSTANT, ACTIVITY_SCENE_CONSTANT);
        enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_NATURAL, ACTIVITY_SCENE_NATURAL);
        enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_FIGURE_EIGHT, ACTIVITY_SCENE_FIGURE_EIGHT);
        enumToCurrentScene.Add(SceneNames.ACTIVITY_SCENE_HARMONIC, ACTIVITY_SCENE_HARMONIC);
        enumToCurrentScene.Add(SceneNames.TRANSITION_SCENE, TRANSITION_SCENE);
        enumToNextScene.Add(SceneNames.MY_MANAGER_SCENE, SETTING_SCENE);
        enumToNextScene.Add(SceneNames.SETTING_SCENE, TUTORIAL_FIRST_PART);
        enumToNextScene.Add(SceneNames.TUTORIAL_FIRST_PART, TUTORIAL_SECOND_PART);
        enumToNextScene.Add(SceneNames.TUTORIAL_SECOND_PART, ACTIVITY_SCENE_CONSTANT);
        enumToNextScene.Add(SceneNames.ACTIVITY_SCENE_CONSTANT, ACTIVITY_SCENE_NATURAL);
        enumToNextScene.Add(SceneNames.ACTIVITY_SCENE_NATURAL, ACTIVITY_SCENE_FIGURE_EIGHT);
        enumToNextScene.Add(SceneNames.ACTIVITY_SCENE_FIGURE_EIGHT, ACTIVITY_SCENE_HARMONIC);
        enumToNextScene.Add(SceneNames.ACTIVITY_SCENE_HARMONIC, TRANSITION_SCENE);
    }

    public string getCurrentName(SceneNames name)
    {
        return enumToCurrentScene[name];
    }

    public string getNextName(SceneNames name)
    {
        return enumToNextScene[name];
    }

}

public enum Difficulty { easy, difficult };

public enum SceneNames
{
    MY_MANAGER_SCENE,
    SETTING_SCENE,
    TUTORIAL_FIRST_PART,
    TUTORIAL_SECOND_PART,
    ACTIVITY_SCENE_CONSTANT,
    ACTIVITY_SCENE_NATURAL,
    ACTIVITY_SCENE_FIGURE_EIGHT,
    ACTIVITY_SCENE_HARMONIC,
    TRANSITION_SCENE
}
