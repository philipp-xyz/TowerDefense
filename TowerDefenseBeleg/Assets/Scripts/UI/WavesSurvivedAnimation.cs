using System.Collections;
using TMPro;
using UnityEngine;

public class WavesSurvivedAnimation : MonoBehaviour {
    
    [Header("General")]
    
    [Tooltip("The text element for displaying how many waves survived.")]
    [SerializeField] private TMP_Text wavesSurvivedText;
    
    private void OnEnable() {
        StartCoroutine(AnimateText());
    }
    
    // coroutine for animating the text
    private IEnumerator AnimateText() {
        wavesSurvivedText.text = "0";
        int wave = 0;

        yield return new WaitForSeconds(0.7f);

        // counting up and updating text
        while (wave < GameManager.Waves) {
            wave++;
            wavesSurvivedText.text = wave.ToString();
            // wait between updating wave numbers
            yield return new WaitForSeconds(0.10f);
        }
    }
    
}
