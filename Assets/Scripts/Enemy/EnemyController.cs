using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
	enum State
	{
		Idle, RunAway, Scared, Approach
	}

	[SerializeField]
	private float m_Visibility;

	[SerializeField]
	private float m_Speed;

	[SerializeField]
	private float m_TurnSpeed;

	[SerializeField]
	private float m_ScaredTime;

	private State m_State;

	private bool m_IsGround;

	private float m_ScaredTimer;

	private Rigidbody m_Rigidbody;

	private Transform m_PlayerTransform;

	void Awake() {
		m_State = State.Idle;
		m_IsGround = false;
		m_ScaredTimer = 0f;
		m_Rigidbody = GetComponent<Rigidbody>();

        // Random Color
        GetComponent<Renderer>().material.SetColor("_Color", Color.HSVToRGB(Random.Range(0f,1f),1f,1f));

		// Fix later.
		m_PlayerTransform = GameObject.Find("PlayerParent").transform;
	}

	void Update ()
	{
		if (!m_IsGround)
			return;

		if (SearchPlayer ())
		{
			m_State = transform.localScale.x < m_PlayerTransform.localScale.x ? State.RunAway : State.Approach;
		}
		else if (m_State == State.Approach)
		{
			m_State = State.Idle;
		}
		else if (m_State == State.RunAway)
		{
			m_State = State.Scared;
			m_ScaredTimer = m_ScaredTime;
		}
		else if (m_State == State.Scared)
		{
			m_ScaredTimer -= Time.deltaTime;

			if (m_ScaredTimer < 0f)
			{
				m_State = State.Idle;
			}
		}

		Move();
	}

	void FixedUpdate()
	{
		var radius = transform.localScale.x;

		m_IsGround = Physics.Raycast(transform.position, -transform.up, radius + 0.01f);
	}

	private void Move()
	{
		switch (m_State)
		{
		case State.Idle:
			Walk ();
			break;

		case State.RunAway:
			RunAway ();
			break;

		case State.Scared:
			RunAway ();
			break;

		case State.Approach:
			Approach ();
			break;
		}
	}

	private void Walk()
	{
		transform.Rotate (Vector3.up * Random.Range (-10f, 10f));

		m_Rigidbody.velocity = transform.forward * m_Speed / 3.0f;
	}

	private void RunAway()
	{
		var dir = NormalizedXZDirection(transform.position, m_PlayerTransform.position);

		transform.forward = Vector3.Lerp(transform.forward, -dir, Time.deltaTime * m_TurnSpeed);
		transform.Rotate (Vector3.up * Random.Range (-10f, 10f));

		m_Rigidbody.velocity = transform.forward * m_Speed;
	}

	private void Approach()
	{
		var dir = NormalizedXZDirection(transform.position, m_PlayerTransform.position);

		transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * m_TurnSpeed);

		m_Rigidbody.velocity = transform.forward * m_Speed;
	}

	private bool SearchPlayer()
	{
		return Vector3.SqrMagnitude(transform.position - m_PlayerTransform.position) < m_Visibility * m_Visibility;
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
