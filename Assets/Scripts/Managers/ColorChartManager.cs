using UnityEngine;

public static class ColorChartManager {
    private static MaterialColorChart chart = null;

    static ColorChartManager(){
        string path = "MaterialColorChart";
        chart = Resources.Load<MaterialColorChart>(path);
    }

    public static Material GetColorMaterial(EColor color) {
        return chart.GetMaterial(color);
    }
}
