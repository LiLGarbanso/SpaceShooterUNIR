using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrera : MonoBehaviour
{
    private int currentHP;
    [SerializeField] private int maxHP;
    public AudioClip sfxGolpe;
    private SpriteRenderer spRend;
    private Collider2D col;
    public List<Sprite> sprites;
    public ParticleSystem pSys;

    private void Start()
    {
        
    }

    public void Init()
    {
        spRend = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        currentHP = maxHP;
        spRend.sprite = sprites[0];
        col.enabled = true;
        spRend.enabled = true;
    }

    public void TakeDMG(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            StartCoroutine(Desactivar());
        }
        else
        {
            SoundMannager.Instance.PlaySFX(sfxGolpe);
            pSys.Play();

            if (currentHP < (maxHP / 4) * 3)
                spRend.sprite = sprites[1];

            if (currentHP < maxHP / 2)
                spRend.sprite = sprites[2];

            if (currentHP < maxHP / 4)
                spRend.sprite = sprites[3];
        }
    }

    IEnumerator Desactivar()
    {
        pSys.Play();
        col.enabled = false;
        spRend.enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
