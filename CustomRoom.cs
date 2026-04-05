using Photon.Realtime;
using UnityEngine;

public class CustomRoom : AkRoom
{
    public AK.Wwise.Switch roomSwitch;

    protected override void Awake()
    {
        base.Awake();

        if (TryGetComponent<Renderer>(out Renderer rend))
        {
            rend.enabled = false;
        }
    }

    protected override void OnTriggerEnter(Collider in_other)
    {
        base.OnTriggerEnter(in_other); 

        //TODO Does this run before the emitter posts event?
        if (in_other.TryGetComponent<AkRoomAwareObject>(out AkRoomAwareObject emitter))
        {
            Debug.Log($"Emitter {emitter.gameObject.name} received switch {roomSwitch}");
            roomSwitch.SetValue(emitter.gameObject);
        }        
    }

    protected override void OnTriggerExit(Collider in_other)
    {
        base.OnTriggerExit(in_other);
    }
}
