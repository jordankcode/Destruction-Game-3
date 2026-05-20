using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponHotbarUI : MonoBehaviour
{
    [System.Serializable]
    public class WeaponSlot
    {
        public Image slotBackground;
        public Image weaponIcon;
        public Image highlight;
        public Image lockOverlay;   // assign a dark/padlock overlay image
    }

    [Header("Slots - match order of weapons in WeaponSwitching")]
    [SerializeField] private WeaponSlot[] slots;

    [Header("Colors")]
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color lockedIconColor = new Color(0.3f, 0.3f, 0.3f, 1f);

    [Header("Input - same keys as WeaponSwitching")]
    [SerializeField] private InputAction[] weaponKeys;

    private int currentSlot = 0;

    private void Start()
    {
        foreach (var action in weaponKeys)
            action.Enable();

        RefreshAll();
    }

    private void Update()
    {
        // Mirror the same unlock conditions from CoinCollection/WeaponSwitching
        if (WeaponSwitching.Upgrade2 && weaponKeys.Length > 2 && weaponKeys[2].WasPressedThisFrame())
            SetSlot(2);
        else if (WeaponSwitching.Upgrade && weaponKeys.Length > 1 && weaponKeys[1].WasPressedThisFrame())
            SetSlot(1);
        else if (weaponKeys.Length > 0 && weaponKeys[0].WasPressedThisFrame())
            SetSlot(0);

        // Refresh locks every frame (in case coins just unlocked something)
        RefreshLocks();
    }

    private void SetSlot(int index)
    {
        currentSlot = index;
        RefreshAll();
    }

    private void RefreshAll()
    {
        RefreshLocks();
        RefreshHighlight();
    }

    private void RefreshHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].highlight != null)
                slots[i].highlight.color = (i == currentSlot) ? highlightColor : Color.clear;
        }
    }

    private void RefreshLocks()
    {
        // Slot 0 always unlocked
        SetSlotLocked(0, false);

        // Slot 1 unlocks when WeaponSwitching.Upgrade is true
        SetSlotLocked(1, !WeaponSwitching.Upgrade);

        // Slot 2 unlocks when WeaponSwitching.Upgrade2 is true
        SetSlotLocked(2, !WeaponSwitching.Upgrade2);
    }

    private void SetSlotLocked(int index, bool locked)
    {
        if (index >= slots.Length) return;

        var slot = slots[index];

        if (slot.lockOverlay != null)
            slot.lockOverlay.gameObject.SetActive(locked);

        // Dim the icon when locked
        if (slot.weaponIcon != null)
            slot.weaponIcon.color = locked ? lockedIconColor : Color.white;
    }

    private void OnDisable()
    {
        foreach (var action in weaponKeys)
            action.Disable();
    }
}