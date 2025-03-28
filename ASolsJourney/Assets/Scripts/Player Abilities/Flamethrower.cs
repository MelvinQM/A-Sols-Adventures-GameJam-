using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Flamethrower : Ability
{
    Shooting shootingScript;
    public override void Activate(GameObject parent)
    {
        if(shootingScript == null)
            shootingScript = parent.GetComponent<AbilityManager>().shootingRef;
        if(shootingScript == null) Debug.Log("NO SHOOTING SCRIPT");
        
        instance.SetActive(true);
        instance.transform.SetPositionAndRotation(shootingScript.shootingPointTransform.position, shootingScript.shootingPointTransform.rotation);
        instance.transform.SetParent(shootingScript.shootingPointTransform);
    }

    // public override void AbilityUpdate()
    // {
        
    // }

    public override void BeginCoolDown(GameObject parent)
    {
        instance.SetActive(false);
    }
    
}
