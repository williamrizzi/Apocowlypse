using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    static GameManagerScript _intancie;
    public static GameManagerScript Instancie
    {
        get { return _intancie; }
    }

    public bool blockSpawns;

    public GameObject[] blocksPrefabs;
    public int mapSize;

    public GameObject aerolitoPrefab;
    public GameObject alienPrefab;
    public GameObject tornadoPrefab;

    public float aerolitoStartCD;
    public float aerolitoStartDur;
    public float aerolitoMin;
    public float aerolitoMax;
    private float aerolitoNext;
    public float alienStartCD;
    public float alienStartDur;
    public float alienMin;
    public float alienMax;
    private float alienNext;
    public float tornadoStartCD;
    public float tornadoStartDur;
    public float tornadoMin;
    public float tornadoMax;
    private float tornadoNext;
    private bool isStart;
    private float startEnd;

    private GameObject cam;

    private GameObject player;

    private GameObject end;

    private Slider slider;

    public List<GameObject> enemys;

    [SerializeField]
    Text vancasPercent;

    public GameObject painel;

    [SerializeField]
    int _vacasWithPlayer = 0;
    public int VacasWithPlayer
    {
        get { return _vacasWithPlayer; }
        set { _vacasWithPlayer = value; }
    }

    [SerializeField]
    int countVacasInScene = 2;

    [SerializeField]
    Vector2 _startPositionBlocks;

    [SerializeField]
    GameObject _vacaPrefeb;

    [SerializeField]
    int maxVacaSpawByBlock;

    void Awake()
    {
        _intancie = this;
    }

    private void Start()
    {
        mapSize = Random.Range(5, 6);

        if (FadeImage.Instance != null)
        {
            FadeImage.Instance.FadeIn();
        }

        blockSpawns = true;
        cam = Camera.main.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        List<GameObject> blocksInst = new List<GameObject>();
        isStart = true;

        for (int i = 0; i < mapSize; i++)
        {
            Vector2 __pos;

            if (i == 0)
            {
                __pos = _startPositionBlocks;
                blocksInst.Add(Instantiate(blocksPrefabs[0], __pos, Quaternion.identity));

            }
            else if (i >= 0 && i <= mapSize - 2)
            {
                Vector2 __tempPos = new Vector2(blocksInst[i - 1].GetComponent<BlockClass>().Ground.bounds.size.x +
                    blocksInst[i - 1].transform.GetChild(0).transform.position.x, _startPositionBlocks.y);

                __pos = new Vector2(__tempPos.x, _startPositionBlocks.y); ;
                blocksInst.Add(Instantiate(blocksPrefabs[Mathf.FloorToInt(Random.Range(2, blocksPrefabs.Length - 1))], __pos, Quaternion.identity));

                int randomQtdVaca = Random.Range(1, maxVacaSpawByBlock);

                for (int j = 0; j < randomQtdVaca; j++)
                {
                    Vector2 __tempSpawPosition = new Vector2(
                        blocksInst[i].GetComponent<BlockClass>().SpawsVacas[Random.Range(1, blocksInst[i].GetComponent<BlockClass>().SpawsVacas.Length - 1)].transform.position.x + Random.Range(-5, 5),
                        blocksInst[i].GetComponent<BlockClass>().SpawsVacas[Random.Range(1, blocksInst[i].GetComponent<BlockClass>().SpawsVacas.Length - 1)].transform.position.y + Random.Range(-0.8f, 0.8f));

                    Instantiate(_vacaPrefeb, __tempSpawPosition, Quaternion.identity);
                    countVacasInScene++;
                }
            }

            else if (i > mapSize - 2)
            {
                Vector2 __tempPos = new Vector2(blocksInst[i - 1].GetComponent<BlockClass>().Ground.bounds.size.x +
                    blocksInst[i - 1].transform.GetChild(0).transform.position.x, _startPositionBlocks.y);

                __pos = new Vector2(__tempPos.x, _startPositionBlocks.y); ;
                blocksInst.Add(Instantiate(blocksPrefabs[1], __pos, Quaternion.identity));
            }

        }

        end = GameObject.Find("End");

        if (end)
        {
            end.transform.position = new Vector2(mapSize * 10.0f - 5.0f, 0.0f);
        }
    }

    private void Update()
    {
        if (blockSpawns == false)
        {
            if (!isStart)
            {
                if (Time.time >= aerolitoNext)
                {
                    SpawnAerolito();
                    aerolitoNext = Time.time + Random.Range(aerolitoMin, aerolitoMax);
                }
                if (Time.time >= alienNext)
                {
                    SpawnAlien();
                    alienNext = Time.time + Random.Range(alienMin, alienMax);
                }
                if (Time.time >= tornadoNext)
                {
                    SpawnTornado();
                    tornadoNext = Time.time + Random.Range(tornadoMin, tornadoMax);
                }
            }
            else
            {
                if (Time.time >= aerolitoNext)
                {
                    SpawnAerolito();
                    aerolitoNext = Time.time + aerolitoStartCD;
                }
                if (Time.time >= alienNext)
                {
                    SpawnAlien();
                    alienNext = Time.time + alienStartCD;
                }
                if (Time.time >= tornadoNext)
                {
                    SpawnTornado();
                    tornadoNext = Time.time + tornadoStartCD;
                }
                if (Time.time >= startEnd)
                {
                    isStart = false;
                }
            }
        }

        slider.value = player.transform.position.x / end.transform.position.x;

        vancasPercent.text = MovementScript.Instance.CowList.Count + " / " + countVacasInScene;
    }

    private void SpawnAerolito()
    {
        Vector3 pos = new Vector3
        {
            x = Random.Range(player.transform.position.x + 5.0f, player.transform.position.x + 9.0f),
            y = Random.Range(-4.5f, 1.5f),
            z = 0.0f
        };
        enemys.Add(Instantiate(aerolitoPrefab, pos, Quaternion.identity));
    }

    private void SpawnAlien()
    {
        Vector3 pos = new Vector3
        {
            x = cam.transform.GetChild(3).position.x,
            y = Random.Range(-2.0f, 3.7f),
            z = 0.0f
        };
        enemys.Add(Instantiate(alienPrefab, pos, Quaternion.identity));
    }

    private void SpawnTornado()
    {
        Vector3 pos = new Vector3
        {
            x = cam.transform.position.x - 9.0f,
            y = 0.0f,
            z = 0.0f
        };
        enemys.Add(Instantiate(tornadoPrefab, pos, Quaternion.identity, cam.transform));
    }

    public void StartSpawning()
    {
        blockSpawns = false;
        aerolitoNext = Time.time + aerolitoStartCD;
        alienNext = Time.time + alienStartCD + aerolitoStartDur;
        tornadoNext = Time.time + tornadoStartCD + aerolitoStartDur + alienStartDur;
        startEnd = Time.time + aerolitoStartDur + alienStartDur + tornadoStartDur;
    }

    public void DestroyAllEnemys()
    {
        foreach(GameObject go in enemys)
        {
            Destroy(go);
        }
        enemys.Clear();
    }
}
