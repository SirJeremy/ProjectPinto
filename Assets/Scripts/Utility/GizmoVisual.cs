using UnityEngine;
using System.Collections;

public class GizmoVisual : MonoBehaviour
{
    public enum GizmoType
    {
        Box,
        WireBox,
        Sphere,
        WireSphere
    };
    public Color color = Color.white;
    public GizmoType gizmoType = GizmoType.Box;
    public Vector3 offSet = Vector3.zero;
    public Vector3 boxSize = Vector3.one;
    public float sphereRadius = 1;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        switch(gizmoType)
        {
            case GizmoType.Box:
                Gizmos.DrawCube(transform.position + offSet, boxSize);
                break;
            case GizmoType.WireBox:
                Gizmos.DrawWireCube(transform.position + offSet, boxSize);
                break;
            case GizmoType.Sphere:
                Gizmos.DrawSphere(transform.position + offSet, sphereRadius);
                break;
            case GizmoType.WireSphere:
                Gizmos.DrawWireSphere(transform.position + offSet, sphereRadius);
                break;
            default:
                break;
        }
    }
}
