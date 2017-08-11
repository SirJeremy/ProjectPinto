using UnityEngine;

[CreateAssetMenu(fileName = "NewColorChart", menuName = "Custom/Color Chart")]
public class ColorChart : ScriptableObject {
    [System.Serializable]
    public class ColorMaterialPair {
        public EColor eColor = EColor.DEFAULT;
        public Color color = Color.white;
    }
    [SerializeField]
    private ColorMaterialPair[] chart = new ColorMaterialPair[0];

    public Color GetColor(EColor color) {
        for(int i = 0; i < chart.Length; i++) {
            if(chart[i].eColor == color) {
                return chart[i].color;
            }
        }
        Debug.LogError("Color " + color + " was not found");
        return Color.magenta;
    }
}
