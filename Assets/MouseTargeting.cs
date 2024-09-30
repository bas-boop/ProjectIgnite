using UnityEngine;

public class MouseTargeting : MonoBehaviour
{
    public GameObject targetIndicator;
    private bool isTargetMode = false;

    void Start()
    {
        targetIndicator.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isTargetMode = !isTargetMode;
            targetIndicator.SetActive(isTargetMode);
        }

        if (isTargetMode)
        {
            TrackMousePosition();
        }
    }

    void TrackMousePosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; 

        targetIndicator.transform.position = mouseWorldPosition;
    }
}