using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponSwitching : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Input Actions")]
    [SerializeField] private InputAction[] weaponKeys;

    [Header("Settings")]
    [SerializeField] private float[] switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);
        timeSinceLastSwitch = 0f;

        // Enable all actions
        foreach (var action in weaponKeys)
        {
            action.Enable();
        }
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            weapons[i] = transform.GetChild(i);
        }

        if (weaponKeys == null) weaponKeys = new InputAction[weapons.Length];
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        for (int i = 0; i < weaponKeys.Length; i++)
        {
            if (weaponKeys[i].WasPressedThisFrame() && timeSinceLastSwitch >= switchTime[i])
            {
                selectedWeapon = i;
            }
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            Select(selectedWeapon);
        }

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
        timeSinceLastSwitch = 0f;
        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
        print("selected new weapon");
    }

    private void OnDisable()
    {
        foreach (var action in weaponKeys)
        {
            action.Disable();
        }
    }
}