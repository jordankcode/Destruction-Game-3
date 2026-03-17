using UnityEngine;
using UnityEngine.InputSystem;

public class SledgeHammerAnimator : MonoBehaviour
{
    private Animator mAnimator;

    [SerializeField] private InputActionAsset actions;
    private InputAction attackAction;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        attackAction = actions.FindActionMap("Player").FindAction("Attack");
        attackAction.Enable();
    }

    void Update()
    {
        if (mAnimator != null && attackAction != null)
        {
            if (attackAction.WasPressedThisFrame())
            {
                mAnimator.SetTrigger("TrSwing");
            }
        }
    }

    void OnDisable()
    {
        attackAction.Disable();
    }
}