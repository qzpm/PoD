//  CacheData.cs
//  http://kan-kikuchi.hatenablog.com/entry/CacheData
//
//  Created by kan.kikuchi on 2016.05.09.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CacheData")]
/// <summary>
/// キャッシュ用のデータ
/// </summary>
public class CacheData : ScriptableObject {

	//データの実体
	private static CacheData _entity;
	public  static CacheData  Entity{
		get{
			if(_entity == null){
				_entity = Resources.Load<CacheData>("CacheData");

				if(_entity == null){
					Debug.LogError("CacheData is null");
				}

				//実機で実行する時は初期化する
				#if !UNITY_EDITOR
				_entity.Refresh ();
				#endif
			}
			return _entity;
		}
	}

	//スコア
	public float P1Score = 100;
	public float P2Score = 100;
	public int P1Area = 5;
	public int P2Area = 5;
	public int P1Skill = -1;
	public int P2Skill = -1;
	public float P1SkillPoint = 0;
	public float P2SkillPoint = 0;

	public bool P1Result = false;
	public bool P2Result = false;
 
	public float Time = 0;

	//=================================================================================
	//初期化
	//=================================================================================

	/// <summary>
	/// キャッシュを全て初期化
	/// </summary>
	public void Refresh(){
		P1Score = 100;
		P2Score = 100;

		P1Area = 5;
		P2Area = 5;

		P1Skill = -1;
		P2Skill = -1;

		P1SkillPoint = 0;
		P2SkillPoint = 0;

		Time = 0;
	}

}