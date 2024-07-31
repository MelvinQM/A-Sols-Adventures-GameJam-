using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class TerraBlast : Ability
{
    Shooting shootingScript;
    public override void Activate(GameObject parent)
    {
        if(shootingScript == null)
            shootingScript = parent.GetComponent<AbilityManager>().shootingRef;
        if(shootingScript == null) Debug.Log("NO SHOOTING SCRIPT");

        instance.SetActive(true);
        instance.transform.SetPositionAndRotation(shootingScript.shootingPointTransform.position, shootingScript.shootingPointTransform.rotation);
        instance.transform.SetParent(shootingScript.AttacksContainer);
        
        //float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        instance.GetComponent<TerraBlastProjectile>().Boom(shootingScript.direction.x, shootingScript.direction.y, shootingScript.rot);
    }

    // public override void AbilityUpdate()
    // {
        
    // }

    public override void BeginCoolDown(GameObject parent)
    {
        instance = Instantiate(prefab);

        if(instance.activeInHierarchy)
            instance.SetActive(false);
    }
}
