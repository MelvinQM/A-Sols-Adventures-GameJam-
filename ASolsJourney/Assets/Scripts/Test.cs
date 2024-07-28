using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private KeyCode addButton;
    [SerializeField] private KeyCode deleteButton;
    [SerializeField] private Ability testAbility;

    void Update()
    {
        if (Input.GetKeyDown(addButton))
        {
            AddTestAbility();
        }

        if (Input.GetKeyDown(deleteButton))
        {
            DeleteTestAbility();
        }
    }

    void AddTestAbility()
    {
        abilityManager.AddAbility(testAbility, true);
    }

    void DeleteTestAbility()
    {
        abilityManager.DeleteAbility(testAbility);
    }
}
       
