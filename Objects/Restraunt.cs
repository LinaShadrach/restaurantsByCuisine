using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TEMPLATE.Objects
{
  public class TEMPLATE_OBJECT
  {
    // where TEMPLATE_OBJECTId references a property of the object
    public int TEMPLATE_OBJECTId {get; set;}
    public string TEMPLATE_OBJECTDescription {get; set;}
    private List<string> TEMPLATE = new List<string> {};
    //where TEMPLATE_OBJECT references the Object

    public TEMPLATE_OBJECT(int TEMPLATEuserdata)
    {
      this.TEMPLATEproperty = TEMPLATEuserdata;
    }

    public override bool Equals(System.Object TEMPLATE_OTHER_OBJECT)
    {
      if (!(otherTask is Task))
      {
        return false;
      }
      else
      {
        TEMPLATE_OBJECT newTEMPLATE_OBJECT = (TEMPLATE_OBJECT) TEMPLATE_OTHER_OBJECT;
        bool TEMPLATEpropertyEquality = (this.TEMPLATEproperty() == newTEMPLATE_OBJECT.TEMPLATEproperty());
        return (TEMPLATEpropertyEquality);
      }
    }

    public static List<Task> GetAll()
    {
      List<TEMPLATE_OBJECT> TEMPLATE_OBJECT_LIST = new List<TEMPLATE_OBJECT>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM TEMPLATE_OBJECTS;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int TEMPLATE_OBJECTId = rdr.GetInt32(0);
        string TEMPLATE_OBJECTDescription = rdr.GetString(1);
        TEMPLATE_OBJECT newTEMPLATE_OBJECT = new TEMPLATE_OBJECT(TEMPLATE_OBJECTDescription, taskId);
        TEMPLATE_OBJECT_LIST.Add(newTEMPLATE_OBJECT);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allTasks;
    }
  }
}
