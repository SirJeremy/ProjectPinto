using UnityEngine;
using UnityEngine.UI;

public class LevelMenuNavigation : MenuNavigation  {
    [SerializeField]
    protected MenuNavigation landingMenu;
    [SerializeField]
    private WorldSet levelData;
    [SerializeField]
    private GameObject worldParent;
    [SerializeField]
    private GameObject levelParent;

    private GameObject[] levelButtons;
    
    private bool isInWorldSelect = true;
    private bool isInGame = false;
    private int currentWorldLoaded = -1;
    private int currentWorldSelected = -1;

    protected override void Awake() {
        base.Awake();
        PopulateWorldMenu();
    }

    private void PopulateWorldMenu() {
        Button button;
        for(int i = 0; i < levelData.NumberOfWorlds; i++) {
            ProceduralButton.CreateWorldButton(worldParent.transform, levelData.GetWorld(i).worldName, out button);
            int x = i; //if it uses i, it will send levelData.NumberOfWorlds for the event call for what ever reason
            button.onClick.AddListener(() => OnWorldButtonClick(x)); //Add lisitenter to the button's onClick event
        }
    }
    private void PopulateLevelMenu(WorldData world, int worldIndex) /* Generates buttons for levels procedually using levelData */ {
        //If its the same world, then don't do anything
        if(currentWorldLoaded == worldIndex)
            return;
        //Initialize
        Button button; //used to add listenter to its onclick
        //Check if buttons already exits, if so, remove them
        if(levelButtons != null) {
            for(int i = 0; i < levelButtons.Length; i++) {
                Destroy(levelButtons[i]);
            }
        }
        //Initialize arr
        levelButtons = new GameObject[world.NumberOfLevels];
        //Create buttons
        for(int i = 0; i < world.NumberOfLevels; i++) {
            levelButtons[i] = ProceduralButton.CreateLevelButton(levelParent.transform, world.GetLevel(i).levelName, out button);
            int x = i; //if it uses i, it will send world.NumberOfLevels for the event call for what ever reason
            button.onClick.AddListener(() => OnLevelButtonClick(x)); //Add lisitenter to the button's onClick event
        }
        currentWorldLoaded = worldIndex;
    }

    private void NavigateToWorldSelect() {
        worldParent.SetActive(true);
        levelParent.SetActive(false);
        isInWorldSelect = true;
    }
    private void NavigateToLevelSelect() {
        PopulateLevelMenu(levelData.GetWorld(currentWorldSelected), currentWorldSelected);
        worldParent.SetActive(false);
        levelParent.SetActive(true);
        isInWorldSelect = false;
    }

    public void OnWorldButtonClick(int index) {
        currentWorldSelected = index;
        NavigateToLevelSelect();
    }
    public void OnLevelButtonClick(int index) {
        
    }

    public override void NavigateTo() {
        base.NavigateTo();
        InputManager.BackButtonLeavesApp = true;
    }
    protected override void OnCancelInput() {
        if(!NotificationWindow.IsShowingNotification) {
            if(isInWorldSelect) {
                landingMenu.NavigateTo();
                NavigateAway();
            }
            else
                NavigateToWorldSelect();
        }
    }
}
