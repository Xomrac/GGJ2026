using System;
using DefaultNamespace;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public bool hasObject = false;
    public DeliverableItem heldObject;
    [SerializeField] private Transform _objectHoldPoint;
    
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    public void RemoveItem()
    {
        heldObject = null;
    }

    public void GrabObject(DeliverableItem obj)
    {
        if (hasObject && heldObject != null)
        {
            throwObject();
        }
        hasObject = true;
        heldObject = obj;
        heldObject.Rigidbody.isKinematic = true;
        heldObject.transform.SetParent(_objectHoldPoint);
        heldObject.transform.localPosition = Vector3.zero;
    }

    private void throwObject() 
    {
        if (!hasObject || heldObject == null) return;
        hasObject = false;
        heldObject.Rigidbody.isKinematic = false;
        heldObject.transform.SetParent(null);
        // launch the object forward
        heldObject.Rigidbody.AddForce(_camera.transform.forward * 500f);
        heldObject = null;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            throwObject();
        }
    }
}
