using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float panSpeed = 5;
    float zoomSpeed = 1000;

    float minCamSize = 1;
    float maxCamSize = 90;

    Vector2 cameraXBounds = new Vector2(-1, 159);

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(159.2f, 89.5f, -10f);
            Camera.main.orthographicSize = 90;
        }
    }

    void Move()
    {
        float zoom = Input.GetAxisRaw("Mouse ScrollWheel") * -1;
        float zoomAmount = Camera.main.orthographicSize + zoom * zoomSpeed * Time.deltaTime;
        Camera.main.orthographicSize = Mathf.Clamp(zoomAmount, minCamSize, maxCamSize);

        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        //transform.Translate(moveDirection * panSpeed * Camera.main.orthographicSize * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x + (moveDirection.x * panSpeed * Camera.main.orthographicSize * Time.deltaTime), cameraXBounds.x - width / 2, cameraXBounds.y - width / 2),
            transform.position.y + moveDirection.y * panSpeed * Camera.main.orthographicSize * Time.deltaTime, -10);
    }
}
