using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;

    Vector3 c_PositionStart;
    Vector3 p_Position;

    Vector3 m_Position;

    float screenHeight;
    float screenWidth;

    float cameraSpeed = 2;

    bool isLocked = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        c_PositionStart = new Vector3(0, 18, -7);
        player = GameObject.Find("Player");
        screenHeight = Display.main.systemHeight;
        screenWidth = Display.main.systemWidth;
        // TODO: updatear las dimensiones si se resizea la ventana
    }

    // Update is called once per frame
    void Update()
    {
        m_Position = Input.mousePosition;
        GetPlayerPosition();
        CenterCamera();
        CameraUpdatePositionOnMouse();
    }

    void CenterCamera()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space))
        {
            isLocked = true;
            transform.position = p_Position + c_PositionStart;
            return;
        }
        else
        {
            isLocked = false;
        }
    }


    void GetPlayerPosition()
    {
        p_Position = player.transform.position;
    }

    Vector2 MouseToCameraRatio()
    {
        Vector2 mcr;
        float horizontalPercent = m_Position.x / screenWidth;
        float verticalPercent = m_Position.y / screenHeight;
        mcr = new Vector2(horizontalPercent, verticalPercent);
        return mcr;
    }

    void CameraUpdatePositionOnMouse()
    {
        if (isLocked) return;
        Vector2 direction = new Vector2(0,0);
        Vector2 mcr = MouseToCameraRatio();
        if(mcr.x >= 0.9f)
        {
            direction.x = 1.0f;
        }
        else if(mcr.x <= 0.1f)
        {
            direction.x = -1.0f;
        }

        if(mcr.y >= 0.9f)
        {
            direction.y = 1.0f;
        }
        else if(mcr.y <= 0.1f)
        {
            direction.y = -1.0f;
        }

        transform.Translate(direction.x * Time.fixedDeltaTime * cameraSpeed, direction.y * Time.fixedDeltaTime * cameraSpeed, 0);
    }
}
