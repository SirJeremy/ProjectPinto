[System.Flags]
public enum ETraversableDirection : short {
    NON_TRAVERSBLE   = 0,
    FROM_DOWN       = 1 << 0,
    FROM_UP         = 1 << 1,
    FROM_LEFT       = 1 << 2,
    FROM_RIGHT      = 1 << 3,
    HORIZONTAL      = FROM_LEFT | FROM_RIGHT,
    VERTICAL        = FROM_DOWN | FROM_UP,
    FULLY_TRAVERSABLE     = FROM_DOWN | FROM_UP | FROM_LEFT | FROM_RIGHT 
};
