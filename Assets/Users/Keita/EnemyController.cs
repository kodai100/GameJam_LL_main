using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField]
	private float m_Visibility;

	[SerializeField]
	private float m_TurnSpeed;

	private Transform m_PlayerTransform;

	void Awake()
	{
		// Fix later.
		m_PlayerTransform = GameObject.Find("Player").transform;
	}

	void Update ()
	{
		if (SearchPlayer())
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
	}

	private void RunAway()
	{
		var dir = NormalizedXZDirection(transform.position, m_PlayerTransform.position);

		transform.forward = Vector3.Lerp(transform.forward, -dir, Time.deltaTime);

		Move();
	}

	private void Approach()
	{
		var dir = NormalizedXZDirection(transform.position, m_PlayerTransform.position);

		transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * m_TurnSpeed);

		Move();
	}

	private void Move()
	{
		var scale = transform.localScale.x;

		transform.Translate(Scale2Speed(scale) * Vector3.forward * Time.deltaTime * m_TurnSpeed);
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
