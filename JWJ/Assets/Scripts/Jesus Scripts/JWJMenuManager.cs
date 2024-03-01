using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.ARStarterAssets;

public class JWJMenuManager : MonoBehaviour
{
    [SerializeField]
    JesusSpawner m_JesusSpawner;

    /// <summary>
    /// The jesus spawner component in charge of spawning Jesus
    /// </summary>
    public JesusSpawner jesusSpawner
    {
        get => m_JesusSpawner;
        set => m_JesusSpawner = (value);
    }

    [SerializeField]
    XRScreenSpaceController m_ScreenSpaceController;

    /// <summary>
    /// The screen space controller associated with the demo scene
    /// </summary>
    public XRScreenSpaceController screenSpaceController
    {
        get => m_ScreenSpaceController;
        set => m_ScreenSpaceController = value;
    }

    [SerializeField]
    XRInteractionGroup m_InteractionGroup;

    /// <summary>
    /// The interaction group for the scene
    /// </summary>
    public XRInteractionGroup interactionGroup
    {
        get => m_InteractionGroup;
        set => m_InteractionGroup = value;
    }

    [SerializeField]
    [Tooltip("The slider for activating plane debug visuals.")]
    DebugSlider m_DebugPlaneSlider;

    /// <summary>
    /// The slider for activating plane debug visuals.
    /// </summary>
    public DebugSlider debugPlaneSlider
    {
        get => m_DebugPlaneSlider;
        set => m_DebugPlaneSlider = value;
    }

    [SerializeField]
    [Tooltip("The plane prefab with shadows and debug visuals.")]
    GameObject m_DebugPlane;

    /// <summary>
    /// The plane prefab with shadows and debug visuals.
    /// </summary>
    public GameObject debugPlane
    {
        get => m_DebugPlane;
        set => m_DebugPlane = value;
    }

    [SerializeField]
    [Tooltip("The plane manager in the AR demo scene.")]
    ARPlaneManager m_PlaneManager;

    /// <summary>
    /// The plane manager in the AR demo scene.
    /// </summary>
    public ARPlaneManager planeManager
    {
        get => m_PlaneManager;
        set => m_PlaneManager = value;
    }

    bool m_IsPointerOverUI;

    readonly List<ARFeatheredPlaneMeshVisualizerCompanion> featheredPlaneMeshVisualizerCompanions = new List<ARFeatheredPlaneMeshVisualizerCompanion>();

    [SerializeField] private GameObject obj;

    private void OnEnable()
    {
        m_ScreenSpaceController.dragCurrentPositionAction.action.started += HideTapOutsideUI;
        m_ScreenSpaceController.tapStartPositionAction.action.started += HideTapOutsideUI;

        m_PlaneManager.planesChanged += OnPlaneChanged;
    }

    private void OnDisable()
    {
        m_ScreenSpaceController.dragCurrentPositionAction.action.started -= HideTapOutsideUI;
        m_ScreenSpaceController.tapStartPositionAction.action.started -= HideTapOutsideUI;

        m_PlaneManager.planesChanged -= OnPlaneChanged;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_PlaneManager.planePrefab = m_DebugPlane;
        ChangePlaneVisibility(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearObject()
    {
        foreach(Transform child in m_JesusSpawner.transform)
        {
            Destroy(child.gameObject);
        }
    }


    void ChangePlaneVisibility(bool setVisible)
    {
        var count = featheredPlaneMeshVisualizerCompanions.Count;
        for (int i = 0; i < count; ++i)
        {
            featheredPlaneMeshVisualizerCompanions[i].visualizeSurfaces = setVisible;
        }
    }


    void HideTapOutsideUI(InputAction.CallbackContext context)
    {
        obj.SetActive(true);
        if(!m_IsPointerOverUI)
        {
            
        }
    }




    void OnPlaneChanged(ARPlanesChangedEventArgs eventArgs) 
    {
        if(eventArgs.added.Count > 0)
        {
            foreach(var plane in eventArgs.added)
            {
                if (plane.TryGetComponent<ARFeatheredPlaneMeshVisualizerCompanion>(out var visualizer))
                {
                    featheredPlaneMeshVisualizerCompanions.Add(visualizer);
                    visualizer.visualizeSurfaces = (m_DebugPlaneSlider.value != 0);
                }
            }
        }

        if (eventArgs.removed.Count > 0)
        {
            foreach (var plane in eventArgs.removed)
            {
                if (plane.TryGetComponent<ARFeatheredPlaneMeshVisualizerCompanion>(out var visualizer))
                    featheredPlaneMeshVisualizerCompanions.Remove(visualizer);
            }
        }

        // Fallback if the counts do not match after an update
        if (m_PlaneManager.trackables.count != featheredPlaneMeshVisualizerCompanions.Count)
        {
            featheredPlaneMeshVisualizerCompanions.Clear();
            foreach (var trackable in m_PlaneManager.trackables)
            {
                if (trackable.TryGetComponent<ARFeatheredPlaneMeshVisualizerCompanion>(out var visualizer))
                {
                    featheredPlaneMeshVisualizerCompanions.Add(visualizer);
                    visualizer.visualizeSurfaces = (m_DebugPlaneSlider.value != 0);
                }
            }
        }
    }
}
