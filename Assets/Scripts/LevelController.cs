using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelController : MonoBehaviour
{
    private int watchersTotal;
    private float lastUpdated;
    private int life;
    private ParticleSystem particle;
    private float lastRecordedParticleStatus;
    private int lastParticleState;
    private int curIndex;
    private List<SpriteRenderer> chairs = new List<SpriteRenderer>();

    public float sitRatio = 0.5f;
    public float secondsToLoseLife = 0.5f;
    public float secondsToBotherWatcher = 2.2f;
    public int remainingWatchers;
    public int levelID = 1;

    void Start()
    {
        life = 100;
        lastUpdated = Time.time;
        lastRecordedParticleStatus = Time.time;
        lastParticleState = 0;
        curIndex = 0;

        SpriteRenderer[] gos = GameObject.FindObjectsOfType<SpriteRenderer>();

        foreach (var x in gos)
        {
            if (x.name.StartsWith("Chair", System.StringComparison.InvariantCulture))
            {
                chairs.Add(x);
            }
        }

        watchersTotal = (int)((float)chairs.Count * sitRatio);
        Shuffle(chairs);

        for(int i = 0; i < watchersTotal; i++)
        {
            var oldSprite = chairs[i].sprite;
            var texture = LoadTexture("Assets/Sprites/ChairSitted.png", oldSprite.texture.height, oldSprite.texture.width);
            chairs[i].sprite = Sprite.Create(
                texture,
                oldSprite.rect,
                oldSprite.pivot,
                oldSprite.pixelsPerUnit);
            chairs[i].transform.position += new Vector3(4.96f, 4.96f, 0);
        }

        var whistler = GameObject.Find("Whistler");
        particle = whistler.gameObject.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastUpdated + secondsToLoseLife)
        {
            lastUpdated = Time.time;
            life -= 1;
        }

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
            Debug.Log("Win");
            Application.LoadLevel(levelID + 1);
        }
        else if (life == 0)
        {
            Debug.Log("Lost");
            Application.LoadLevel(levelID);
        }
    }


    public void Shuffle(List<SpriteRenderer> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            SpriteRenderer temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private Texture2D LoadTexture(string FilePath, int h, int w)
    {

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(h, w);
            if (Tex2D.LoadImage(FileData))
                return Tex2D;
        }
        return null;
    }

    private void BotherWatcher()
    {
        var oldSprite = chairs[curIndex].sprite;
        var texture = LoadTexture("Assets/Sprites/Chair.png", oldSprite.texture.height, oldSprite.texture.width);
        chairs[curIndex].sprite = Sprite.Create(
            texture,
            oldSprite.rect,
            oldSprite.pivot,
            oldSprite.pixelsPerUnit);
        chairs[curIndex].transform.position += new Vector3(158.72f, 158.72f, 0);

        curIndex++;
    }
}
