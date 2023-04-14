using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewritterEffect : MonoBehaviour
{
    float speed = 20f;

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        //Devuelve la corrutina que escribe
        return StartCoroutine(TypeText(textToType, textLabel));
    }
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        float t = 0;
        //el numero de caracteres escritos
        int charIndex = 0;
        while (charIndex < textToType.Length)
        {
            t += Time.deltaTime * speed;
            //esto hace que cualquier número con decimal siempre este redondeado al minimo. eg: 2.6 ----> 2
            //con el clamp obligamos a que no se pase de numero de caracteres
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            //esto escribira el texto
            textLabel.text = textToType.Substring(0, charIndex);
            yield return null;
        }
        textLabel.text = textToType;
    }
}
