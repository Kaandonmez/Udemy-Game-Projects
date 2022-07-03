using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;

    float movementFactor;
    [SerializeField] float period = 2f;


    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon/*En küçük float sayı*/) { return; } // NaN error den kurtulmak için
        float cycles = Time.time / period; //gittikçe büyüyor
        const float tau = Mathf.PI * 2; //6.283'ün const değeri
        float rawSinWave = Mathf.Sin(cycles * tau); //-1 den 1 e gidiş
        movementFactor = (rawSinWave + 1f) / 2f; //  recalc çünkü 0 dan 1 e geçiş daha temiz bir görünüm için
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}