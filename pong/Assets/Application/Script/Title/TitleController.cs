using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {


	/// <summary>
	/// ゲームをスタートする
	/// </summary>
	public void OnGamePlay(){
		Mgrs.sceneMgr.LoadScene(DEFINE.SCENE_PONG_GAME);
	}
	
	
	/// <summary>
	/// ゲームのプレイ履歴画面に遷移
	/// </summary>
	public void OnPlayHistory(){
		//TODO:表示処理追加
	}
	
}
