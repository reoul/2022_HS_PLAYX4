using UnityEngine;

public static class ExtensionMethod
{
    public static void SetPosAllZero(this LineRenderer lineRenderer)
    {
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i,Vector3.zero);
        }
    }
}