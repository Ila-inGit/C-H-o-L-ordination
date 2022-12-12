class Constants
{
    #region Scene Names
    public const string MAIN_CAMERA_TAG = "MainCamera";
    public const string MY_MANAGER_SCENE = "0-MyManagerScene";
    public const string SETTING_SCENE = "SettingScene";
    public const string TUTORIAL_FIRST_PART = "3-TutorialFirstPart";
    public const string TUTORIAL_SECOND_PART = "4-TutorialSecondPart";
    public const string START_ACTIVITIES_SCENE = "5-StartActivitiesScene";
    public const string ACTIVITY_SCENE_CONSTANT = "6-ActivitySceneConstant";
    public const string ACTIVITY_SCENE_NATURAL = "7-ActivitySceneNatural";
    public const string ACTIVITY_SCENE_FIGURE_EIGHT = "8-ActivitySceneFigureEight";
    public const string ACTIVITY_SCENE_HARMONIC = "9-ActivitySceneHarmonic";
    public const string TRANSITION_SCENE = "TransitionScene";
    public const string FINAL_SCENE = "FinalScene";
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
    public const string RESET_TIMER = "ResetTimer";
    public const string TIME_BEFORE_CLICK = "TimeBeforeClick";
    public const string ANGLE = "Angle";
    public const string PLANET = "Planet";
    public const string CONTINUE_BUTTON = "ContinueButton";
    public const string POINTS_COUNTER = "PointsCounter";
    public const string ACTIVATION_TUTORIAL = "ActivationTutorial";

    #endregion Tag names

}

public enum Difficulty
{
    EASY, MEDIUM, DIFFICULT
};

public enum SceneNames
{
    MY_MANAGER_SCENE,
    SETTING_SCENE,
    TUTORIAL_FIRST_PART,
    TUTORIAL_SECOND_PART,
    START_ACTIVITIES_SCENE,
    ACTIVITY_SCENE_CONSTANT,
    ACTIVITY_SCENE_NATURAL,
    ACTIVITY_SCENE_FIGURE_EIGHT,
    ACTIVITY_SCENE_HARMONIC,
    TRANSITION_SCENE,
    FINAL_SCENE
}

public class SceneDifficulty
{
    public SceneNames name { get; set; }
    public Difficulty difficulty { get; set; }

    public bool isMusicActive { get; set; }
    public bool isRhythmActive { get; set; }

    public bool isRhythmNotSynch { get; set; }
    public bool isMusicNotSynch { get; set; }

    public SceneDifficulty(SceneNames name, Difficulty difficulty, bool isMusicActive, bool isRhythmActive, bool isMusicNotSynch, bool isRhythmNotSynch)
    {
        this.name = name;
        this.difficulty = difficulty;
        this.isMusicActive = isMusicActive;
        this.isRhythmActive = isRhythmActive;
        this.isRhythmNotSynch = isRhythmNotSynch;
        this.isMusicNotSynch = isMusicNotSynch;
    }
}
