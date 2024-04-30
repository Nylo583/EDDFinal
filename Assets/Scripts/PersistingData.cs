using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistingData : MonoBehaviour
{
    public static PersistingData Instance;

    public string NAME { get; set; }
    public int SCORE { get; set; }

    private void Awake() {
        if (Instance is not null) {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
