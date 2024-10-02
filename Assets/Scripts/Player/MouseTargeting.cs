using UnityEngine;

public class MouseTargeting : MonoBehaviour
{
    [SerializeField] private GameObject targetIndicator;

    private Camera mainCam;
    private bool isTargetMode;

    private void Start()
    {
        mainCam = Camera.main;
        targetIndicator.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isTargetMode = !isTargetMode;
            targetIndicator.SetActive(isTargetMode);
        }

        if (isTargetMode) 
            TrackMousePosition();
    }

    private void TrackMousePosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = mainCam.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f; 

        targetIndicator.transform.position = mouseWorldPosition;
    }
}