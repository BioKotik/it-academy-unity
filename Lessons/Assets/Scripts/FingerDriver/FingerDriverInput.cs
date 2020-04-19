using UnityEngine;


public class FingerDriverInput : MonoBehaviour
{
    [SerializeField] private Transform m_SteerWheelTransform;

    [SerializeField] [Range(0f, 100f)] private float m_MaxSteerAngle = 90f;

    [SerializeField] [Range(0f, 1f)] private float m_SteerAcceleration = 0.25f;

    private float steerAxis;

    public float SteerAxis
    {
        get { return steerAxis;}

        set { steerAxis = Mathf.Lerp(steerAxis, value, m_SteerAcceleration); }
    }

    private Vector2 startSteerWheelPoint;
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
        //Запоминаем координату руля в экранной системе координат
        startSteerWheelPoint = 
            mainCamera.WorldToScreenPoint(m_SteerWheelTransform.position);
        
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //Меряем угол между рулем и точкой касания экрана
            Vector2 dir = (Vector2) Input.mousePosition - startSteerWheelPoint;
            float angle = Vector2.Angle(Vector2.up, dir);

            angle /= m_MaxSteerAngle;
            angle = Mathf.Clamp01(angle);
            if (Input.mousePosition.x > startSteerWheelPoint.x)
                angle *= -1f;

            SteerAxis = angle;
        }
        else
        {
            SteerAxis = 0;
        }
        
        m_SteerWheelTransform.localEulerAngles =
            new Vector3(0f,0f,SteerAxis * m_MaxSteerAngle);

        Vector3 pos = mainCamera.ScreenToWorldPoint(startSteerWheelPoint);
        pos.z = -3;

        m_SteerWheelTransform.position = pos;
    }
}