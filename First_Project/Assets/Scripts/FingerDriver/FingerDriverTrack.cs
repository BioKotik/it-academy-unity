using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FingerDriverTrack : MonoBehaviour
{
    public class TrackSegment
    {
        public Vector3[] Points = new Vector3[3];

        public bool IsPointInSegment(Vector3 point)
        {
            return MathfTriangles.IsPointInTriangleXY(
                point, Points[0], Points[1], Points[2]);
        }
    }

    [SerializeField] private LineRenderer m_LineRenderer;
    [SerializeField] private bool m_ViewDebug;

    private Vector3[] corners;
    public int scores;
    private TrackSegment[] segments;
    private Dictionary<TrackSegment, bool> _segments = new Dictionary<TrackSegment, bool>();
    
    // Start is called before the first frame update
    void Start()
    {
        scores = 0;
        //Заполняем массив опорных точек трассы
        corners = new Vector3[transform.childCount];

        for (int i = 0; i < corners.Length; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            corners[i] = obj.transform.position;
            obj.GetComponent<MeshRenderer>().enabled = false;
        }
        
        //Настраиваем LineRenderer
        m_LineRenderer.positionCount = corners.Length;
        m_LineRenderer.SetPositions(corners);
        
        //Запекаем Mesh
        Mesh mesh = new Mesh();
        m_LineRenderer.BakeMesh(mesh, true);
        
        //Создаем массив сегментов трассы
        //Каждый треугольник описан 3-мя вершинами из массива вершин
        segments = new TrackSegment[mesh.triangles.Length / 3];
        
        int segmentsCounter = 0;
        for (int i = 0; i < mesh.triangles.Length; i+=3)
        {
            segments[segmentsCounter] = new TrackSegment();
            segments[segmentsCounter].Points = new Vector3[3];
            segments[segmentsCounter].Points[0] =
                mesh.vertices[mesh.triangles[i]];
            
            segments[segmentsCounter].Points[1] =
                mesh.vertices[mesh.triangles[i + 1]];
            
            segments[segmentsCounter].Points[2] =
                mesh.vertices[mesh.triangles[i + 2]];

            segmentsCounter++;
        }
        for (int i = 0; i < segments.Length; i++)
        {
            _segments.Add(segments[i], false);
        }

        if (!m_ViewDebug)
        {
            return;
        }

        foreach (var segment in segments)
        {
            foreach (var point in segment.Points)
            {
                GameObject sphere = 
                    GameObject.CreatePrimitive(PrimitiveType.Sphere);

                sphere.transform.position = point;
                sphere.transform.localScale = Vector3.one * 0.1f;
            }
        }

    }

    /// <summary>
    ///     Определяем находится ли точка в пределах трассы
    /// </summary>
    /// <param name="point">Точка</param>
    /// <returns></returns>
    public bool IsPointInTrack(Vector3 point)
    {      
        for (int i = 0; i < _segments.Count; i++)
        {
            if (!_segments.ElementAt(i).Value && _segments.ElementAt(i).Key.IsPointInSegment(point))
            {
                _segments[segments.ElementAt(i)] = true;
                scores++;
                return true;
            }
            else if(_segments.ElementAt(i).Key.IsPointInSegment(point))
            {
                return true;
            }
        }

        return false;
    }
}
