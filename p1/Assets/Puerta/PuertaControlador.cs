using UnityEngine;

public class PuertaControlador : MonoBehaviour
{
    public Animator PuertaAnimator;
    private bool abierta = false;

    //open dor pli
    [ContextMenu("Abrilo pelotudo")]
    public void AbrirPuerta() {
        print("abrio la puelta");
        PuertaAnimator.SetBool("Abrir",true);
    }
    //ta cerra la puelta
    [ContextMenu("Tenes que cerrar la puerta")]
    public void CerrarPuerta()
    {
        print("cerro la puelta");
        PuertaAnimator.SetBool("Abrir",false);
    }
    //puerta afk
    [ContextMenu("la puelta")]
    public void PuertaCerrada()
    {
        print("puelta dolmida");
    }

    public void TogglePuerta()
    {
        abierta = !abierta;
        if (abierta)
        {
            CerrarPuerta();
        }
        else
        {
            AbrirPuerta();
        }
    }
}
