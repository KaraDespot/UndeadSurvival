using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    private float minX = -20.9f;
    private float maxX = 20.7f;
    private float minY = -19f;
    private float maxY = 9.9f;

    public void RestrictPosition(Transform objTransform)
    {
        float clampedX = Mathf.Clamp(objTransform.position.x, minX, maxX);

        float clampedY = Mathf.Clamp(objTransform.position.y, minY, maxY);

        objTransform.position = new Vector3(clampedX, clampedY, objTransform.position.z);
    }

    public bool IsWithinBoundary(Transform objTransform)
    {
        return objTransform.position.x >= minX && objTransform.position.x <= maxX &&
               objTransform.position.y >= minY && objTransform.position.y <= maxY;
    }
}
