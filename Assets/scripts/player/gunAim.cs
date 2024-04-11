using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunAim : MonoBehaviour
{
    public Transform gunTransform;
    public Camera mainCamera;

    private void Update()
    {
        if (gunTransform == null || mainCamera == null)
        {
            Debug.LogWarning("Gun transform or main camera is not assigned.");
            return;
        }

        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, gunTransform.position.z - mainCamera.transform.position.z));
        Vector3 direction = (mouseWorldPosition - gunTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
     
        gunTransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        if (90f < angle && angle < 180f || -180f < angle && angle < -90f)
        {
            gunTransform.localScale = new Vector3(1f, -1f, 1);
        }
        else
        {
            gunTransform.localScale = new Vector3(1f, 1f, 1);
        }
    }
}
