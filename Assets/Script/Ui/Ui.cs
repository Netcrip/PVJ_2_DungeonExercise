using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    private Vector2 _scrollPosition;
    private int _selectedIndex = -1; // Índice del ítem seleccionado
    private string[] _items = new string[11]; // Array de ítems
    private DungeonGenerator _generator;
    
    private void Start()
    {
        _generator = GameObject.FindFirstObjectByType<DungeonGenerator>().GetComponent<DungeonGenerator>();
        // Inicializar los ítems
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i] = $"Item {i + 1}";
        }
    }

    private void OnGUI()
    {
       
        //botones
        float w = Screen.width / 2;   // Posición central en el eje X
        float h = Screen.height - 80; // Posición en el eje Y
        float xPos = Screen.width - 260; // 10 píxeles de margen desde el borde derecho    

        if (GUI.Button(new Rect(w - 130, h, 250, 50), "Regenerate Dungeon"))
        {
            RegenerateDungeon();
        }

        
        if (GUI.Button(new Rect(w + 130, h, 250, 50), "Save Dungeon"))
        {
            SaveDungeon();
        }

        if (GUI.Button(new Rect(xPos, 180, 250, 50), "Load Dungeon"))
        {
            LoadDungeon();
        }


        // panel de scroll
        GUILayout.BeginArea(new Rect(xPos, 10, 250, 150), GUI.skin.box); // Recuadro con bordes
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(250), GUILayout.Height(150));

        // Crear botones individuales para cada ítem
        for (int i = 0; i < _items.Length; i++)
        {
            // Cambiar el estilo de la etiqueta si está seleccionada
            GUIStyle estiloLabel = new GUIStyle(GUI.skin.label);
            if (i == _selectedIndex)
            {
                estiloLabel.normal.textColor = Color.red; // Cambiar el color del texto del ítem seleccionado
                estiloLabel.fontStyle = FontStyle.Bold;   // Cambiar a texto en negrita
            }

            // Crear la etiqueta
            GUILayout.Label(_items[i], estiloLabel, GUILayout.Width(230));

            // Detectar clics en la etiqueta
            Rect lastRect = GUILayoutUtility.GetLastRect();
            if (Event.current.type == EventType.MouseDown && lastRect.Contains(Event.current.mousePosition))
            {
                _selectedIndex = i; // Actualizar el índice del ítem seleccionado
                Debug.Log("Ítem seleccionado: " + _items[_selectedIndex]);
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();

    }

    void RegenerateDungeon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void LoadDungeon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void SaveDungeon()
    {
        _generator.SaveDungeon();
    }
}
