using UnityEngine;

public static class Mgrs {

	private static AppSceneManager _sceneMgr;
	public static AppSceneManager sceneMgr{
		get{
			if (_sceneMgr == null){

				//初回：sceneから検索して取得
				_sceneMgr = GameObject.FindObjectOfType<AppSceneManager>();

				//対象が存在しない場合は新しく生成する
				if (_sceneMgr == null){
					GameObject gmoScneMgr = new GameObject("sceneManager");
					_sceneMgr = gmoScneMgr.AddComponent<AppSceneManager>();
				}
			}

			return _sceneMgr;
		}
	}
}
