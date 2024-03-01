using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Internal;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using Unity.VisualScripting;

public class JesusSpawnTrigger : MonoBehaviour
{
    /// <summary>
    /// The <see cref="IARInteractor"/> that determines where to spawn the object.
    /// </summary>
    [SerializeField]
    Object m_ARInteractorObject;

    public Object arInteractorObject
    {
        get => m_ARInteractorObject;
        set => m_ARInteractorObject = value;
    }

    
    [SerializeField]
    JesusSpawner m_JesusSpawner;
    /// <summary>
    /// The behavior to use to spawn objects.
    /// </summary>
    public JesusSpawner jesusSpawner
    {
        get => m_JesusSpawner;
        set => m_JesusSpawner = value;
    }

    IARInteractor m_ARInteractor;
    XRBaseControllerInteractor m_ARInteractorAsControllerInteractor;
    bool m_EverHadSelection;


    [SerializeField]
    GameObject debugTestObject;

    void Start()
    {
        if(m_JesusSpawner == null)
        {
            m_JesusSpawner = FindObjectOfType<JesusSpawner>();
        }
        m_ARInteractor = m_ARInteractorObject as IARInteractor;
        m_ARInteractorAsControllerInteractor = m_ARInteractorObject as XRBaseControllerInteractor;
    }

    private void Update()
    {
        //var attemptSpawn = false;

        //var currentControllerState = m_ARInteractorAsControllerInteractor.xrController.currentControllerState;
        //if (currentControllerState.selectInteractionState.activatedThisFrame)
        //    m_EverHadSelection = m_ARInteractorAsControllerInteractor.hasSelection;
        //else if (currentControllerState.selectInteractionState.active)
        //    m_EverHadSelection |= m_ARInteractorAsControllerInteractor.hasSelection;
        //else if(currentControllerState.selectInteractionState.deactivatedThisFrame)
        //    attemptSpawn = !m_ARInteractorAsControllerInteractor.hasSelection && !m_EverHadSelection;

        //if(attemptSpawn) 
        //{ 
        //    debugTestObject.SetActive(true);
        //}

        if(m_ARInteractor.TryGetCurrentARRaycastHit(out var arRaycastHit))
        {
            debugTestObject.SetActive(true);
            var arPlane = arRaycastHit.trackable as ARPlane;
            if (arPlane == null)
            {
                
                return;
            }
                
            
            m_JesusSpawner.TrySpawnObject(arRaycastHit.pose.position, arPlane.normal);
        }
        else
            debugTestObject.SetActive(true);
    }
}
