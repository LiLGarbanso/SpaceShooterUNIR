using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    [SerializeField] private string nivel;
    public ParticleSystem pSys;
    public GameObject sp1, sp2, txt, player;
    public AudioClip sndANim;
    public Animator animator;
    public bool isExit;

    public void CargarEscena()
    {
        SceneManager.LoadScene(nivel);
        //SceneManager.LoadSceneAsync(nivel)
    }

    public void Sound()
    {
        SoundMannager.Instance.PararSonido();
        SoundMannager.Instance.PlayMusic(sndANim);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (isExit)
                {
                    Application.Quit();
                }
                else
                {
                    pSys.Play();
                    sp1.SetActive(false);
                    sp2.SetActive(false);
                    txt.SetActive(false);
                    player.SetActive(false);
                    animator.enabled = true;
                }
            }
        }
    }
}
