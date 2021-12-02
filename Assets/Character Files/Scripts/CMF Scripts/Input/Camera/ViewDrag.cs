using UnityEngine;

public class ViewDrag : MonoBehaviour
{
    public float speed = 3.5f;
    private float X;
    private float Y;

    // void Update()
    // {
    //     if (Input.touchCount > 0)
    //     {
    //         X = Input.touches[0].deltaPosition.x;
    //         Y = Input.touches[0].deltaPosition.y;
    //     }

    //     if (Input.GetMouseButton(1))
    //     {
    //         transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
    //         X = transform.rotation.eulerAngles.x;
    //         Y = transform.rotation.eulerAngles.y;
    //         transform.rotation = Quaternion.Euler(X, Y, 0);
    //     }
    // }
    // void Update()
    // {
    //     if (Input.touchCount > 0 &&
    //     Input.GetTouch(0).phase == TouchPhase.Moved)
    //     {
    //         // Get movement of the finger since last frame
    //         Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

    //         // Move object across XY plane
    //         transform.Translate(-touchDeltaPosition.x * speed,
    //                     -touchDeltaPosition.y * speed, 0);
    //     }
    // }

    // Vector3 FirstPoint;
    // Vector3 SecondPoint;
    // float xAngle;
    // float yAngle;
    // float xAngleTemp;
    // float yAngleTemp;

    // void Start()
    // {
    //     xAngle = 0;
    //     yAngle = 0;
    //     this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    // }

    // void Update()
    // {
    //     if (Input.touchCount == 3)
    //     {
    //         if (Input.GetTouch(2).phase == TouchPhase.Began)
    //         {
    //             FirstPoint = Input.GetTouch(2).position;
    //             xAngleTemp = xAngle;
    //             yAngleTemp = yAngle;
    //         }
    //         if (Input.GetTouch(2).phase == TouchPhase.Moved)
    //         {
    //             SecondPoint = Input.GetTouch(2).position;
    //             xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
    //             yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
    //             this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    //         }
    //     }
    //     else if (Input.touchCount == 2)
    //     {
    //         if (Input.GetTouch(1).phase == TouchPhase.Began)
    //         {
    //             FirstPoint = Input.GetTouch(1).position;
    //             xAngleTemp = xAngle;
    //             yAngleTemp = yAngle;
    //         }
    //         if (Input.GetTouch(1).phase == TouchPhase.Moved)
    //         {
    //             SecondPoint = Input.GetTouch(1).position;
    //             xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
    //             yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
    //             this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    //         }
    //     }

    //     else if (Input.touchCount == 1)
    //     {
    //         if (Input.GetTouch(0).phase == TouchPhase.Began)
    //         {
    //             FirstPoint = Input.GetTouch(0).position;
    //             xAngleTemp = xAngle;
    //             yAngleTemp = yAngle;
    //         }
    //         if (Input.GetTouch(0).phase == TouchPhase.Moved)
    //         {
    //             SecondPoint = Input.GetTouch(0).position;
    //             xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
    //             yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
    //             this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    //         }
    //     }
    // }

    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;
    public float ymax;
    public float ymin;



    void Start()
    {


        xAngle = 0;
        yAngle = 0;


    }

    void Update()
    {

        if (Input.touchCount > 0)
        {
            bool yclamp = false;
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                FirstPoint = Input.GetTouch(0).position;
                xAngleTemp = xAngle;
                if (!yclamp)
                    yAngleTemp = yAngle;
            }
            if (yAngle > ymax && yAngle - yAngleTemp > 0 || yAngle < ymin && yAngle - yAngleTemp < 0)
            {
                yclamp = true;

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    // yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                    this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                }
            }
            else
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                    this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                    yclamp = false;
                }


                Debug.Log(yAngle);
            }
        }

    }
}