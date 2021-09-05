using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;

public class DataLoadandSave : MonoBehaviour
{
    private void Start()
    {
        CreateXml();
    }

    void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

         
    }
}
