using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
	[SerializeField]
	private float m_Visibility;

	[SerializeField]
	private float m_TurnSpeed;

	private bool m_IsGround;

	private Rigidbody m_Rigidbody;

	private Transform m_PlayerTransform;

	void Awake()
	{
		m_IsGround = false;
		m_Rigidbody = GetComponent<Rigidbody>();

		// Fix later.
		m_PlayerTransform = GameObject.Find("Player").transform;
	}

	void Update ()
	{
		if (!m_IsGround)
			return;

		if (SearchPlayer())
		{
			Move();
		}
		else
		{
			m_Rigidbody.velocity = Vector3.zero;
		}
	}

	void FixedUpdate()
	{
		m_IsGround = m_IsGround || Physics.Raycast(transform.position, -transform.up, 0.01f);
	}

	private void Move()
	{
		if (transform.localScale.x < m_PlayerTransform.localScale.x)
		{
			RunAway();
		}
		else
		{
			Approach();
		}
	}

	private void RunAway()
	{
		var dir = NormalizedXZDirection(transform.position, m_PlayerTransform.position);

		transform.forward = Vector3.Lerp(transform.forward, -dir, Time.deltaTime * m_TurnSpeed);

		m_Rigidbody.velocity = Scale2Speed(transform.localScale.x) * transform.forward;
	}

	private void Approach()
	{
		var dir = NormalizedXZDirection(transform.position, m_PlayerTransform.position);

		transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * m_TurnSpeed);

		m_Rigidbody.velocity = Scale2Speed(transform.localScale.x) * transform.forward;
	}

	private bool SearchPlayer()
	{
		return Vector3.SqrMagnitude(transform.position - m_PlayerTransform.position) < m_Visibility * m_Visibility;
	}

	private float Scale2Speed(float scale)
	{
		var rate = 0.5f;

		return rate * scale;
	}

	private Vector3 NormalizedXZDirection(Vector3 a, Vector3 b)
	{
		var dir = b - a;
		dir.y = 0f;
		return dir.normalized;
	}

#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, m_Visibility);
	}
#endif
}
