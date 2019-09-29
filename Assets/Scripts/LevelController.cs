using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelController : MonoBehaviour
{
    private int watchersTotal;
    private float lastUpdated;
    private ParticleSystem particle;
    private float lastRecordedParticleStatus;
    private int lastParticleState;
    private int curIndex;
    private List<Animator> chairs = new List<Animator>();

    public float sitRatio = 0.5f;
    public float secondsToBotherWatcher = 1f;
    public int remainingWatchers;
    public int levelID = 1;

    void SetupAnimators()
    {
        Animator[] anms = GameObject.FindObjectsOfType<Animator>();
        foreach (var x in anms)
        {
            if (x.name.StartsWith("Chair", System.StringComparison.InvariantCulture))
            {
                chairs.Add(x);
            }
        }
    }

    void Start()
    {
        lastUpdated = Time.time;
        lastRecordedParticleStatus = Time.time;
        lastParticleState = 0;
        curIndex = 0;

        SetupAnimators();

        watchersTotal = (int)((float)chairs.Count * sitRatio);
        Shuffle(chairs);

        for(int i = 0; i < watchersTotal; i++)
        {
            chairs[i].Play("ChairChanging", 0, 0.5f);
        }

        var whistler = GameObject.Find("Whistler");
        particle = whistler.gameObject.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (Time.time >= lastRecordedParticleStatus + secondsToBotherWatcher)
        {
            lastRecordedParticleStatus = Time.time;
            if (particle.isPlaying && lastParticleState == 1)
            {
                BotherWatcher();
            }

            lastParticleState = particle.isPlaying ? 1 : 0;
        }

        remainingWatchers = (int)((float)chairs.Count * sitRatio) - curIndex;
        if (remainingWatchers == 0)
        {
            Modal.MessageBox("Yes!", "Your noise is remarkable!", () => {
                Application.LoadLevel(levelID + 1);
              });
        }
    }


    public void Shuffle(List<Animator> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Animator temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private Texture2D LoadTexture(string FilePath, float height, float width)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(FilePath))
        {
            fileData = File.ReadAllBytes(FilePath);
            tex = new Texture2D((int)height, (int)width);
            tex.LoadImage(fileData);
        }
        return tex;
    }

    private void BotherWatcher()
    {
        chairs[curIndex].Play("ChairChanging", 0, 0f);
        curIndex++;
    }
}
