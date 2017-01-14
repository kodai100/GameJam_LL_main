using UnityEngine;

public class EnemyController : MonoBehaviour
{
	private CharacterController m_CharacterController;

	void Awake()
	{
		m_CharacterController = GetComponent<CharacterController>();
	}

	void Update ()
	{
		var scale = transform.localScale.x;

		m_CharacterController.SimpleMove(transform.forward * Scale2Speed(scale));
	}

	private float Scale2Speed(float scale)
	{
		var rate = 0.5f;

		return rate * scale;
	}
}
