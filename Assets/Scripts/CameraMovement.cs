using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraHolderTransform;

    [SerializeField] float minZoom = -10;
    [SerializeField] float maxZoom =10;
    public float sencetivityX = 0.01f;
    public float sencetivityY = 0.01f;
    public float moveSencetivity = 0.02f;

    private void Update()
    {
        if (Mouse.current.rightButton.value != 0)
        {
            Vector2 mouseDelta = Mouse.current.delta.value;
            cameraHolderTransform.eulerAngles += Vector3.up * mouseDelta.x * sencetivityX;
            // убрал пока вращение по вертикали (Степан)
            // if (cameraHolderTransform.eulerAngles.x + mouseDelta.y * sencetivityY < 90)
            //     cameraHolderTransform.eulerAngles += Vector3.right * mouseDelta.y * sencetivityY;
            // else cameraHolderTransform.eulerAngles = new Vector3(90, cameraHolderTransform.eulerAngles.y, 0);
            if (cameraHolderTransform.eulerAngles.x > 180 || cameraHolderTransform.eulerAngles.x < 0)
                cameraHolderTransform.eulerAngles = new Vector3(0, cameraHolderTransform.eulerAngles.y, 0);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (transform.localPosition.z < maxZoom)
            {
                transform.GetComponent<Transform>().localPosition = new Vector3(0, 0, transform.localPosition.z + 2);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (transform.localPosition.z > minZoom)
            {
                transform.GetComponent<Transform>().localPosition = new Vector3(0, 0, transform.localPosition.z - 2);
            }
        }
        cameraHolderTransform.position += transform.forward * Input.GetAxis("Vertical") * moveSencetivity;
        cameraHolderTransform.position += transform.right * Input.GetAxis("Horizontal") * moveSencetivity;
        cameraHolderTransform.position = new Vector3(cameraHolderTransform.position.x, 0, cameraHolderTransform.position.z);
    }
}