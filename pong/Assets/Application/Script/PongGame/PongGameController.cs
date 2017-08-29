using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


/// <summary>
/// Pongゲームのコントローラ
/// </summary>
public class PongGameController : MonoBehaviour {

	/// <summary>
	/// ボール作成する親obj
	/// </summary>
	public Transform parentBall;

	/// <summary>
	/// スコア表示(player1)
	/// </summary>
	public Text lblPlayer1;

	/// <summary>
	/// スコア表示(player2)
	/// </summary>
	public Text lblPlayer2;

	public RacketComponent racket1;
	public RacketComponent racket2;

	/// <summary>
	/// ゲームセット時に表示するobj
	/// </summary>
	public GameObject objGameSet;
	public Text lblWinner;

	private LinkedList<BallComponent> ballList = new LinkedList<BallComponent>();
	BallComponent currentBall;

	private ScoreManager scoreMgr;
	private GameObject prefabBall;


	#region Initialize
	/// <summary>
	/// 初期設定
	/// </summary>
	void Awake() {
		this.prefabBall = Resources.Load<GameObject>( DEFINE.PREFAB_PATH_BALL );
		this.GameStart();
	}


	/// <summary>
	/// ゲーム初期化＆スタート
	/// </summary>
	public void GameStart() {
		this.scoreMgr = new ScoreManager( DEFINE.GAME_MATCH_POINT );
		this.objGameSet.SafeActive( false );
		this.InitBall();
		this.UpdateScoreLabel();
	}

	/// <summary>
	/// ボール発射
	/// </summary>
	public void InitBall() {
		currentBall = Util.InstantiateComponent<BallComponent>( this.prefabBall, this.parentBall );
		currentBall.Init( this.CollisionEnter );

		ballList.AddLast(currentBall);
	}

	#endregion


	#region Update
	/// <summary>
	/// 更新処理
	/// </summary>
	private void Update() {

		UpdateRackets();
	}


	/// <summary>
	/// ラケットの更新
	/// </summary>
	private void UpdateRackets(){
		if (racket1 != null){
			racket1.UpdateRacket(ballList);
		}
		if (racket2 != null){
			racket2.UpdateRacket(ballList);
		}
	}


	/// <summary>
	/// ボールがゴールに衝突
	/// </summary>
	/// <param name="ball">Ball.</param>
	/// <param name="goal">Goal.</param>
	public void CollisionEnter(BallComponent ball, GoalComponent goal) {
		this.scoreMgr.AddScore( goal.playerId );
		this.UpdateScoreLabel();

		ballList.Remove(ball);

		if( this.scoreMgr.isGameSet ) {
			this.GameSet();
		}else{
			this.InitBall();
		}
	}

	/// <summary>
	/// スコアラベルの更新
	/// </summary>
	private void UpdateScoreLabel() {
		this.lblPlayer1.text = this.scoreMgr.score[1].ToString();
		this.lblPlayer2.text = this.scoreMgr.score[2].ToString();
	}

	#endregion


	/// <summary>
	/// ゲーム終了
	/// </summary>
	private void GameSet() {
		Debug.Log("GameSet");
		this.lblWinner.text = string.Format("player{0} winner!!", this.scoreMgr.winnerPlayerId);
		this.objGameSet.SafeActive( true );
	}


	/// <summary>
	/// タイトルに戻る
	/// </summary>
	public void OnBack() {
		//シーン遷移
		Mgrs.sceneMgr.LoadScene(DEFINE.SCENE_TITLE);
	}
}
