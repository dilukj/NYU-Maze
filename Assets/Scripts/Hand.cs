using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public enum HandType
{
    Left,
    Right
}

public class Hand : MonoBehaviour
{
    public HandType type = HandType.Left;
    public bool isHidden { get; private set; }

    public InputAction trackedAction = null;
    bool m_isCurrentlyTracked = false;

    List<SkinnedMeshRenderer> m_currentRenderers = new List<SkinnedMeshRenderer>();

    Collider[] m_colliders = null;
    public bool isCollisionEnabled { get; private set; }

    public XRBaseInteractor interactor = null;

    private void Awake()
    {
       if (interactor == null)
        {
            interactor = GetComponentInParent<XRBaseInteractor>();
        }
    }

    private void OnEnable()
    {
        interactor.selectEntered.AddListener(OnGrab2);
        interactor.selectExited.AddListener(OnRelease2);
        //interactor.onSelectEntered.AddListener(OnGrab);
        //interactor.onSelectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        interactor.selectEntered.RemoveListener(OnGrab2);
        interactor.selectExited.RemoveListener(OnRelease2);
        //interactor.onSelectEntered.RemoveListener(OnGrab);
        //interactor.onSelectExited.RemoveListener(OnRelease);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_colliders = GetComponentsInChildren<Collider>().Where(childCollider => !childCollider.isTrigger).ToArray();
        trackedAction.Enable();
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        float isTracked = trackedAction.ReadValue<float>();
        if(isTracked == 1.0f && !m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = true;
            Show();
        } 
        else if (isTracked == 0 && m_isCurrentlyTracked)
        {
            m_isCurrentlyTracked = false;
            Hide();
        }
    }

    void Show()
    {
        foreach (var renderer in m_currentRenderers)
        {
            renderer.enabled = true;
        }
        isHidden = false;
        EnableCollisions(true);
    }

    void Hide()
    {
        m_currentRenderers.Clear();
        SkinnedMeshRenderer[] renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
            m_currentRenderers.Add(renderer);
        }
        isHidden = true;
        EnableCollisions(false);
    }

    public void EnableCollisions(bool enabled)
    {
        if (isCollisionEnabled == enabled)
            return;

        isCollisionEnabled = enabled;
        foreach (var collider in m_colliders)
        {
            collider.enabled = true;
        }
    }

    //void OnGrab(XRBaseInteractable grabbedObject)
    //{
    //    HandControl ctrl = grabbedObject.GetComponent<HandControl>();
    //    if(ctrl != null && ctrl.hideHand)
    //    {
    //        Hide();
    //    }

    //}

    //void OnRelease(XRBaseInteractable releasedObject)
    //{
    //    HandControl ctrl = releasedObject.GetComponent<HandControl>();
    //    if (ctrl != null && ctrl.hideHand)
    //    {
    //        Show();
    //    }
    //}

    void OnGrab2(SelectEnterEventArgs args)
    {
        // Debug.Log("Grabbed");
        HandControl ctrl = args.interactableObject.colliders[0].gameObject.GetComponentInParent<HandControl>();
        if (ctrl != null && ctrl.hideHand)
        {
            Hide();
        }

    }

    void OnRelease2(SelectExitEventArgs args)
    { 
        // Debug.Log("Released");
        HandControl ctrl = args.interactableObject.colliders[0].gameObject.GetComponentInParent<HandControl>();
        if (ctrl != null && ctrl.hideHand)
        {
            Show();
        }
    }
}
