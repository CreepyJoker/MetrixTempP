using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Controls;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Metrix.Cmp
{
    public class CustomDataTableConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => (objectType == typeof(DataTable));
        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            var rowsArray = JArray.Load(reader);
            var metaDataObj = (JObject)rowsArray.First();
            var dt = new DataTable();

            foreach (var prop in metaDataObj.Properties())
            {
                dt.Columns.Add(prop.Name);
            }
            foreach (JObject rowDataObj in rowsArray)
            {
                var row = dt.NewRow();
                foreach (DataColumn col in dt.Columns)
                {
                    if (rowDataObj[col.ColumnName].Type == JTokenType.Array)
                    {
                        row[col] = rowDataObj[col.ColumnName].ToString(Formatting.None);
                    }
                    else
                    {
                        row[col] = rowDataObj[col.ColumnName].ToObject(col.DataType);
                    }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertUserData
    {

        public static int nrDungeonsFailed = 0;
        public static int nrAffixesFailed = 0;
        public static string Deflate(string compressedString)
        {

            byte[] gZipBuffer = Convert.FromBase64String(compressedString);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }

        }

        public static string MTX_Arcan(string src, Encoding encoding = null)
        {
            string fin = "";

            if (src != "" && src != null)
            {
                encoding = encoding ?? Encoding.UTF8;
                fin = encoding.GetString(Convert.FromBase64String(src));
                fin = fin.Replace("+", "");
                fin = fin.Replace("\"[", "[");
                fin = fin.Replace("]\"", "]");
                //fin = fin.Replace('\', )
            }

            return fin;
        }
        public class DungeonFailedMechanicsStruct
        {
            public string Id { get; set; }
            public string[] failedMechanics { get; set; }
        }
        public static DataTable MTX_JsonFailedMech(string json, string dungeonName, int dungeonNr = 0)
        {
            nrDungeonsFailed = 0;
            var dt = new DataTable();
            try
            {
                var jsonLinq = JObject.Parse(json);

                var srcArray = jsonLinq.SelectToken($"{dungeonName}[{dungeonNr}].FM");

                for(int i=0; i <= 3; i++)
                {
                    var asd = jsonLinq.SelectToken($"{dungeonName}[{i}].FM");
                    if (asd != null)
                    {
                        nrDungeonsFailed = nrDungeonsFailed + 1;
                    }
                }
                //Console.WriteLine(srcArray.ToString());
                //Console.WriteLine(nrDungeonsFailed);

                var trgArray = new JArray();

                DataTable table = new DataTable();
                //DataRow[] rowArray = dataTable.Select();

                //return table;

                List<DungeonFailedMechanicsStruct> _dungeonFailedMechs = new List<DungeonFailedMechanicsStruct>();
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                int rowsCreated = 0;
                if (srcArray == null)
                {
                    MessageBox.Show("There are no failed mechanics for the specified dungeon.");
                    
                }
                else
                {
                    foreach (var column in srcArray.Children<JProperty>())
                    {
                        dt.Columns.Add(column.Name);
                    }
                    string data = null;

                    foreach (var column in srcArray.Children<JProperty>())
                    {
                        //dt.Columns.Add(column.Name);

                        foreach (var subColumns in column)
                        {
                            foreach (var specificData in subColumns)
                            {
                                data = specificData.ToString().Replace("\"", "");

                                string[] dataVars = data.Split(':');

                                var points = dataVars[1];
                                var mechanicID = dataVars[0];
                                mechanicID = WowInfo.MTX_SetBossMechanicByID(mechanicID);

                                data = mechanicID + " " + points;
                                //Console.WriteLine(data);

                                foreach (DataColumn col in dt.Columns)
                                {
                                    var shouldIncreaseRows = 0;
                                    var columnsNr = dt.Rows.Count - 1;
                                    var isNullColumn = dt.Rows[columnsNr].IsNull(col.ColumnName);

                                    if (!isNullColumn)
                                    {
                                        shouldIncreaseRows = 1;
                                    }
                                    else
                                    {
                                        shouldIncreaseRows = 0;
                                    }

                                    if (shouldIncreaseRows > 0 && column.Name == col.ColumnName)
                                    {
                                        dr = dt.NewRow();
                                        //dr[col.ColumnName] = specificData.ToString();
                                        dt.Rows.Add(dr);
                                        Console.WriteLine("Creating new row...");
                                        rowsCreated = rowsCreated + 1;
                                    }
                                    //Console.WriteLine(colVal);

                                    //Console.WriteLine(shouldFuckThem);
                                }
                                if (data != null)
                                {
                                    //dr = dt.Rows[rowsCreated];
                                    dr.SetField<string>(column.Name, data);
                                }
                            }

                        }
                    }
                }
                

                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "")
                    {
                        col.ColumnName = WowInfo.MTX_SetBossNameByID(col.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There are no failed mechanics for the specified dungeon.");
            }
            

            return dt;
            //return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());

        }

        public static DataTable MTX_JsonFailedAffixes(string json, string dungeonName, int dungeonNr = 0)
        {
            nrAffixesFailed = 0;

            var dt = new DataTable();
            try
            {
                var jsonLinq = JObject.Parse(json);

                var srcArray = jsonLinq.SelectToken($"{dungeonName}[{dungeonNr}].FA");

                for (int i = 0; i <= 3; i++)
                {
                    var asd = jsonLinq.SelectToken($"{dungeonName}[{i}].FA");
                    if (asd != null)
                    {
                        nrAffixesFailed = nrAffixesFailed + 1;
                    }
                }

                //Console.WriteLine(nrDungeonsFailed);

                var trgArray = new JArray();

                DataTable table = new DataTable();
                //DataRow[] rowArray = dataTable.Select();

                //return table;

                List<DungeonFailedMechanicsStruct> _dungeonFailedMechs = new List<DungeonFailedMechanicsStruct>();
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
                int rowsCreated = 0;
                if (srcArray == null)
                {
                    MessageBox.Show("There are no failed affixes for the specified run. Run["+ (dungeonNr + 1) +"]");

                }
                else
                {
                    foreach (var column in srcArray.Children<JProperty>())
                    {
                        dt.Columns.Add(column.Name);
                    }
                    
                    foreach (var column in srcArray.Children<JProperty>())
                    {
                        //dt.Columns.Add(column.Name);
                        string data = null;
                        foreach (var subColumns in column)
                        {
                            foreach (var specificData in subColumns)
                            {
                                Console.WriteLine(specificData.ToString());
                                data = specificData.ToString().Replace("\"", "");

                                string[] dataVars = data.Split(':');
                                data = dataVars[1].ToString();

                                foreach (DataColumn col in dt.Columns)
                                {
                                    var shouldIncreaseRows = 0;
                                    var columnsNr = dt.Rows.Count - 1;
                                    var isNullColumn = dt.Rows[columnsNr].IsNull(col.ColumnName);

                                    if (!isNullColumn)
                                    {
                                        shouldIncreaseRows = 1;
                                    }
                                    else
                                    {
                                        shouldIncreaseRows = 0;
                                    }

                                    if (shouldIncreaseRows > 0 && column.Name == col.ColumnName)
                                    {
                                        dr = dt.NewRow();
                                        //dr[col.ColumnName] = specificData.ToString();
                                        dt.Rows.Add(dr);
                                        Console.WriteLine("Creating new row...");
                                        rowsCreated = rowsCreated + 1;
                                    }
                                    //Console.WriteLine(colVal);

                                    //Console.WriteLine(shouldFuckThem);

                                }
                                if (data != null)
                                {
                                    //dr = dt.Rows[rowsCreated];
                                    dr.SetField<string>(column.Name, data);
                                }
                            }

                        }
                    }
                }


                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "")
                    {
                        col.ColumnName = WowInfo.MTX_SetAffixByID(col.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There are no failed mechanics for the specified dungeon.");
            }


            return dt;
            //return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());

        }
        public static DataTable Tabulate(string json, string dungeonName, bool failedMechanics = false)
        {
            var jsonLinq = JObject.Parse(json);
            
            var srcArray = jsonLinq.SelectToken(dungeonName);

            if (failedMechanics == true)
            {
                srcArray = jsonLinq.SelectToken($"{dungeonName}[*].FM");
            }
            
            var trgArray = new JArray();
            try
            {
                
                if (srcArray != null)
                {
                    foreach (JObject row in srcArray.Children<JObject>())
                    {
                        var cleanRow = new JObject();
                        foreach (JProperty column in row.Properties())
                        {
                            if (column.Value is JValue)
                            {
                                cleanRow.Add(column.Name, column.Value);
                            }
                        }

                        trgArray.Add(cleanRow);
                    }
                }
                
                

            }
            catch(Exception ex)
            {

            }
            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }

        

        public static DataTable JsonStringToDataTable(string userData, string dungeonName, int runNumber)
        {
            DataTable dt = new DataTable();

            var jsonLinq = JObject.Parse(userData);

            //var srcArray = jsonLinq.SelectToken(dungeonName);

            var srcArray = jsonLinq.SelectToken($"{dungeonName}[*].FM");

            string jsonString = srcArray.ToString();
            Console.WriteLine(jsonString);
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");

            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString) && ColumnsNameString.Length <= 8)
                        {
                            //Console.WriteLine(ColumnsNameString.Length);
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        Console.WriteLine(RowColumns, RowDataString);
                        nr[RowColumns] = RowDataString;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }
        public static DataTable JsonArrayParsingTest(string jsonData, string dungeonName)
        {
            DataTable dt = new DataTable();

            var jsonLinq = JObject.Parse(jsonData);

            //var srcArray = jsonLinq.SelectToken(dungeonName);

            var srcArray = jsonLinq.SelectToken($"{dungeonName}[*].FM");
            string jsonString = srcArray.ToString();
            dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
            

            dt = JsonConvert.DeserializeObject<DataTable>(srcArray.ToString());
            return dt;
        }
    }
}
