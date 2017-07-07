public struct IndexVector {
    private int x;
    private int y;

    public int X { get { return x; } }
    public int Y { get { return y; } }
    public static IndexVector Zero { get { return new IndexVector(0, 0); } }
    public static IndexVector Up { get { return new IndexVector(0, 1); } }
    public static IndexVector Down { get { return new IndexVector(0, -1); } }
    public static IndexVector Left { get { return new IndexVector(-1, 0); } }
    public static IndexVector Right { get { return new IndexVector(1, 0); } }

    public IndexVector(int x, int y) {
        this.x = 0;
        this.y = 0;
    }

    public override string ToString() {
        return "(" + x + "," + y + ")";
    }
    public override bool Equals(object obj) {
        if(obj.GetType() != GetType())
            return false;

        IndexVector vector = (IndexVector)obj;
        return (X == vector.X) && (Y == vector.Y);
    }
    public bool Equals(IndexVector vector) {
        return (X == vector.X) && (Y == vector.Y);
    }
    public static bool operator ==(IndexVector vectorA, IndexVector vectorB) {
        return vectorA.Equals(vectorB);
    }
    public static bool operator !=(IndexVector vectorA, IndexVector vectorB) {
        return !(vectorA == vectorB);
    }
    public static IndexVector operator +(IndexVector vectorA, IndexVector vectorB) {
        return new IndexVector(vectorA.X + vectorB.X, vectorA.Y + vectorA.Y);
    }
    public static IndexVector operator -(IndexVector vectorA, IndexVector vectorB) {
        return new IndexVector(vectorA.X - vectorB.X, vectorA.Y - vectorA.Y);
    }
    public static IndexVector operator *(IndexVector vector, int number) {
        return new IndexVector(vector.X * number, vector.Y * number);
    }
    public static IndexVector operator /(IndexVector vector, int number) {
        return new IndexVector(vector.X / number, vector.Y / number);
    }
    public override int GetHashCode() {
        unchecked {
            const int hashBase = 113;
            const int hashMul = 421;
            int hash = hashBase;
            hash = (hash * hashMul) ^ x.GetHashCode();
            hash = (hash * hashMul) ^ y.GetHashCode();
            return hash;
        }
    }
}