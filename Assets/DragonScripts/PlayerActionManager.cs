using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private PlayerInputController inputController;

    public float speedX = 5f;
    public float speedJump = 16f;

    private bool jumping = false;
	
	private float velocityY = 0f;
	Collider[] collisionArray = new Collider[10];

	[SerializeField] SpriteRenderer spriteRenderer;
	//int layerMaskPlatform = 1 << 7;


	private void Update()
    {
		float dt = Time.deltaTime;

		// �ړ�����
		float moveX = inputController.HorizontalMovement;

		if (inputController.JumpPressed() && jumping == false)
		{
			// �W�����v���s
			jumping = true;
			velocityY = speedJump;
		}

		Vector2 movePos = transform.position;

		// X���̈ړ�
		movePos.x += (moveX * speedX * dt);


		// Y���̈ړ� (�W�����v)
		float currentY = movePos.y;
		velocityY += (-20f * dt);
		movePos.y += (velocityY * dt);
		bool falling = currentY > movePos.y;


		// Platform�Ƃ̓����蔻��
		int hitCount = Physics.OverlapBoxNonAlloc(movePos + Vector2.up * 0.2f, new Vector2(0.4f, 0.4f), collisionArray, Quaternion.identity, LayerMask.GetMask("Platform"));
		bool hitPlatform = Physics.Raycast(movePos + Vector2.up, Vector2.down, out RaycastHit raycastHit, 2f, LayerMask.GetMask("Platform"));

		// ���n����
		if (hitCount > 0  && falling && hitPlatform)
		{
			// ���n�����ꍇ
			movePos.y = raycastHit.point.y;
			velocityY = 0f;
			jumping = false;
		}

		// ���f������
		transform.position = movePos;

		// �̂̌�����ύX
		if (moveX != 0f)
		{
			spriteRenderer.flipX = (moveX < 0f);
		}
	}
}
