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
	private int m_InitPopulation;

	[SerializeField]
	private float m_SafetyRange;

	[SerializeField]
	private GameObject m_EnemyPrefab;

	private Transform m_PlayerTransform;

	private Transform m_Enemys;

	private float m_Timer;

	void Awake()
	{
		// Fix later.
		m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		m_Enemys = GameObject.Find("Enemys").transform;
		m_Timer = m_SpawnInterval;
	}

	void Start()
	{
		for (int i = 0; i < m_InitPopulation; i++)
		{
			Spawn ();
		}
	}

	void Update()
	{
		m_Timer -= Time.deltaTime;

		if (m_Timer < 0f)
		{
			Spawn();
			m_Timer = m_SpawnInterval;
		}
	}

	private void Spawn()
	{
		Vector3 position = Vector3.zero;

		// Prevent Infinite Loop.
		for (int i = 0; i < 10; i++)
		{
			position = GetSpawnPosition();

			if (SqrDistanceXZ(position, m_PlayerTransform.position) > m_SafetyRange * m_SafetyRange)
				break;
		}

		var go = (GameObject)Instantiate(m_EnemyPrefab, position, Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up));
		go.transform.SetParent(m_Enemys);
	}

	private Vector3 GetSpawnPosition()
	{
		Vector3 pos = Vector3.zero;
		var halfWidth = m_EnemyPrefab.transform.localScale.x / 2f;

		pos.x = Random.Range(transform.position.x - m_SpawnAreaSize.x / 2f + halfWidth, transform.position.x + m_SpawnAreaSize.x / 2f - halfWidth);
		pos.y = transform.position.y + m_SpawnAreaSize.y;
		pos.z = Random.Range(transform.position.z - m_SpawnAreaSize.z / 2f + halfWidth, transform.position.z + m_SpawnAreaSize.z / 2f - halfWidth);

		return pos;
	}

	private float SqrDistanceXZ(Vector3 a, Vector3 b)
	{
		var dir = a - b;
		dir.y = 0f;
		return Vector3.SqrMagnitude(dir);
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
