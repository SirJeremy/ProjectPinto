using UnityEngine;
public struct IndexVector {
    #region Variables
    private int x;
    private int y;
    #endregion

    #region Properties
    public int X { get { return x; } }
    public int Y { get { return y; } }
    public static IndexVector Zero { get { return new IndexVector(0, 0); } }
    public static IndexVector Up { get { return new IndexVector(0, 1); } }
    public static IndexVector Down { get { return new IndexVector(0, -1); } }
    public static IndexVector Left { get { return new IndexVector(-1, 0); } }
    public static IndexVector Right { get { return new IndexVector(1, 0); } }
    public Vector2 ToVector2 { get { return new Vector2(X, Y); } }
    public Vector3 ToVector3 { get { return new Vector3(X, 0, Y); } }
    #endregion

    #region Constructors
    public IndexVector(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public IndexVector(Vector2 vector) {
        x = (int)vector.x;
        y = (int)vector.y;
    }
    #endregion

    #region Methods
    public static IndexVector GetDirection(EDirection direction) {
        switch(direction) {
            case EDirection.UP:
                return Up;
            case EDirection.DOWN:
                return Down;
            case EDirection.LEFT:
                return Left;
            case EDirection.RIGHT:
                return Right;
            default:
                Debug.LogError("Invalid case " + direction);
                return Up;
        }
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
        return new IndexVector(vectorA.X + vectorB.X, vectorA.Y + vectorB.Y);
    }
    public static IndexVector operator -(IndexVector vectorA, IndexVector vectorB) {
        return new IndexVector(vectorA.X - vectorB.X, vectorA.Y - vectorB.Y);
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
    #endregion
}