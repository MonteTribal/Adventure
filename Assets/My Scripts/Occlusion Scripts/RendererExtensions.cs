using UnityEngine;

public static class RendererExtensions
{
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        if (!camera) //no camera, render it
        {
            return true;
        }

        else if (Vector3.Distance(renderer.transform.position, camera.transform.position) > camera.farClipPlane*1.3f) //if to far, hide
        {
            return false;
        }



        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}