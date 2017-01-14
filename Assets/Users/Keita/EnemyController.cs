using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField]
	private float m_Visibility;

	private CharacterController m_CharacterController;

	private Transform m_PlayerTransform;

	void Awake()
	{
		m_CharacterController = GetComponent<CharacterController>();

		// Fix later.
		m_PlayerTransform = GameObject.Find("Player").transform;
	}

	void Update ()
	{
		var scale = transform.localScale.x;

		if (SearchPlayer())
		{
			m_CharacterController.SimpleMove(transform.forward * Scale2Speed(scale));
		}
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

#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, m_Visibility);
	}
#endif
}
