using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAnimator : MonoBehaviour {
    #region Methods
    // Start is called before the first frame update
    private void Start() {
        Assert.IsNotNull(animator, "No animator was assigned to PlayerAnimator");
        Assert.IsNotNull(spriteRenderer, "SpriteRenderer was not assigned to PlayerMovement");
    }

    public void FlipSpriteHorizontal(bool isFlipped) {
        spriteRenderer.flipX = isFlipped;
    }

    public void SetWalkingAnimation(bool isSet, int direction) {
        animator.SetBool(walkBooleanName, isSet);
        animator.SetInteger(moveDirectionIntName, direction);
    }

    public void SetAttackingAnimation(bool isSet, int direction) {
        animator.SetBool(attackBooleanName, isSet);
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
    #endregion
}
