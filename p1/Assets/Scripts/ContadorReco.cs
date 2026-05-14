using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorReco : MonoBehaviour
{
    public static ContadorReco instance;
    public int recolectar = 0;
    public int total = 1;

    public TextMeshProUGUI ContadorRecos;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ActualizarUI();
    }
    public void recolectarLlave()
    {
        recolectar++;
        Debug.Log("Llave: " + recolectar);

        ActualizarUI();
    }

    void ActualizarUI()
    {
        ContadorRecos.text = recolectar + " / " + total;
    }
}
