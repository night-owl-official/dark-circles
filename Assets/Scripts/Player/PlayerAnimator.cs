using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAnimator : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        Assert.IsNotNull(animator, "No animator was assigned to PlayerAnimator");
        Assert.IsNotNull(spriteRenderer, "SpriteRenderer was not assigned to PlayerMovement");
    }

    public void OnPlayerAttackStart() {
        // Unset animation attack flag
        SetAttackingAnimation(false);
        SetDirectionAnimation(-1);
    }

    public void OnPlayerAttackEnd() {
        // Tell PlayerMovement to unlock movement
        onPlayerDoneAttacking?.Invoke(true);
    }

    public void FlipSpriteHorizontal(bool isFlipped) {
        spriteRenderer.flipX = isFlipped;
    }

    public void SetWalkingAnimation(bool isSet) {
        animator.SetBool(walkBooleanName, isSet);
    }

    public void SetAttackingAnimation(bool isSet) {
        animator.SetBool(attackBooleanName, isSet);
    }

    public void SetDirectionAnimation(int direction) {
        animator.SetInteger(moveDirectionIntName, direction);
    }

    public void SetHitAnimation() {
        animator.SetTrigger(hitTriggerName);
    }
    #endregion

    #region Member variables
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string walkBooleanName = "isWalking";
    [SerializeField]
    private string attackBooleanName = "isAttacking";
    [SerializeField]
    private string moveDirectionIntName = "moveDirection";
    [SerializeField]
    private string hitTriggerName = "hit";

    public BoolEvent onPlayerDoneAttacking;
    #endregion
}
