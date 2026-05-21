using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObjetoRecolectable : MonoBehaviour
{
    // ── Audio ─────────────────────────────────────────────────────────────────
    [Header("Movimiento")]
    public float floatSpeed = 2f;
    public float floatHeight = 0.5f;
    private Vector3 startPos;

    [Header("Referencias de Audio")]
    [Tooltip("AudioSource para el sonido de ocio (idle). Configúralo en el Inspector.")]
    public AudioSource audioSourceOcio;

    [Tooltip("AudioSource para el sonido de recolección. Configúralo en el Inspector.")]
    public AudioSource audioSourceRecoleccion;

    [Header("Sonido de Ocio - Proximidad")]
    [Tooltip("Distancia máxima desde la cual el jugador comienza a escuchar el sonido de ocio.")]
    public float distanciaMaxima = 15f;

    [Tooltip("Distancia mínima a la que el volumen del sonido de ocio llega al máximo.")]
    public float distanciaMinima = 2f;

    [Tooltip("Volumen máximo del sonido de ocio cuando el jugador está muy cerca.")]
    [Range(0f, 1f)]
    public float volumenMaxOcio = 1f;

    private Transform jugadorTransform;
    private bool recolectado = false;

    void Start()
    {
        // Buscar al jugador por tag
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
            jugadorTransform = jugador.transform;
        else
            Debug.LogWarning("[ObjetoRecolectable] No se encontró un GameObject con tag 'Player'.");

        // Iniciar sonido de ocio en loop
        if (audioSourceOcio != null)
        {
            audioSourceOcio.loop = true;
            audioSourceOcio.volume = 0f;
            audioSourceOcio.spatialBlend = 0f;
            audioSourceOcio.Play();
        }
        else
        {
            Debug.LogWarning($"[ObjetoRecolectable] '{gameObject.name}': No tiene asignado audioSourceOcio.");
        }
        startPos = transform.position;
    }

    void Update()
    {
        if (recolectado) return;
        // 2. Volumen del sonido de ocio según distancia
        if (jugadorTransform != null && audioSourceOcio != null)
        {
            float distancia = Vector3.Distance(transform.position, jugadorTransform.position);
            audioSourceOcio.volume = CalcularVolumenPorDistancia(distancia);
        }

        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private float CalcularVolumenPorDistancia(float distancia)
    {
        if (distancia >= distanciaMaxima) return 0f;
        if (distancia <= distanciaMinima) return volumenMaxOcio;
        float t = 1f - ((distancia - distanciaMinima) / (distanciaMaxima - distanciaMinima));
        return Mathf.Lerp(0f, volumenMaxOcio, t);
    }

    private void OnTriggerEnter(Collider otro)
    {
        if (recolectado) return;
        if (otro.CompareTag("Player"))
            Recolectar();
    }

    private void Recolectar()
    {
        recolectado = true;

        if (audioSourceOcio != null)
            audioSourceOcio.Stop();

        if (audioSourceRecoleccion != null && audioSourceRecoleccion.clip != null)
            AudioSource.PlayClipAtPoint(audioSourceRecoleccion.clip, transform.position, 0.1f);

        if (GestorRecolectables.Instancia != null)
            GestorRecolectables.Instancia.RegistrarRecoleccion();
        else
            Debug.LogError("[ObjetoRecolectable] No se encontró GestorRecolectables en la escena.");

        Destroy(gameObject, 0f);
    }
}
