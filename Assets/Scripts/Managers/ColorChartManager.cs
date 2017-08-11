using UnityEngine;

public static class ColorChartManager {
    private static ColorChart chart = null;

    static ColorChartManager(){
        string path = "ColorChart";
        chart = Resources.Load<ColorChart>(path);
    }

    public static Color GetColorMaterial(EColor color) {
        return chart.GetColor(color);
    }
}
