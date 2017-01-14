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
		m_CharacterController.SimpleMove(transform.forward);
	}
}
