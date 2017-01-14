using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
#if UNITY_EDITOR
	[SerializeField]
	private bool m_ShowSpawnArea;
#endif

	[SerializeField]
	private Vector3 m_SpawnAreaSize;

	[SerializeField]
	private float m_SpawnInterval;

	[SerializeField]
	private float m_SafetyRange;

	[SerializeField]
	private GameObject m_EnemyPrefab;

	private Transform m_PlayerTransform;

	private float m_Timer;

	void Awake()
	{
		// Fix later.
		m_PlayerTransform = GameObject.Find("Player").transform;
		m_Timer = m_SpawnInterval;
	}

	void Update()
	{
		m_Timer -= Time.deltaTime;

		if (m_Timer < 0f)
		{
			Vector3 pos = transform.position;

			Instantiate(m_EnemyPrefab, pos, Quaternion.identity);
		}
	}

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		if (m_ShowSpawnArea)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(transform.position + new Vector3(0f, m_SpawnAreaSize.y / 2f, 0f), m_SpawnAreaSize);
		}
	}
#endif
}
