using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;


public class CameraMovementController : MonoBehaviour
{
    [Header("Required Objs")]
    [SerializeField] private Camera main_camera;
    [SerializeField] private Transform camera_target;
    [SerializeField] private GameInputController input;

    [SerializeField] private float move_speed = 20f;

    private void Update()
    {
        float dt = Time.unscaledDeltaTime;

        updateMovement();
    }

    private void updateMovement()
    {
        Vector3 forward = main_camera.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = main_camera.transform.right;
        right.y = 0f;
        right.Normalize();

        Vector2 input_direction = input.getCameraMovement();

        Vector3 move_direction = forward * input_direction.y + right * input_direction.x;
        camera_target.position += move_direction * move_speed * Time.deltaTime;
    }
}