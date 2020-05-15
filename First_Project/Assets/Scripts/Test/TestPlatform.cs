using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlatform : MonoBehaviour
{
    [SerializeField] private GameObject m_BasePlatform;
    [SerializeField] private GameObject m_DonePlatform;

    private void Start()
    {
        m_BasePlatform.SetActive(true);
        m_DonePlatform.SetActive(false);
    }
    public void SetUpDone()
    {
        m_BasePlatform.SetActive(false);
        m_DonePlatform.SetActive(true);
    }

}
