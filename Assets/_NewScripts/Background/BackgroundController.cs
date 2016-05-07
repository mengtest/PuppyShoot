using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private GameObject main_camera;
    
    public static float WIDTH = 19.2f;

    public static int BG_NUM = 4;

    void Update()
    {
        float total_width = WIDTH*BG_NUM;
        Vector3 bg_position = this.transform.position;

        Vector3 camera_position = this.main_camera.transform.position;
        if (bg_position.x + total_width*0.5f < camera_position.x)
        {
            bg_position.x += total_width;
            this.transform.position = bg_position;
        }

        if (camera_position.x < bg_position.x - total_width*0.5f)
        {
            bg_position.x -= total_width;
            this.transform.position = bg_position;
        }
    }
}
