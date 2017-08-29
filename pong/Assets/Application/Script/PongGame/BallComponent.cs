using UnityEngine;

public class BallComponent : MonoBehaviour {

	//初期速度
	public Rigidbody2D rigidbody;	//速度を与える対象のRigidBody
	private const float randomRange1 = 2000.0f;
	private const float randomRange2 = 4000.0f;

	private System.Action<BallComponent, GoalComponent> goalAction;

	/// <summary>
	/// 初期化処理：速度の初期化とゴール時のActionを指定
	/// </summary>
	/// <param name="goalAction">Goal action.</param>
	public void Init(System.Action<BallComponent, GoalComponent> goalAction) {
		this.rigidbody.AddForce (new Vector2(Random.Range(randomRange1,randomRange2),Random.Range(randomRange1,randomRange2)));
		this.goalAction = goalAction;
	}


	/// <summary>
	/// 他オブジェクトに接触->ゴールオブジェクトの場合はゴール処理を実行
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other) {
		if( other.gameObject.tag == "Goal" ) {
			GoalComponent goal = other.gameObject.GetComponent<GoalComponent>();

			if (this.goalAction != null){
				this.goalAction(this, goal);
			}
			Destroy( this.gameObject );
		}
	}
}
