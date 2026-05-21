using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Reference Resolution")]
    [SerializeField] private float referenceWidth;
    [SerializeField] private float referenceHeight;
    private float referenceOrthographic;
    [Header("Crop")]
    [SerializeField] private bool preventCrop = true;

    void Start()
    {
        ApplyCameraSize();
    }

    private void ApplyCameraSize()
    {
        Camera cam = GetComponent<Camera>();
        referenceOrthographic = cam.orthographicSize;

        if (!cam.orthographic)
            return;

        float referenceAspect = referenceWidth / referenceHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        float sizeByWidth = referenceOrthographic * (referenceAspect / currentAspect);

        if (preventCrop)
        {
            cam.orthographicSize = Mathf.Max(referenceOrthographic, sizeByWidth);
        }
        else
        {
            cam.orthographicSize = sizeByWidth;
        }
    }
}
