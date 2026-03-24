using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitching : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform[] weapons;

    [Header("Input Actions")]
    [SerializeField] private InputAction[] weaponKeys;

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    public static bool Upgrade = false;
    public static bool Upgrade2 = false;
    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);
        timeSinceLastSwitch = 0f;

        foreach (var action in weaponKeys)
            action.Enable();
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            weapons[i] = transform.GetChild(i);

        if (weaponKeys == null) weaponKeys = new InputAction[weapons.Length];
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (weaponKeys.Length > 0 && weaponKeys[0].WasPressedThisFrame() && timeSinceLastSwitch >= switchTime)
            selectedWeapon = 0;

        if (Upgrade && weaponKeys.Length > 1 && weaponKeys[1].WasPressedThisFrame() && timeSinceLastSwitch >= switchTime)
            selectedWeapon = 1;

        if (Upgrade2 && weaponKeys.Length > 2 && weaponKeys[2].WasPressedThisFrame() && timeSinceLastSwitch >= switchTime)
            selectedWeapon = 2;

        if (previousSelectedWeapon != selectedWeapon) Select(selectedWeapon);

        timeSinceLastSwitch += Time.deltaTime;
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == weaponIndex);

        timeSinceLastSwitch = 0f;
        OnWeaponSelected();
    }

    private void OnWeaponSelected() { }

    private void OnDisable()
    {
        foreach (var action in weaponKeys)
            action.Disable();
    }
}