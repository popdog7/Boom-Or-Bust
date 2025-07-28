using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private GameInputController input_controller;

    private GameObject selected_object;
    private float y_offset;

    private void Awake()
    {
        input_controller.onDragStartAction += onDragStart;
    }

    public void Update()
    {
        mouseDrag();
    }

    private RaycastHit castHit()
    {
        Vector2 mouse_pos = input_controller.getMousePosition();

        Vector3 mouse_position_far = new Vector3(mouse_pos.x, mouse_pos.y, Camera.main.farClipPlane);
        Vector3 mouse_position_near = new Vector3(mouse_pos.x, mouse_pos.y, Camera.main.nearClipPlane);
        Vector3 mouse_world_position_far = Camera.main.ScreenToWorldPoint(mouse_position_far);
        Vector3 mouse_world_position_near = Camera.main.ScreenToWorldPoint(mouse_position_near);

        RaycastHit hit;

        Physics.Raycast(mouse_world_position_near, mouse_world_position_far - mouse_world_position_near, out hit);

        return hit;
    }

    public void mouseDrag()
    {
        if (selected_object != null)
        {
            updateSelectedObjectPosition(0.25f);
        }
    }

    private void updateSelectedObjectPosition(float origin_offset)
    {
        Vector2 mouse_pos = input_controller.getMousePosition();

        Vector3 position = new Vector3(mouse_pos.x, mouse_pos.y, Camera.main.WorldToScreenPoint(selected_object.transform.position).z);
        Vector3 world_position = Camera.main.ScreenToWorldPoint(position);
        selected_object.transform.position = new Vector3(world_position.x, y_offset + origin_offset, world_position.z);
    }

    private void selectObject()
    {
        RaycastHit hit = castHit();
        if (hit.collider != null)
        {
            if (!hit.collider.CompareTag("Dragable"))
            {
                Debug.Log(hit.collider.tag);
                return;
            }

            selected_object = hit.collider.gameObject;
            y_offset = selected_object.transform.position.y;
            //Cursor.visible = false;
        }
    }

    private void onDragStart(object sender, System.EventArgs e)
    {
        if (selected_object == null)
        {
            selectObject();
        }
        else
        {
            updateSelectedObjectPosition(0);

            selected_object = null;
            //Cursor.visible = true;
        }
    }
}
