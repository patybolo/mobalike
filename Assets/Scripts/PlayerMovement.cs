using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Vector3 p_DirInput_h;
    Vector3 p_DirInput_v;
    Vector3 p_MovDir;

    public Camera cam;

    Rigidbody rb;
    public LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        UpdateVision();
        movement();
    }

    void movement()
    {
        p_DirInput_h = new Vector3(Input.GetAxis("Horizontal") * 3 * Time.fixedDeltaTime, 0, 0);
        p_DirInput_v = new Vector3(0, 0, Input.GetAxis("Vertical") * 3 * Time.fixedDeltaTime);
        transform.position += Vector3.ClampMagnitude(p_DirInput_h += p_DirInput_v, 1f);
    }

    void UpdateVision()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f, groundLayer))
        {
            Vector3 targetPosition = hitInfo.point;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;

            if(direction !=  Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
            }
        }

    }
}
