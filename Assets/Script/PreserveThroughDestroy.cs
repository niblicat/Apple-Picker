using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveThroughDestroy : MonoBehaviour {
    private static Dictionary<string, GameObject> instances = new Dictionary<string, GameObject>();
    [SerializeField] private string ID;

    void Awake() {
        if (instances.ContainsKey(ID)) {
            var existing = instances[ID];

            // A null result indicates the other object was destroyed for some reason
            if (existing != null) {
                if (ReferenceEquals(gameObject, existing))
                    return;
                Invoke(nameof(PrepareDestroy), 0.05f);
            }
            else {
                instances[ID] = gameObject;
                DontDestroyOnLoad(gameObject);
            }
        }
        // The following code registers this GameObject regardless of whether it's new or replacing
        Invoke(nameof(PrepareImmunity), 0.05f);
    }

    void PrepareDestroy() {
        Destroy(instances[ID]);
        
    }
    void PrepareImmunity() {
        instances[ID] = gameObject;
        DontDestroyOnLoad(gameObject);
    }
}
