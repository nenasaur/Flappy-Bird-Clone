using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool yVariable;

    // Start is called before the first frame update
    void Start()
    {
        float randomY = Random.Range(-2f, 2f);
        transform.position = new Vector2(transform.position.x, randomY);

        // Probabilidade de oscilar
        yVariable = Random.Range(-1, 5) < 0;
        if (yVariable) {
            OscilateY();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(
            transform.position.x - speed * Time.deltaTime,
            transform.position.y
        );
    }

    void OscilateY()
    {
        // Pontuação mínima para oscilar
        if (GameController.instance.GetScore() < 10) return;

        float variation = 0.08f;
        float newY = transform.position.y + (yVariable ? variation : -variation);
        if (newY > 2 || newY < -2)
        {
            yVariable = !yVariable;
            newY = transform.position.y + (yVariable ? variation : -variation);
        }
        transform.position = new Vector2(transform.position.x, newY);
        Invoke("OscilateY", 0.05f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DestroyPoint"))
        {
            Destroy(gameObject);
        }
    }
}