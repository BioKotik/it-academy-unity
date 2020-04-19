using UnityEngine;
using UnityEngine.SceneManagement;

public class FingerDriverPlayer : MonoBehaviour
{
     [SerializeField] private FingerDriverTrack m_Track;
     [SerializeField] private FingerDriverInput m_Input;
     //Точка по которой будет проверяться нахождение на трасе
     //Вынесена в нос автомобиля
     [SerializeField] private Transform m_TrackPoint;
     [SerializeField] private float m_CarSpeed = 2f;
     [SerializeField] private float m_MaxSteer = 90f;

     private void Update()
     {
          if (m_Track.IsPointInTrack(m_TrackPoint.position))
          {
               transform.Translate(transform.up * Time.deltaTime * m_CarSpeed, Space.World);
               transform.Rotate(0f,0f,m_MaxSteer * m_Input.SteerAxis * Time.deltaTime);
          }
          else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
     }
     
     
}