using System;
using DefaultNamespace;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public bool hasObject = false;
    public DeliverableItem heldObject;
    [SerializeField] private Transform _objectHoldPoint;
    [SerializeField] private LayerMask _nonInteractableLayer;
    [SerializeField] private LayerMask _interactableLayer;

    
    private PlayerSFX _playerSFX;
    private Camera _camera;


    public void ForceRemoval()
    {
        hasObject = false;
        heldObject = null;
        if(_objectHoldPoint.transform.childCount > 0)
        {
            foreach(Transform child in _objectHoldPoint.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void Awake()
    {
        _camera = Camera.main;
        _playerSFX = GetComponent<PlayerSFX>();
    }
    
    public void RemoveItem()
    {
        if (!hasObject || heldObject == null) return;
        
        heldObject = null;
        hasObject = false;
    }

    public void GrabObject(DeliverableItem obj)
    {
        if (hasObject && heldObject != null)
        {
            throwObject();
        }
        hasObject = true;
        heldObject = obj;
        heldObject.gameObject.layer = Utils.LayerMaskToLayer(_nonInteractableLayer);
        heldObject.Rigidbody.isKinematic = true;
        heldObject.transform.SetParent(_objectHoldPoint);
        heldObject.transform.localPosition = Vector3.zero;
    }

    private void throwObject() 
    {
        if (!hasObject || heldObject == null) return;
        hasObject = false;
        heldObject.gameObject.layer = Utils.LayerMaskToLayer(_interactableLayer);
        heldObject.Rigidbody.isKinematic = false;
        heldObject.transform.SetParent(null);
        // launch the object forward
        heldObject.Rigidbody.AddForce(_camera.transform.forward * 500f);
        heldObject = null;

        _playerSFX.PlayThrowSFX();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            throwObject();
        }
    }
}
