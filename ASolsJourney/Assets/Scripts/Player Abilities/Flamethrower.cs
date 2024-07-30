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
        instance.gameObject.SetActive(true);
        if(shootingScript == null)
            shootingScript = parent.GetComponent<AbilityManager>().shootingRef;
        
        if(shootingScript == null) Debug.Log("NO SHOOTING SCRIPT");
        instance.transform.position = shootingScript.shootingPointTransform.position;
        instance.transform.SetParent(shootingScript.shootingPointTransform);
        
    }

    public override void AbilityUpdate()
    {
        //instance.transform.SetPositionAndRotation(shootingScript.shootingPointTransform.position, shootingScript.shootingPointTransform.rotation);
    }

    public override void BeginCoolDown(GameObject parent)
    {
        instance.SetActive(false);
    }
    
}
