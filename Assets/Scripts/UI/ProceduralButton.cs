using UnityEngine;
using UnityEngine.UI;

public class ProceduralButton : MonoBehaviour {
    [SerializeField]
    private Button button;
    [SerializeField]
    private Text text;

    private static GameObject worldPrefab;
    private static GameObject levelPrefab;

    public static GameObject CreateWorldButton(Transform parent, string label, out Button button) {
        if(worldPrefab == null) {
            worldPrefab = Resources.Load<GameObject>("Prefabs/UI/WorldProceduralButton");
        }
        ProceduralButton pb = Instantiate(worldPrefab, parent).GetComponent<ProceduralButton>();
        pb.Initalize(label);
        button = pb.button;
        return pb.gameObject;
    }
    public static GameObject CreateLevelButton(Transform parent, string label, out Button button) {
        if(levelPrefab == null) {
            levelPrefab = Resources.Load<GameObject>("Prefabs/UI/LevelProceduralButton");
        }
        ProceduralButton pb = Instantiate(levelPrefab, parent).GetComponent<ProceduralButton>();
        pb.Initalize(label);
        button = pb.button;
        return pb.gameObject;
    }

    private void Initalize(string label) {
        text.text = label;
        gameObject.name = "Button_" + label;
        text.name = "Text_" + label;
    }
}
