using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateObjects : MonoBehaviour
{
    [SerializeField] private GameObject m_BaseCube;

    [SerializeField] private GameObject[] m_Cubes;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (var cube in m_Cubes)
        {
            GameObject newCube = Instantiate(m_BaseCube);
            Vector3 pos = cube.transform.position;
            pos.y += 1;
            newCube.transform.position = pos;

            newCube.name = cube.name + "_up";
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_BaseCube.transform.Translate(Vector3.up * Time.deltaTime);
    }
}
