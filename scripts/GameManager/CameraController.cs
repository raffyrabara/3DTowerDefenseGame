using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 2f;
    public float minY = 2.5f;
    public float maxY = 6f;
    public float initialminY =2.5f;
    public float initialminX =6f;
    public float minX = -50f; // Set your desired minimum X position
    public float maxX = 50f;  // Set your desired maximum X position
    public float minZ = -50f; // Set your desired minimum Z position
    public float maxZ = 50f;  // Set your desired maximum Z position

    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos += Vector3.forward * panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos += Vector3.back * panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos += Vector3.right * panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos += Vector3.left * panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        pos.x = Mathf.Clamp(pos.x, pos.y - 6, pos.y - 6); // Limit X position
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ); // Limit Z position

        transform.position = pos;
    }
}
