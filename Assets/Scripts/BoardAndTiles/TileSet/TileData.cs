[System.Serializable]
public class TileData {
    public ETile type = ETile.EMPTY;
    public EColor color = EColor.DEFAULT;

    public TileData() {
        type = ETile.EMPTY;
        color = EColor.DEFAULT;
    }
    public TileData(ETile type, EColor color) {
        this.type = type;
        this.color = color;
    }
}