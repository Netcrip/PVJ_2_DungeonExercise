using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    private Vector2 _scrollPosition;
    private int _selectedIndex = -1; 
    private List<string> _items = new List<string>(); 
    private DungeonGenerator _generator;
    private Dictionary<string, MyDungeons> myDungeons = new Dictionary<string, MyDungeons>();

    private void Awake()
    {
        _generator = GameObject.FindFirstObjectByType<DungeonGenerator>().GetComponent<DungeonGenerator>();
        ChargeMyDungeons();
        UpdateItemList();
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
        GUILayout.BeginArea(new Rect(xPos, 10, 250, 150), GUI.skin.box);
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(250), GUILayout.Height(150));

        for (int i = 0; i < _items.Count; i++) 
        {
            GUIStyle estiloLabel = new GUIStyle(GUI.skin.label);
            if (i == _selectedIndex)
            {
                estiloLabel.normal.textColor = Color.red;
                estiloLabel.fontStyle = FontStyle.Bold;
            }

            GUILayout.Label(_items[i], estiloLabel, GUILayout.Width(230));

            Rect lastRect = GUILayoutUtility.GetLastRect();
            if (Event.current.type == EventType.MouseDown && lastRect.Contains(Event.current.mousePosition))
            {
                _selectedIndex = i;
                Debug.Log("Ítem seleccionado: " + _items[_selectedIndex]);
            }
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    private void RegenerateDungeon()
    {
        GameManager.Instance.Reload();
    }

    private void ChargeMyDungeons()
    {
        myDungeons = _generator.DungeonLoad();
    }

    private void SaveDungeon()
    {
        _generator.SaveDungeon();
        ChargeMyDungeons(); 
        UpdateItemList();   
    }

    // Actualizar la lista de items en la UI
    private void UpdateItemList()
    {
        _items.Clear(); // Limpia la lista 
        if(myDungeons !=null)
            foreach (var data in myDungeons)
            {
                _items.Add(data.Key);
            }
    }

    private void LoadDungeon(){
        if(_selectedIndex>=0)
            GameManager.Instance.ChargeDungeon(myDungeons[_items[_selectedIndex]]);
    }
    
}
