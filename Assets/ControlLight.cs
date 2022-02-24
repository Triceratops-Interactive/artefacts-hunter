using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class ControlLight : MonoBehaviour
{
    Color color_light = new Color(255, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLightRedToWhite();
    }

    private void ChangeLightRedToWhite()
    {
        float ratio = calcDistanceToPharao();


        color_light.g = 255 * ratio;
        color_light.b = 255 * ratio;

        Light2D light = gameObject.GetComponent(typeof(Light2D)) as Light2D;
        //light.color = color_light;
        light.color = new Color(1,ratio,ratio,1);
    }

    private float calcDistanceToPharao()
    {
        Transform transform_parent = gameObject.GetComponentInParent(typeof(Transform)) as Transform;

        //position of right upper corner
        Vector2 max_distance = new Vector2(48, 60);
        //float max_x = 48;
        //float max_y = 60;

        //postion of pharao
        Vector2 min_distance = new Vector2(-51, 24);
        //float min_x = -50;
        //float min_y = 24;


        float x_ges = Mathf.Abs(max_distance.x) + Mathf.Abs(min_distance.x);
        float x_ratio =  1-(Mathf.Abs(min_distance.x - transform_parent.position.x)/x_ges);

        float y_ges = Mathf.Abs(max_distance.y) + Mathf.Abs(min_distance.y);
        //float y_ratio = 1-(transform_parent.position.y / max_distance.y);
        float y_ratio = 1 - (Mathf.Abs(min_distance.y- transform_parent.position.y) / max_distance.y);
        float ratio_avg = (x_ratio + y_ratio) / 2;


        return ratio_avg;
    }
}
