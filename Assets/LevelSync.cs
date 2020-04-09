using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.GameStructure;

public class LevelSync : MonoBehaviour
{

    int levelCount;
    void Awake()
	{
		StartCoroutine(SyncLevelsFromServer());
	}

	public IEnumerator SyncLevelsFromServer()
	{
		var cloud = new ES3Cloud("http://garagekitgames.com/ES3Cloud.php", "d0626d40cd72");


		yield return StartCoroutine(cloud.DownloadFilenames());

		if (cloud.isError)
			Debug.LogError(cloud.error);

        levelCount = cloud.filenames.Length;
        GameManager.Instance.LevelSetupMode = GameManager.GameItemSetupMode.Automatic;
        GameManager.Instance.NumberOfAutoCreatedLevels = levelCount-1;

        foreach (var filename in cloud.filenames)
		{
			Debug.Log(filename);
			//levelNames.Add(filename);
			yield return StartCoroutine(cloud.DownloadFile(filename));
		}


		//foreach (var levelName in levelNames)
		//{
		//    Debug.Log(levelName);
		//    yield return StartCoroutine(cloud.Sync(levelName));
		//}
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
