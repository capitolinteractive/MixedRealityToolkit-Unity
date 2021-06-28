using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicHolder : MonoBehaviour
{
    public List<Material> Pics;

    public List<AudioClip> AudClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPic(Material x)
    {
        Pics.Add(x);
    }

    public void AddClip(AudioClip x)
    {
        AudClip.Add(x);
    }

    public void LoadPics()
    {

    }
}
