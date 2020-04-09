using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using GameFramework.GameStructure;
using System.IO;

public class LevelGenerator : UnitySingleton<LevelGenerator>
{
	public Texture2D map;

    public Color startPoint;

    public ColorToPrefab[] colorMappings;

    public Transform player;
    public Transform playerTarget;
    public AstarPath path;
    public int enemyCount;

    public Color[] groundColors;

    public Material groundMaterial;

    public Color[] wallColors;
    public Material wallMaterial;

    public Material enemyMaterial;

    public Transform[] startingPositions;
    public int startingPos;
    public GameObject[] rooms;

    public List<string> levelNames;


    private int direction;
    // Start is called before the first frame update
    private void Awake()
    {

        //StartCoroutine(UploadToCloud());

        //StartCoroutine(GetFileNames());
       // StartCoroutine(SyncLevelsFromServer());

        enemyCount = 1;

        string dataPath = Application.persistentDataPath;
        string filename = "Map_" + GameManager.Instance.Levels.Selected.Number + ".png";

        filename = dataPath + "/" + filename;

        if(File.Exists(filename))
        {
            byte[] imageData = File.ReadAllBytes(filename);
            map = new Texture2D(1, 1, TextureFormat.RGBA32, false, false);//,TextureFormat.ETC2_RGBA8,true);
            //map.filterMode = FilterMode.Point;
            map.anisoLevel = 0;

            map.filterMode = FilterMode.Point;

            map.LoadImage(imageData);
            
            
        }
        else
        {
            map = Resources.Load("BrutalBeatdown/Levels_2/Map_" + GameManager.Instance.Levels.Selected.Number, typeof(Texture2D)) as Texture2D;

        }
        //map = Resources.Load("BrutalBeatdown/Levels_2/Map_"+ GameManager.Instance.Levels.Selected.Number, typeof(Texture2D)) as Texture2D;
    }

    public IEnumerator GetFileNames()
    {
        // Create a new ES3Cloud object with the URL to our ES3Cloud.php file.
        var cloud = new ES3Cloud("http://garagekitgames.com/ES3Cloud.php", "d0626d40cd72");

        // Download the filenames of all global files on the server and output them to console.
        yield return StartCoroutine(cloud.DownloadFilenames());

        if (cloud.isError)
            Debug.LogError(cloud.error);

        foreach (var filename in cloud.filenames)
        {
            Debug.Log(filename);
            levelNames.Add(filename);
        }
            

    }

    public IEnumerator SyncLevelsFromServer()
    {
        var cloud = new ES3Cloud("http://garagekitgames.com/ES3Cloud.php", "d0626d40cd72");


        yield return StartCoroutine(cloud.DownloadFilenames());

        if (cloud.isError)
            Debug.LogError(cloud.error);

        foreach (var filename in cloud.filenames)
        {
            Debug.Log(filename);
            levelNames.Add(filename);
            yield return StartCoroutine(cloud.Sync(filename));
        }


        //foreach (var levelName in levelNames)
        //{
        //    Debug.Log(levelName);
        //    yield return StartCoroutine(cloud.Sync(levelName));
        //}
    }

    public IEnumerator UploadToCloud()
    {

        var cloud = new ES3Cloud("http://garagekitgames.com/ES3Cloud.php", "d0626d40cd72");

        yield return StartCoroutine(cloud.Sync("Map_1.png"));

        yield return StartCoroutine(cloud.UploadFile("Map_1.png"));

        if (cloud.isError)
            Debug.LogError(cloud.error);


    }
    void Start()
    {


        player = GameObject.FindGameObjectWithTag("CamTarget_Player").transform;
        playerTarget = FindObjectOfType<TargetMarker>().transform;


        GenerateLevel();

        //if (GameManager.Instance.Levels.Selected.Number <= 25)
        //{
        //    GenerateLevel();
        //}
        //else
        //{
        //    startingPos = Random.Range(0, startingPositions.Length);

        //    transform.position = startingPositions[startingPos].position;

        //    Instantiate(rooms[0], transform.position, Quaternion.identity);

        //    direction = Random.Range(1, 6);
        //}
        






        Random.InitState(GameManager.Instance.Levels.Selected.Number);
        
        wallMaterial.color = wallColors[Random.Range(0, wallColors.Length)];// new Color(Random.value, Random.value, Random.value, 1.0f);
        groundMaterial.color = groundColors[Random.Range(0, groundColors.Length)];  //new Color(Random.value, Random.value, Random.value, 1.0f);
        AstarPath.active.Scan();
        path.Scan();
        
        
    }



    public void GenerateLevel()
    {
        enemyCount = 0;
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    public void GenerateTile(int x, int z)
    {
        Color pixelColor = map.GetPixel(x, z);

        if(pixelColor.a == 0)
        {
            //pixel is transparent no data
            return;
        }
        else if (pixelColor.Equals(startPoint))
        {
            Vector3 position = new Vector3(x, 1, z);
            player.transform.position = position;
            playerTarget.position = position;
        }

        foreach (var colorMapping in colorMappings)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x, 1, z);
                if(colorMapping.parent)
                {
                    Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(colorMapping.prefab, position, Quaternion.identity);
                }

                if(colorMapping.prefabType == ColorToPrefab.PrefabType.Enemy)
                {
                    enemyCount++;
                }
                
            }
            
        }

        //Debug.Log(ColorUtility.ToHtmlStringRGBA(pixelColor));

    }
}
