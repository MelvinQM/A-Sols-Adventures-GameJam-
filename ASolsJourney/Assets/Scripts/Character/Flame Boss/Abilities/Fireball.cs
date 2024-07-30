using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class Fireball : Ability
    {
        private Transform target;

        public override void UseAbility(object[] args)
        {
            base.UseAbility(args);
            target = (Transform)args[0];

            
        }
    }
}