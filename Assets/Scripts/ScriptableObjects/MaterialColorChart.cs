using UnityEngine;

[CreateAssetMenu(fileName = "NewMaterialColorChart", menuName = "Custom/Material Color Chart")]
public class MaterialColorChart : ScriptableObject {
    [System.Serializable]
    public class ColorMaterialPair {
        public EColor color = EColor.DEFAULT;
        public Material material = null;
    }
    [SerializeField]
    private ColorMaterialPair[] chart = new ColorMaterialPair[0];

    public Material GetMaterial(EColor color) {
        for(int i = 0; i < chart.Length; i++) {
            if(chart[i].color == color) {
                return chart[i].material;
            }
        }
        Debug.LogError("Color " + color + " was not found");
        return null;
    }
}
