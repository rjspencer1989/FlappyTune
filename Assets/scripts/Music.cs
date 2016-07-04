using UnityEngine;

using System.Collections;

using System.Xml;

class MusicParser : MonoBehaviour{
    XmlDocument getXml(string name){
        TextAsset asset = Resources.Load(name) as TextAsset;
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(asset.text);
        return doc;
    }

    void start(){
        XmlDocument music = getXml("star");
        
    }
}