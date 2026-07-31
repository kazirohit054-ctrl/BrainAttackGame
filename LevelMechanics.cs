using UnityEngine;

public class LevelMechanics : MonoBehaviour
{
    
    public bool isDraggable = true;
    private Vector3 offset;
    private Camera mainCamera;

    
    public bool enableShakeMechanic = false;
    public float shakeThreshold = 2.0f;

    
    public bool enablePinchToScale = false;
    public float minScale = 0.5f;
    public float maxScale = 3.0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 1. Mobile Shake Detection (Phone ko hilane par puzzle solve hone ke liye)
        if (enableShakeMechanic)
        {
            if (Input.acceleration.sqrMagnitude >= shakeThreshold)
            {
                OnPhoneShaken();
            }
        }

        // 2. Pinch to Scale (Two Finger Zoom In / Zoom Out)
        if (enablePinchToScale && Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            ScaleObject(difference * 0.01f);
        }
    }

    // Drag & Drop Mechanics
    private void OnMouseDown()
    {
        if (!isDraggable) return;
        offset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        if (!isDraggable) return;
        transform.position = GetMouseWorldPos() + offset;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToViewportPoint(gameObject.transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void ScaleObject(float increment)
    {
        Vector3 newScale = transform.localScale + new Vector3(increment, increment, increment);
        newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
        newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
        newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);
        transform.localScale = newScale;
    }

    private void OnPhoneShaken()
    {
        Debug.Log("🎯 Phone Shaken! Puzzle Action Triggered.");
        // Puzzle specific logic trigger hoga
    }
}