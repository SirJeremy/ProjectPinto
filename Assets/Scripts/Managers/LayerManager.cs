using UnityEngine;
using System.Collections;

public static class LayerManager {
    public readonly static int Player = 1 << 8;
    public readonly static int Enemy = 1 << 10;
    public readonly static int Ground = 1 << 14;
    public readonly static int Goal = 1 << 18;
    public readonly static int Hand = 1 << 20;
    public readonly static int Pressables = 1 << 17;
    public readonly static int NoCollide = 1 << 21;
}
