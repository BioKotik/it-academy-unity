using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class TrajectoryCurve : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private List<Transform> Targets;
   
    [SerializeField] private BezierCurve bezierCurve;
    
    [FormerlySerializedAs("lineRendererComponent")] public LineRenderer lineRenderer;

    [NonSerialized] public Vector3 distance;
    public GameObject BulletPrefab; 
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Camera mainCamera;
    private Keyframe currentKey;
    [SerializeField] private Vector3[] bulletPoints;
    private List<RaycastHit> _raycastHits;
    private Vector3 mouseInWorld;
    private RaycastHit raycastHit;
   
   private bool ricocheteDetected  = false;
   private bool LineDrawed  = false;
   private int visibleRicocheteCount;
   
    public Vector3 StartPosition
    {
        get => _startPosition;
        set => _startPosition = value;
    }

    public Vector3 TargetPosition
    {
        get => _targetPosition;
        set => _targetPosition = value;       
    }

    void Start()
    {
        _raycastHits = new List<RaycastHit>();
        bulletPoints = new Vector3[] { };
        //bezierCurve.lineRenderer = lineRenderer;
        mainCamera = Camera.main;
        StartPosition = Player.position;
        TargetPosition = Targets[0].position;
        distance = TargetPosition - StartPosition;
        bezierCurve.lineRenderer = lineRenderer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {      
        StartPosition = Player.position;
        TargetPosition = Targets[0].position;
        visibleRicocheteCount = 3;// число отображаемых рикошетов

        Debug.DrawLine(StartPosition, TargetPosition, Color.red);


        float enter;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        new Plane(StartPosition, TargetPosition, new Vector3(StartPosition.x,StartPosition.y, StartPosition.z + 0.01f )).Raycast(ray, out enter);
        
        if (Input.GetMouseButton(0))
        {
           
                mouseInWorld = ray.GetPoint(enter);
                bezierCurve.point1.position = mouseInWorld;
            
        }
        ShowTrajectory();
    }

    
    public void ShowTrajectory()
    {
        Vector3[] points = new Vector3[50];
        
        lineRenderer.positionCount = 0;
        
        ricocheteDetected = false;
        LineDrawed = false;
        
        for (int i = 0; i < points.Length; i++)
        {
            if (i == 0)
            {
                points[i] = StartPosition;
                AddPointOnLineRenderer(points[i]);
                continue;
            }
          
            float time = (float)i /  points.Length;
            if (!ricocheteDetected)
            {
                points[i] = bezierCurve.CalculateQuadraticBezierPOint(time, StartPosition,mouseInWorld, TargetPosition);
                AddPointOnLineRenderer(points[i]);
            }
            

            Vector3 ricocheteDirection = MakeRicochete(points[i - 1], points[i] - points[i - 1]);

           if (ricocheteDetected && !LineDrawed)
            {
                LineDrawed = true;
                points[i] = raycastHit.point;
                if (visibleRicocheteCount > 0)
                {
                    visibleRicocheteCount--;
                    GenerateAfterRicochetTrajectory(points[i], ricocheteDirection, 1f);
                }
            }
        }
 
        if (Input.GetMouseButtonUp(0))
        {
            print("Up");
            bulletPoints = points;
            GameObject bullet = Instantiate(BulletPrefab, points[0], Quaternion.identity);
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        }
    }

    private void AddPointOnLineRenderer(Vector3 point)
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, point);
    }
    
  
    private Vector3 MakeRicochete(Vector3 startPosition, Vector3 direction)
    {
        Vector3 ricocheteDirection = new Vector3();

        RaycastHit hit;
        Ray ray = new Ray(startPosition, direction);
        Physics.Raycast(ray, out hit, direction.magnitude);
        raycastHit=hit;

        if (hit.collider != null)
        {

            if (hit.collider.gameObject.CompareTag("Ricochete"))
            {
                Vector3 inNormal = hit.normal;
                ricocheteDirection = Vector3.Reflect(direction, inNormal);
                ricocheteDetected = true;
            }
        }

        return ricocheteDirection;
      
    }
    
    

 
    private List<Vector3> GenerateAfterRicochetTrajectory(Vector3 startPosition, Vector3 direction, float step)
    {
        List<Vector3> lineDots = new List<Vector3>();
        ricocheteDetected = false;
        LineDrawed = false;
        for (int i = 0; i < 50; i++)
        {
            Vector3 ricocheteDirection;
            if (i == 0)
                {
                    lineDots.Add(startPosition);
                    MakeRicochete(lineDots[i], direction);
                    AddPointOnLineRenderer(lineDots[i]);
                    continue;
                }

            if (!ricocheteDetected) 
            {
                lineDots.Add(lineDots[i - 1] + direction * step);
                AddPointOnLineRenderer(lineDots[i]);
                ricocheteDirection = MakeRicochete(lineDots[i], direction);
            }
            
            if (ricocheteDetected && !LineDrawed)
            {
                ricocheteDirection = MakeRicochete(lineDots[i], direction);
                lineDots[i] = raycastHit.point;
                LineDrawed = true;
                if(visibleRicocheteCount > 0 )
                {
                    visibleRicocheteCount--;
                    GenerateAfterRicochetTrajectory(lineDots[i], ricocheteDirection, 1f);
                }
            }
        }
        ricocheteDetected = true;
        LineDrawed = true;
        return lineDots;
    }

}
