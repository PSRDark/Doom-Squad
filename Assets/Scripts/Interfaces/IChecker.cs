using UnityEngine;
using System.Collections;

public class IChecker : MonoBehaviour {

	public static IDamageable IDamageable(Collider2D collider)
    {
        MonoBehaviour[] mbs = collider.gameObject.GetComponents<MonoBehaviour>();
        foreach(MonoBehaviour mb in mbs)
        {
            if(mb is IDamageable)
            {
                IDamageable idamageable = mb as IDamageable;
                return idamageable;
            }
        }
        return null;
    }
    public static IDamageable IDamageable(GameObject collider)
    {
        MonoBehaviour[] mbs = collider.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in mbs)
        {
            if (mb is IDamageable)
            {
                IDamageable idamageable = mb as IDamageable;
                return idamageable;
            }
        }
        return null;
    }
}
