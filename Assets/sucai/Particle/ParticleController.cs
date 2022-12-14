using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    
    //颜色预制
    public Color FireCol, WaterCol, PoisionCol;
    public Color ElemColor(int elem) {
        if(elem == 200) return FireCol;
        if(elem == 300) return WaterCol;
        if(elem < 100) return PoisionCol;
        return new Color(255, 255, 255, 255) / 255;
    }


    public List<ParticleSystem> Parti = new List<ParticleSystem>();
    Dictionary<string, int> Dic = new Dictionary<string, int>();

    public static ParticleController Instance { get; private set; }
    private void Awake() {
        Instance = this;
        for(int i = 0; i < Parti.Count; i++) {//自动为每个粒子取名
            Dic.Add(Parti[i].name, i);
        }
    }

    public ParticleSystem AssignParticle(Transform t, string parname) {
        
        ParticleSystem par = Instantiate(Parti[Dic[parname]], t.position, t.rotation);
        par.transform.localScale = t.localScale;
        StartCoroutine(Following(par, t));
        return par;
    }//按目标分配粒子，粒子将跟随该目标直至其消亡

    public ParticleSystem AssignParticle(Vector3 pos, float rotation, string parname) {
        ParticleSystem par = Instantiate(Parti[Dic[parname]], pos, Quaternion.Euler(new Vector3(0, 0, rotation)));
        Destroy(par.gameObject, 0.5f); ;
        return par;

    }//给定位置和方向，生成一次

    IEnumerator Following(ParticleSystem par, Transform tar) {
        Transform tpar = par.transform;
        while(tar != null) {
            if(tpar == null) yield break;
            tpar.position = tar.position;
            tpar.rotation = tar.rotation;
            yield return new WaitForSeconds(0.02f);
        }
        var emi = par.emission;
        emi.rateOverTime = 0;
        Destroy(par.gameObject, 1f);
    }


}
