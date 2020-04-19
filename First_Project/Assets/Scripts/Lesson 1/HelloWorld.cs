using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelloWorld : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World!");
        txt.text = "Hello World!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
