using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Utilities;

public class JesusSpawner : MonoBehaviour
{
    [SerializeField] Camera m_CameraToFace;

    /// <summary>
    /// The camera that objects will face when spawned. If not set, defaults to the <see cref="Camera.main"/> camera.
    /// </summary>
    public Camera cameraToFace
    {
        get
        {
            EnsureFacingCamera();
            return m_CameraToFace ;
        }

        set => m_CameraToFace = value;
    }

    [SerializeField] GameObject m_JesusObject;
    /// <summary>
    /// The object to spawn
    /// </summary>
    public GameObject JesusObject
    {
        get => m_JesusObject;
        set => m_JesusObject = value;
    }

    bool m_OnlySpawnInView = true;
    /// <summary>
    /// Whether to only spawn an object if the spawn point is within view of the <see cref="cameraToFace"/>.
    /// </summary>
    public bool onlySpawnInView
    {
        get => m_OnlySpawnInView;
        set => m_OnlySpawnInView = value;
    }

    float m_ViewportPeriphery = 0.15f;
    /// <summary>
    /// The size, in viewport units, of the periphery inside the viewport that will not be considered in view.
    /// </summary>
    public float viewportPeriphery
    {
        get => m_ViewportPeriphery; 
        set => m_ViewportPeriphery = value;
    }

    public event Action<GameObject> objectSpawned;
    private bool hasSpawned;

    [SerializeField] private GameObject mainPanel;

    [SerializeField] private GameObject explantionText;

    [SerializeField] private AudioManager audioManager;


    private void Awake()
    {
        EnsureFacingCamera() ;
    }

    void EnsureFacingCamera()
    {
        if(m_CameraToFace == null)
            m_CameraToFace = Camera.main;
    }



    public bool TrySpawnObject(Vector3 spawnPoint, Vector3 spawnNormal)
    {
        if(hasSpawned) return false;
        //if(m_OnlySpawnInView)
        //{
        //    var viewMin = m_ViewportPeriphery;
        //    var viewMax = 1f - m_ViewportPeriphery;
        //    var pointInViewportSpace = cameraToFace.WorldToScreenPoint(spawnPoint);
        //    if(pointInViewportSpace.z < 0f || pointInViewportSpace.x > viewMax || pointInViewportSpace.x < viewMin ||
        //        pointInViewportSpace.y > viewMax || pointInViewportSpace.y < viewMin)
        //    {
        //        return false;
        //    }
        //}

        var newObject = Instantiate(m_JesusObject);
        newObject.transform.parent = transform;

        newObject.transform.position = spawnPoint;
        EnsureFacingCamera();

        
        var facePosition = m_CameraToFace.transform.position;
        var forward = facePosition - spawnPoint;
        BurstMathUtility.ProjectOnPlane(forward, spawnNormal, out var projectedForward);
        newObject.transform.rotation = Quaternion.LookRotation(projectedForward, spawnNormal);

        newObject.transform.eulerAngles -= new Vector3(0f, 130f, 0f);
        audioManager.SetJesus(newObject);

        objectSpawned?.Invoke(newObject);
        hasSpawned = true;
        explantionText.SetActive(false);
        mainPanel.SetActive(true);
        return true;
    }
    public void ClearObejcts()
    {
        Destroy(transform.GetChild(0));
    }
}
