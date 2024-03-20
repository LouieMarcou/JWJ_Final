using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouched = UnityEngine.InputSystem.EnhancedTouch;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private GameObject JesusObject;

    [SerializeField] private ARRaycastManager aRRaycastManager;
    [SerializeField] private ARPlaneManager aRPlaneManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [SerializeField] private JesusSpawner m_JesusSpawner;
    private GameObject m_jesusGameObject;
    private bool m_IsMovable;
    //private bool m_IsDragging = false;

    private GameObject[] planes;

    private void OnEnable()
    {
        EnhancedTouched.EnhancedTouchSupport.Enable();
        EnhancedTouched.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouched.EnhancedTouchSupport.Disable();
        EnhancedTouched.Touch.onFingerDown -= FingerDown;
        ClearPlanes();
    }

    void FingerDown(EnhancedTouched.Finger finger)
    {
        if(finger.index != 0 && m_IsMovable == false)
        {
            return;
        }
        if(aRRaycastManager.Raycast(finger.currentTouch.screenPosition,hits,TrackableType.PlaneWithinPolygon))
        {
            foreach(ARRaycastHit hit in hits)
            {
                var arPlane = hit.trackable as ARPlane;
                m_JesusSpawner.TrySpawnObject(hit.pose.position, arPlane.normal);
                m_jesusGameObject = m_JesusSpawner.transform.GetChild(0).gameObject;

                aRPlaneManager.enabled = false;
                
            }
            ClearPlanes();
        }
    }

    public void SetIsMoveable()
    {
        if(m_IsMovable == false)
        {
            m_IsMovable = true;
            m_jesusGameObject.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = true;
            m_jesusGameObject.GetComponent<Lean.Touch.LeanPinchScale>().enabled = true;
            m_jesusGameObject.GetComponent<Lean.Touch.LeanTwistRotateAxis>().enabled = true;
        }
        else if(m_IsMovable == true)
        {
            m_IsMovable = false;
            m_jesusGameObject.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = false;
            m_jesusGameObject.GetComponent<Lean.Touch.LeanPinchScale>().enabled = false;
            m_jesusGameObject.GetComponent<Lean.Touch.LeanTwistRotateAxis>().enabled = false;
        }
    }

    private void ClearPlanes()
    {
        hits.Clear();
        planes = GameObject.FindGameObjectsWithTag("ARPlane");
        foreach (var plane in planes)
        {
            Destroy(plane.gameObject);
        }
    }
}
