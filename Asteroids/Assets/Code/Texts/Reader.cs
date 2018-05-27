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
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
	public static List<SurvivalLevelData> getSurvivalLevelDataFromXML(/*string level_Name*/)
	{
        List<SurvivalLevelData> levels = new List<SurvivalLevelData>();
		XmlDocument xml_doc = new XmlDocument();
		xml_doc.Load ("Assets/Resources/level.xml");
		//XmlNode level_info;
        XmlNodeList levelsInfo;


        //level_info = xml_doc.SelectSingleNode ("LEVELS/SURVIVAL/LEVEL[@name='" +level_Name+ "']");
        //levelsInfo = xml_doc.SelectNodes("LEVELS/SURVIVAL/LEVEL[@name='" + level_Name + "']");
        levelsInfo = xml_doc.SelectNodes("LEVELS/SURVIVAL/LEVEL");
        // Debug.Log(levelsInfo.Count);

        if (levelsInfo != null) 
		{
            foreach (XmlNode nextLevelInfo in levelsInfo)
            {
                //XmlNode nextLevelInfo = levelsInfo.Item(i);
                SurvivalLevelData level = new SurvivalLevelData();
                level.force_Spaceship = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[0].InnerText);
                level.max_Speed_Spaceship = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[1].InnerText);
                level.numberOfWaypoints = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[2].InnerText);
                level.numberOf_Asteroids = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[3].InnerText);
                level.force_Asteroids = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[4].InnerText);
                level.numberOfLifeKits = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[5].InnerText);
                levels.Add(level);
                // Debug.Log("XML reader: Adding survival level");
            }
		}

		return levels;

	}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static List<StoryLevelData> getStoryLevelDataFromXML(/*string level_Name*/)
    {
        List<StoryLevelData> levels = new List<StoryLevelData>();
        XmlDocument xml_doc = new XmlDocument();
        xml_doc.Load("Assets/Resources/level.xml");
        //XmlNode level_info;
        XmlNodeList levelsInfo;


        //level_info = xml_doc.SelectSingleNode ("LEVELS/SURVIVAL/LEVEL[@name='" +level_Name+ "']");
        //levelsInfo = xml_doc.SelectNodes("LEVELS/SURVIVAL/LEVEL[@name='" + level_Name + "']");
        levelsInfo = xml_doc.SelectNodes("LEVELS/SURVIVAL/LEVEL");

        if (levelsInfo != null)
        {
            for (int i = 0; i < levelsInfo.Count; i++)
            {
                XmlNode nextLevelInfo = levelsInfo.Item(i);
                StoryLevelData level = new StoryLevelData();
                level.force_Spaceship = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[0].InnerText);
                level.max_Speed_Spaceship = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[1].InnerText);
                level.numberOfAliens = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[2].InnerText);
                level.numberOf_Asteroids = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[3].InnerText);
                level.force_Asteroids = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[4].InnerText);
                level.numberOfTrash = int.Parse(((XmlElement)nextLevelInfo).GetElementsByTagName("VARIABLE")[5].InnerText);
                levels.Add(level);
                // Debug.Log("XML reader: Adding story level");
            }
        }

        return levels;

    }
    #endregion
}
