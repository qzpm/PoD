using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン全体管理のクラス
/// </summary>
public class AppSceneManager : MonoBehaviour 
{

	[SerializeField]
	Animator	transitionAnimator;				//シーン遷移時に表示するアニメーション
	string		keySceneHide = "isHide";	//そのトリガー


	void Awake()
	{
		if (transitionAnimator!=null){
			//実行したアニメーションでAnimExecTransitionを呼ぶこと!
			transitionAnimator.SetBool(keySceneHide, false);
		}
	}

	/// <summary>
	/// トランジション込みでシーン遷移
	/// </summary>
	public void LoadScene(string sceneName, bool isTransition = true)
	{

		Debug.Log("LoadScene:" + sceneName);
		//アニメータが紐付けされている場合はそちらを実行
		if (transitionAnimator!=null && isTransition){

			//実行したアニメーションでAnimExecTransitionを呼ぶこと!
			StartCoroutine(IELoadScene(sceneName));
		}
		else{
			ExecLoadScene(sceneName);
		}
	}


	/// <summary>
	/// 演出を交えつつ遷移実行
	/// </summary>
	/// <returns>The load scene.</returns>
	/// <param name="sceneName">Scene name.</param>
	private IEnumerator IELoadScene(string sceneName){

		transitionAnimator.SetBool(keySceneHide, true);

		yield return new WaitForSeconds(0.5f);

		ExecLoadScene(sceneName);
	}


	/// <summary>
	/// 遷移実行
	/// </summary>
	private void ExecLoadScene(string sceneName)
	{
		if (!string.IsNullOrEmpty(sceneName)){
			SceneManager.LoadScene(sceneName);
		}
	}
}
