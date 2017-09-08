using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のグラフィックを管理する
/// </summary>
public class EnemyGraphController : MonoBehaviour {

	#region SerializeField variables
	[SerializeField] Material strongEnemy;
	[SerializeField] Material weakEnemy;
	#endregion

	#region public functions
	/// <summary>
	/// 渡されたScale値を参考に、全ての敵グラフィックのグラフィックを更新する（食べるか食べられるか）
	/// </summary>
	public void UpdateAllEnemiesColor(float scale){

		GameObject enemy;
		Color enemyColor;
		for (int i = 0; i < transform.childCount; i++) {

			enemy = transform.GetChild (i).gameObject;
			enemyColor = enemy.GetComponent<Renderer>().material.GetColor("_Color");
			if (enemy.transform.localScale.x <= scale) {	// 青く
				enemy.GetComponent<Renderer>().material = weakEnemy;
			}
			else {	// 赤く
				enemy.GetComponent<Renderer>().material = strongEnemy;
			}
			enemy.GetComponent<Renderer> ().material.SetColor ("_Color", enemyColor);
		}

	}
	#endregion

}
