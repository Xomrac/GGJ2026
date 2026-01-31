using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public bool hasObject = false;
    public GameObject heldObject;
    public Vector3 force;
    public void throwObject() 
    {
        hasObject = false;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject.transform.parent = null; 
        heldObject.GetComponent<Rigidbody>().AddForce(force,ForceMode.Impulse);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            throwObject();
        }
    }
}
