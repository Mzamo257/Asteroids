using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public static class Reader {

	#region Public Attributes
	#endregion

	#region Private Attributes
	#endregion

	#region MonoDevelop Methods
	#endregion

	#region User Methods
    // TODO: Hacer funciones diferentes para los diferentes niveles
	public static BaseLevelData getDataFromXML(string level_Name)
	{
		BaseLevelData level = new BaseLevelData ();
		XmlDocument xml_doc = new XmlDocument();
		xml_doc.Load ("Assets/Resources/level.xml");
		XmlNode level_info;


		level_info = xml_doc.SelectSingleNode ("LEVELS/SURVIVAL/LEVEL[@name='" +level_Name+ "']");
		if (level_info != null) 
		{
			
			level.force_Spaceship = int.Parse(((XmlElement)level_info).GetElementsByTagName ("VARIABLE")[0].InnerText);
			level.max_Speed_Spaceship = int.Parse(((XmlElement)level_info).GetElementsByTagName ("VARIABLE")[1].InnerText);
			level.numberOf_Waypoints = int.Parse(((XmlElement)level_info).GetElementsByTagName ("VARIABLE")[2].InnerText);
			level.numberOf_Asteroids = int.Parse(((XmlElement)level_info).GetElementsByTagName ("VARIABLE")[3].InnerText);
			level.force_Asteroids = int.Parse(((XmlElement)level_info).GetElementsByTagName ("VARIABLE")[4].InnerText);
			level.lifeKit = int.Parse(((XmlElement)level_info).GetElementsByTagName ("VARIABLE")[5].InnerText);


		}

		return level;

	}
	#endregion
}
