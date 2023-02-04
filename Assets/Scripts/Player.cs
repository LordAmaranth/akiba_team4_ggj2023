using UnityEngine;


public class Player : MonoBehaviour
{
	float speedMove = 5f; // �ړ��X�s�[�h
	float speedJump = 16f; // �W�����v��

	// �W�����v�p
	bool flgJumping = false;
	float accY = 0f;


	SpriteRenderer cmpSpRender;


	// Start is called before the first frame update
	void Start()
    {
		cmpSpRender = transform.Find("Body").GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
    {
		var dt = Time.deltaTime;

		// �ړ�����
		var moveX = 0f;
		moveX = Input.GetAxis("Horizontal");

		if ( flgJumping == false && Input.GetButtonDown("Jump") )
		{
			// �W�����v���s
			flgJumping = true;
			accY = speedJump;
		}

		var posTmp = transform.position;

		// X���̈ړ�
		posTmp.x += ( moveX * speedMove * dt );

		// Y���̈ړ� (�W�����v)
		var bupY = posTmp.y;
		accY += ( -20f * dt );
		posTmp.y += ( accY * dt );

		// Platform�Ƃ̓����蔻��
		var hits = Physics.OverlapBox( posTmp + Vector3.up * 0.2f, new Vector3( 0.4f, 0.4f, 0.4f ), Quaternion.identity, LayerMask.GetMask("Platform") );
		var flgHitPlatform = Physics.Raycast(posTmp + Vector3.up, Vector3.down, out RaycastHit raycastHit, 2f, LayerMask.GetMask("Platform"));

		// ���n����
		if ( ( bupY > posTmp.y ) && ( hits.Length > 0 ) && flgHitPlatform )
		{
			// ���n�����ꍇ
			posTmp.y = raycastHit.point.y;
			accY = 0f;
			flgJumping = false;
		}

		// ���f������
		transform.position = posTmp;

		// �̂̌�����ύX
		if( moveX != 0f )
		{
			cmpSpRender.flipX = ( moveX < 0f );
		}
	}
}
