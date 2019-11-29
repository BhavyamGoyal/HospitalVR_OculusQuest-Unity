using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using HCFramework;

public class TextAnimation : MonoBehaviour
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.AddListner<NextTutorial>(NextTut);
      //  StartTyping(fullText1);
    }
    public void NextTut(NextTutorial data)
    {
        StartTyping(data.text,data.textMesh);

    }
    void Update()
    {

    }

    public async void StartTyping(string text,TextMeshProUGUI textMesh)
    {

        string outputText = "";
        int i = 0;
        while (i < text.Length)
        {
            outputText = outputText + text.ToCharArray()[i];
            textMesh.text = outputText;
            await Task.Delay(60);
            i++;
        }
    }
}
