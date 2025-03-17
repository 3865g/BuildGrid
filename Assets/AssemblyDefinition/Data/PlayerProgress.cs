using System;
using System.Collections.Generic;

namespace AssemblyDefinition.Data
{
  [Serializable]
  public class PlayerProgress
  {
      public List<BuildingData> buildings = new List<BuildingData>();
  }
}