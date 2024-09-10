using UnityEngine;
using System.IO;
using System;
public class DungeonFileLoader{

    private readonly string _filePath;

    public DungeonFileLoader(string filePath){
        _filePath=filePath;
    }

    public string LoadDungeonsFile(){
    if (!File.Exists(_filePath))
        {
            Debug.LogError("El archivo no existe en la ruta especificada: " + _filePath);
            return null;
        }

        try
        {
            return File.ReadAllText(_filePath);;
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error al cargar el archivo JSON: " + ex.Message);
            return null;
        }
    }


}