using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public List<AudioClip> AudioKu = new List<AudioClip>();
    float[] playtime = new float[100];
    Dictionary<string, int> Dic = new Dictionary<string, int>();
    AudioSource myaudio;

    public static AudioController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        myaudio = GetComponent<AudioSource>();
        for (int i = 0; i < AudioKu.Count; i++)
        {//自动为每个音效取名
            Dic.Add(AudioKu[i].name, i);
        }
    }

    public void PlayAudio(string aname)
    {
        if (Time.time - playtime[Dic[aname]] < 0.1f) return;
        playtime[Dic[aname]] = Time.time;
        myaudio.PlayOneShot(AudioKu[Dic[aname]]);
    }

    public void PlayAudio(AudioClip ad)
    {
        myaudio.PlayOneShot(ad);
    }

}
