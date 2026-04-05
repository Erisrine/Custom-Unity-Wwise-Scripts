using UnityEngine;

public class OneShotEnvironmentAwareEmitter : MonoBehaviour
{
    void Awake()
    {
        gameObject.AddComponent<AkGameObj>().isEnvironmentAware = true;
        gameObject.AddComponent<SphereCollider>().isTrigger = true;
        gameObject.AddComponent<Rigidbody>().isKinematic = true;
        gameObject.AddComponent<AkRoomAwareObject>();
        //gameObject.AddComponent<AkSpatialAudioDebugDraw>().drawDiffractionPaths = true;

        if (WwiseAudioManager.Instance.DebugEmitters)
        {
           /* GameObject sphereGO = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphereGO.GetComponent<Collider>().enabled = false;
            sphereGO.name = "DebugSphere";
            sphereGO.transform.SetParent(transform);
            sphereGO.transform.localPosition = Vector3.zero;
            sphereGO.transform.localRotation = Quaternion.identity;
            sphereGO.transform.localScale = Vector3.one * 0.2f; // small sphere*/
        }
    }

    public void PlayEnvironmentAwareEvent(AK.Wwise.Event wwiseEvent)
    {
        // Post the event with a callback
        wwiseEvent.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, Callback);
    }

    public void PlayEnvironmentAwareEvent(string eventName)
    {
        // Post the event with a callback using the string version
        AkUnitySoundEngine.PostEvent(
            eventName,
            gameObject,
            (uint)AkCallbackType.AK_EndOfEvent,
            Callback,      
            null           
        );
    }   

    private void Callback(object in_cookie, AkCallbackType in_type, object in_info)
    {
        // Destroy the GameObject after the event ends
        //Debug.Log("Destroy Emitter");
        Destroy(gameObject);
    }

    // Static Spawn Methods
    public static OneShotEnvironmentAwareEmitter Spawn(AK.Wwise.Event wwiseEvent, Vector3 position, Quaternion rotation = default)
    {
        if (rotation == default) rotation = Quaternion.identity;

        GameObject go = new GameObject(wwiseEvent.Name);
        go.transform.position = position;
        go.transform.rotation = rotation;

        var emitter = go.AddComponent<OneShotEnvironmentAwareEmitter>();

        ApplyRoomSwitch(go);

        Debug.Log("Emitter Playing!");
        emitter.PlayEnvironmentAwareEvent(wwiseEvent);
        return emitter;
    }

    public static OneShotEnvironmentAwareEmitter Spawn(string eventName, Vector3 position, Quaternion rotation = default)
    {
        if (rotation == default) rotation = Quaternion.identity;

        GameObject go = new GameObject("OneShotEmitter");
        go.transform.position = position;
        go.transform.rotation = rotation;

        var emitter = go.AddComponent<OneShotEnvironmentAwareEmitter>();

        ApplyRoomSwitch(go);
        
        emitter.PlayEnvironmentAwareEvent(eventName);
        Debug.Log("Emitter Playing!");
        return emitter;
    }

    private static void ApplyRoomSwitch(GameObject go)
    {
        Collider[] hits = Physics.OverlapSphere(go.transform.position, 0.1f);

        CustomRoom bestRoom = null;
        int bestPriority = int.MinValue;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<CustomRoom>(out var room))
            {
                if (room.priority > bestPriority)
                {
                    bestPriority = room.priority;
                    bestRoom = room;
                }
            }
        }

        if (bestRoom != null)
        {
            Debug.Log($"Using room {bestRoom.name} (priority {bestPriority})");
            bestRoom.roomSwitch.SetValue(go);
        }
    }


}
