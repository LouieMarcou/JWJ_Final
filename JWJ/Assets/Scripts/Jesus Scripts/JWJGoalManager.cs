using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JWJGoalManager : MonoBehaviour
{
    [SerializeField]
    JesusSpawner m_JesusSpawner;

    /// <summary>
    /// Jesus Spawner used to detect whether the spawning goal has been achieved
    /// </summary>
    
    public JesusSpawner jesusSpawner
    {
        get => m_JesusSpawner;
        set => m_JesusSpawner = value;
    }


    [SerializeField]
    JWJMenuManager m_JWJMenuManager;


    public JWJMenuManager jWJMenuManager
    {
        get => m_JWJMenuManager;
        set => m_JWJMenuManager = value;
    }

    [SerializeField]
    GameObject debugTestObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
        //{
        //    //debugTestObject.SetActive(true);
        //}
    }
}
