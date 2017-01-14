using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
#if UNITY_EDITOR
	[SerializeField]
	private bool m_ShowSpawnArea;
#endif

	[SerializeField]
	private Vector3 m_SpawnAreaCenter;

	[SerializeField]
	private Vector3 m_SpawnAreaSize;

	[SerializeField]
	private float m_SafetyRange;

	[SerializeField]
	private GameObject m_EnemyPrefab;

	private Transform m_PlayerTransform;

	void Awake()
	{
		// Fix later.
		m_PlayerTransform = GameObject.Find("Player").transform;
	}

#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		if (m_ShowSpawnArea)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireCube(m_SpawnAreaCenter + new Vector3(0f, m_SpawnAreaSize.y / 2f, 0f), m_SpawnAreaSize);
		}
	}
#endif
}
