using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject spawnedObject;
    public Transform player;
    public ObjectManager ObjectManager;
    private void Start()
    {
        ObjectManager = FindAnyObjectByType<ObjectManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&ObjectManager.hasObject==false)
        {
            SpawnObject();
            ObjectManager.hasObject = true;
        }
        if (ObjectManager.hasObject == false)
        {
            Debug.Log("Your hands are full!");

        }
    }

    private void SpawnObject()
    {
        // Implementation for spawning an object
        var obj= Instantiate(spawnedObject, Vector3.zero, Quaternion.identity,player);
        obj.GetComponent<Transform>().localPosition = Vector3.zero;
        ObjectManager.heldObject = obj;
        Debug.Log("Object spawned!");
        
    }   
}
