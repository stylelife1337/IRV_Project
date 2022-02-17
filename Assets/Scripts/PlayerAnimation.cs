using UnityEngine;

public class PlayerAnimation {

    private Animator animator;
    private int animSpeed = Animator.StringToHash("Speed");

	public void Init (Animator anim)
    {
        animator = anim;
	}
	
	public void UpdateAnimation (float speed)
    {
        animator.SetFloat(animSpeed, speed);
	}
}
