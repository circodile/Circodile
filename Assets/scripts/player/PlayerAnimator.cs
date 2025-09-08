using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;

    private Vector2 lastMove = Vector2.down; // Por defecto abajo

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(moveX, moveY).normalized;

        bool isMoving = move != Vector2.zero;

        if (isMoving)
        {
            lastMove = move;
        }

        animator.SetFloat("moveX", isMoving ? move.x : lastMove.x);
        animator.SetFloat("moveY", isMoving ? move.y : lastMove.y);
        animator.SetBool("isMoving", isMoving);
    }
}
