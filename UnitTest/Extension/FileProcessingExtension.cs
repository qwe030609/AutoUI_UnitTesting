using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using Chroma.UnitTest.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Reflection.Emit;
using System.Collections;
using System.Windows.Input;
using Castle.Core.Internal;
using System.Collections.Concurrent;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;

namespace PP5AutoUITests
{
    /// <summary>
    /// Provides extensions related with file and folder operations.
    /// </summary>
    public static class FileProcessingExtension
    {
        private static string currWorkingDirectory = Directory.GetParent(System.Environment.CurrentDirectory).FullName;

        // Property to access and reset the static variable
        public static string CurrWorkingDirectory
        {
            get { return currWorkingDirectory; }
            //set { currWorkingDirectory = value; }
        }

        /// <summary>
        /// Deletes files given only the filename without extension, which means to delete same files with same name (extension excluded)
        /// Ex: {Folder}/xxx.tp1, {Folder}/xxx.tp2, {Folder}/xxx.tp3
        ///     given {Folder}, {filename(xxx)}, the files: xxx.tp1, xxx.tp2, xxx.tp3 will be deleted
        /// </summary>
        /// <param name="folderPath">The path of the folder containing the files.</param>
        /// <param name="fileName">The filename to be deleted.</param>
        /// <returns><c>true</c> if the files are deleted successfully; otherwise, <c>false</c>.</returns>
        public static bool DeleteFilesWithDifferentExtension(this string folderPath, string fileName)
        {
            var filesToDelete = Directory.GetFiles(folderPath, fileName + ".*");
            if (filesToDelete.Length > 0)
            {
                //File exists delete it
                foreach (string file in filesToDelete)
                {
                    File.Delete(file);
                }
            }
            else
            {
                //File does not exist
            }

            return filesToDelete.ToList().TrueForAll(fn => File.Exists(fn));
        }

        public static bool JsonCountNodesInList(string filePath, string nodePath /*node1[0]/node2*/, out int nodeCount)
        {
            nodeCount = 0;
            try
            {
                // Load the JSON configuration from the file
                var json = File.ReadAllText(filePath);
                var config = JObject.Parse(json);

                // Parse the nodePath to extract node indices and the property update information
                var nodes = nodePath.Split('/');

                JToken nodeFound = config;
                List<string> nodesNames = new List<string>();
                List<int?> indices = new List<int?>();

                // Decode node path and indices
                foreach (var node in nodes)
                {
                    if (node.Contains('['))
                    {
                        var parts = node.TrimEnd(']').Split('[');
                        nodesNames.Add(parts[0]);
                        indices.Add(int.Parse(parts[1]));
                    }
                    else
                    {
                        nodesNames.Add(node);
                        indices.Add(null);
                    }
                }

                // Navigate to the specified node
                for (int i = 0; i < nodesNames.Count; i++)
                {
                    nodeFound = nodeFound.SelectToken(nodesNames[i]);
                    if (nodeFound == null)
                    {
                        Logger.LogMessage($"Node {nodesNames[i]} not found.");
                        return false;
                    }

                    if (indices[i].HasValue && nodeFound is JArray array)
                    {
                        // Access specific indexed node if index exists
                        nodeFound = array[indices[i].Value];
                    }
                }

                if (nodeFound is JArray array1)
                {
                    nodeCount = array1.Count;
                }

                Logger.LogMessage($"NodeArray count is: '{nodeCount}'.");
            }
            catch (Exception ex)
            {
                Logger.LogMessage($"Error getting NodeArray count: {ex.Message}");
            }
            return true;
        }

        /// <summary>
        /// Creates a new node in a JSON file based on the specified node path.
        /// </summary>
        /// <param name="filePath">The full path of the JSON file.</param>
        /// <param name="nodePath">The path to create the new node, following the format: {nodePart}@{propertyPart}</param>
        /// <returns>True if the new node is successfully created; otherwise, false.</returns>
        /// <remarks>
        /// Node Path Format: {nodePart}@{propertyPart}
        /// 
        /// nodePart: nodeName1[index1]/nodeName2[index2]/.../nodeName_n[{srcIndex}>{destIndex}]
        /// - Represents the hierarchical structure of nodes
        /// - Last node format: [srcIndex>destIndex] or [>destIndex]
        ///   - [>destIndex]: Creates a clean node (clears inner properties)
        ///   - [srcIndex>destIndex]: Clones the source node to create a new node
        /// 
        /// propertyPart: PropertyName1=Value1,PropertyName2=Value2,...
        /// - Defines properties to be set on the new node
        /// 
        /// Examples:
        /// 1. Create a clean node at the end:
        ///    CommandGroupInfos[0]/Commands[>-1]@CommandName=cmdName,GroupID=300,IsDevice=true
        /// 
        /// 2. Clone and create a node at the end:
        ///    CommandGroupInfos[0]/Commands[0>-1]@CommandName=newCmd,GroupID=301,IsDevice=false
        /// 
        /// For more details on usage and edge cases, refer to the method implementation.
        /// </remarks>
        public static bool JsonCreateNewNodeInList(string filePath, string nodePath)
        {
            bool propertyUpdated = false;
            try
            {
                // Load the JSON configuration from the file
                var json = File.ReadAllText(filePath);
                var config = JObject.Parse(json);

                // Parse the nodePath to extract node indices and the property update information
                var atSplit = nodePath.Split('@');
                var nodes = atSplit[0].Split('/');
                var propertyAssignmentList = atSplit[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries); // atSplit[1]: propertyName1=newValue1,propertyName2=newValue2,propertyName3=newValue3
                List<string> propertyNames = new List<string>();
                List<JToken> newValues = new List<JToken>();
                foreach (var propertyAssignment in propertyAssignmentList)
                {
                    var propertyNameAndValue = propertyAssignment.Trim().Split('=');
                    propertyNames.Add(propertyNameAndValue[0].Trim());
                    newValues.Add(JToken.FromObject(ParseStringToObjectOfType(propertyNameAndValue[1].Trim())));
                    //TypeHelper.IsLong(propertyNameAndValue[1].Trim().GetType());
                    //var roc = new System.Collections.ObjectModel.ReadOnlyCollection<string>(new []{""});
                    //bool isROC = TypeHelper.IsReadOnlyCollection(roc.GetType());

                    //Assert.IsTrue(isROC);
                    //typeof(System.Collections.Generic.IReadOnlyCollection)
                }

                JToken nodeFound = config;
                JToken parent = null;
                List<string> nodesNames = new List<string>();
                List<string> indices = new List<string>();
                int? destNodeToCreateIdx = null, sourceNodeToCopyIdx = null;
                bool isCreateCleanNode = false;
                //string pnTmp = "pnTmp";

                // Decode node path and indices
                foreach (var node in nodes)
                {
                    if (node.Contains('['))
                    {
                        var parts = node.TrimEnd(']').Split('[');
                        nodesNames.Add(parts[0]);

                        // Adam, 20240829, Add source > destination node index parsing: [src>dst]
                        //if (parts[1].Contains('>'))
                        //{
                        //    var partIndeces = parts[1].Split(new char[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                        //    var sourceIdx = partIndeces[0];
                        //    var destIdx = partIndeces[1];
                        //    indices.Add(string.Concat(sourceIdx, ',' , destIdx));
                        //}
                        //else
                        //{
                        //    indices.Add(parts[1]);
                        //}
                        indices.Add(parts[1]);
                    }
                    else
                    {
                        nodesNames.Add(node);
                        indices.Add(null);
                    }
                }

                // Navigate to the specified node
                for (int i = 0; i < nodesNames.Count; i++)
                {
                    nodeFound = nodeFound.SelectToken(nodesNames[i]);
                    if (nodeFound == null)
                    {
                        Logger.LogMessage($"Node {nodesNames[i]} not found.");
                        return propertyUpdated;
                    }


                    // Adam, 20240829, Add source > destination node index parsing: [src>dst]
                    if (indices[i].Contains('>'))
                    {
                        var parts = indices[i].Split('>');
                        isCreateCleanNode = parts[0].ToNullableInt() == null;
                        sourceNodeToCopyIdx = isCreateCleanNode ? parts[0].ToNullableInt() != -1 ?
                                              0 : parts[0].ToNullableInt() : nodeFound.Count() - 1;
                        destNodeToCreateIdx = parts[1].ToNullableInt() != -1 ? parts[1].ToNullableInt() : nodeFound.Count();
                        indices[i] = sourceNodeToCopyIdx.ToString();
                    }
                    else
                        if (indices[i].ToNullableInt() == -1)
                        indices[i] = (nodeFound.Count() - 1).ToString();

                    int? idx = indices[i].ToNullableInt();
                    if (idx.HasValue)
                    {
                        if (nodeFound is JArray array)
                        {
                            // Access specific indexed node if index exists
                            nodeFound = array.IsNullOrEmpty() ? array : array[idx.Value];
                        }
                    }

                    //if (idx.HasValue && nodeFound is JArray array)
                    //{
                    //    else
                    //    {
                    //        if (idx == -1)
                    //            indices[i] = (nodeFound.Count() - 1).ToString();
                    //    }

                    //    // Access specific indexed node if index exists
                    //    nodeFound = array[indices[i].Value];
                    //}
                }
                if (nodeFound is JArray && !nodeFound.HasValues)
                {
                    parent = nodeFound;
                    nodeFound = new JObject();
                }
                else
                    parent = nodeFound.Parent; // Store parent reference

                // Copy and update the node
                if (nodeFound is JObject obj)
                {
                    // Only copy from the node: [src>dst]
                    var newNode = (JObject)obj.DeepClone();

                    // Adam, 20240829, for clean node creation:
                    // Create clean node: [>dst]
                    if (newNode.HasValues && isCreateCleanNode)
                    {
                        foreach (var child in newNode.Properties())
                        {
                            //if ((child.Value as JValue))
                            if (child.Value is JArray array)
                            {
                                array.RemoveAll();
                            }
                            else if (child.Value is JValue)
                            {
                                var property = child;
                                if (property.Name.Contains('$')) { }    // $type : "Chroma.Common.Command, Common", retain original value
                                else
                                {
                                    if (property.Value.Type is JTokenType.String 
                                        || property.Value.Type is JTokenType.Integer 
                                        || property.Value.Type is JTokenType.Float)
                                        property.Value = null;
                                    else if (property.Value.Type is JTokenType.Boolean)
                                        property.Value = false;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < propertyNames.Count; i++)
                        newNode[propertyNames[i]] = newValues[i];

                    if (parent is JArray parentArray)
                    {
                        // Add the new node to the end of the parent array
                        parentArray.Insert((int)destNodeToCreateIdx, newNode);
                        propertyUpdated = true;
                    }
                    else
                    {
                        Logger.LogMessage("Parent node is not an array.");
                        return propertyUpdated;
                    }
                }
                else
                {
                    Logger.LogMessage("Target node is not an object.");
                    return propertyUpdated;
                }

                if (!propertyUpdated)
                {
                    Logger.LogMessage($"Property with name '{propertyNames.ListToString()}' not updated.");
                }
                else
                {
                    // Save the updated JSON configuration back to the file
                    File.WriteAllText(filePath, config.ToString(Formatting.None));
                    Logger.LogMessage($"Node copied, property '{propertyNames.ListToString()}' updated, and new node added successfully.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage($"Error updating property: {ex.Message}");
            }
            return propertyUpdated;
        }

        public static bool JsonUpdateProperty(string filePath, string nodePath, string propertyName, object newValue)
        {
            bool propertyUpdated = false;
            try
            {
                // Load the JSON configuration from the file
                var json = File.ReadAllText(filePath);
                var config = JObject.Parse(json);

                // Find the specific property by name and update its ItemValue
                var nodes = nodePath.Split('/');
                JToken nodeFound = config.DeepClone();

                foreach (var node in nodes)
                {
                    nodeFound = nodeFound.SelectToken(node);
                    if (nodeFound == null)
                        return propertyUpdated;
                }

                foreach (var item in nodeFound)
                {
                    if (item["Name"].ToString() == propertyName)
                    {
                        if (item["ItemValue"] != JToken.FromObject(newValue))
                            item["ItemValue"] = JToken.FromObject(newValue);
                        propertyUpdated = true;
                        break;
                    }
                }

                if (!propertyUpdated)
                {
                    Logger.LogMessage($"Property with name '{propertyName}' not found.");
                }

                // Save the updated JSON configuration back to the file
                File.WriteAllText(filePath, config.ToString(Formatting.None));
                Logger.LogMessage($"Property '{propertyName}' updated successfully.");
            }
            catch (Exception ex)
            {
                Logger.LogMessage($"Error updating property: {ex.Message}");
            }
            return propertyUpdated;
        }

        public static bool JsonGetProperty<Ttype>(string filePath, string nodePath, out List<Ttype> propertyValues)
        {
            bool propertyFound = false;
            propertyValues = new List<Ttype>(); // Initialize the output list
            try
            {
                // Load the JSON configuration from the file
                string propertyName = string.Empty;
                var json = File.ReadAllText(filePath);
                var config = JObject.Parse(json);
                List<string> nodes = new List<string>();
                List<int?> nodeIndices = new List<int?>();
                JToken nodeFound = config.DeepClone();

                // Decode the nodePath
                string[] nodesAndPropName = nodePath.Split('@');    // Split at '@' to separate path and property name
                string nodesPath = nodesAndPropName.First();        // Before '@': the path of nodes
                propertyName = nodesAndPropName.Last();             // After '@': the property name

                var nodesTmp = nodesPath.Split('/');
                foreach (var node in nodesTmp)
                {
                    if (node.Contains('['))
                    {
                        var parts = node.TrimEnd(']').Split('[');
                        nodes.Add(parts[0]);
                        nodeIndices.Add(int.Parse(parts[1]));
                    }
                    else
                    {
                        nodes.Add(node);
                        nodeIndices.Add(null); // No index for this node
                    }
                }

                // Navigate to the specified node
                for (int i = 0; i < nodes.Count; i++)
                {
                    nodeFound = nodeFound.SelectToken(nodes[i]);
                    if (nodeIndices[i].HasValue && nodeFound != null && nodeFound.HasValues && nodeFound.Type == JTokenType.Array)
                    {
                        nodeFound = nodeFound[nodeIndices[i].Value]; // Access specific indexed node if index exists
                    }

                    if (nodeFound == null)
                    {
                        Logger.LogMessage($"Node {nodes[i]} not found.");
                        return propertyFound;
                    }
                }

                // Find the specific properties by name and collect their values
                if (nodeFound.Type == JTokenType.Array)
                {
                    foreach (var item in nodeFound.Children())
                    {
                        if (item[propertyName] != null)
                        {
                            Ttype propertyValue = item[propertyName].ToObject<Ttype>();
                            propertyValues.Add(propertyValue);
                            propertyFound = true;
                        }
                    }
                }
                else if (nodeFound[propertyName] != null) // For single item scenario
                {
                    Ttype propertyValue = nodeFound[propertyName].ToObject<Ttype>();
                    propertyValues.Add(propertyValue);
                    propertyFound = true;
                }

                if (!propertyValues.Any())
                {
                    Logger.LogMessage($"Property with name '{propertyName}' not found.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage($"Error getting property: {ex.Message}");
            }
            return propertyFound;
        }

        public static bool JsonGetProperty<Ttype>(string filePath, string nodePath /*{xxxx[idx1]/xxxx[idx2]@{propertyName}}*/, out Ttype propertyValue)
        {
            bool propertyFound = false;
            propertyValue = default; // Initialize out parameter
            try
            {
                // Load the JSON configuration from the file
                string propertyName = string.Empty;
                var json = File.ReadAllText(filePath);
                var config = JObject.Parse(json);
                List<(string, int?)> nodesWithIndces = new List<(string, int?)>();
                List<string> nodes = new List<string>();
                List<int?> nodeIdces = new List<int?>();
                JToken nodeFound = config.DeepClone();

                /* Legacy code (not all nodes have index supported)
                //// decode the nodePath
                //int i = 0;
                //var nodesTmp = nodePath.Split('/');
                //foreach (var node in nodesTmp)
                //{
                //    if (i < nodesTmp.Length - 1)
                //        nodes.Add(node);
                //    if (i == nodesTmp.Length - 1)
                //    {
                //        var lastNodeAndPropName = node.Split('@'); // 0: xxxx[idx], 1: {propertyName}
                //        if (lastNodeAndPropName[0] != null)
                //        {
                //            var lastNodeAndIndex = lastNodeAndPropName[0].TrimEnd(']').Split('[');
                //            nodes.Add(lastNodeAndIndex[0]);
                //            nodeIdx = int.Parse(lastNodeAndIndex[1]);
                //        }
                //        propertyName = lastNodeAndPropName[1];
                //    }
                //    i++;
                //}
                */

                // Decode the nodePath
                string[] nodesAndPropName = nodePath.Split('@');    // 0: xxxx[idx1]/xxxx[idx2], 1: propertyName
                string nodesPath = nodesAndPropName.First();        // nodePath:        xxxx[idx1]/xxxx[idx2]
                propertyName = nodesAndPropName.Last();             // propertyPath:    propertyName

                var nodesTmp = nodesPath.Split('/');
                for (int i = 0; i < nodesTmp.Length; i++)
                {
                    var node = nodesTmp[i];
                    if (node.Contains('['))
                    {
                        nodeIdces.Add(int.Parse(node.TrimEnd(']').Split('[').Last()));  // Get the node indeces and nodes
                        nodes.Add(node.TrimEnd(']').Split('[').First());
                    }
                    else
                    {
                        nodeIdces.Add(null);
                        nodes.Add(node);
                    }
                }
                for (int i = 0; i < nodesTmp.Length; i++)
                    nodesWithIndces.Add((nodes[i], nodeIdces[i]));

                // Navigate to the specified node
                for (int i = 0; i < nodesTmp.Length; i++)
                {
                    nodeFound = nodeFound.SelectToken(nodesWithIndces[i].Item1);
                    if (nodesWithIndces[i].Item2 != null)
                        nodeFound = nodeFound[nodesWithIndces[i].Item2];
                    if (nodeFound == null)
                    {
                        Logger.LogMessage($"Node not found.");
                        return propertyFound;
                    }
                }

                // Find the specific property by name and get its value
                if (nodeFound != null && nodeFound[propertyName] != null)
                {
                    propertyValue = nodeFound[propertyName].ToObject<Ttype>();
                    propertyFound = true;
                }
                else
                    Logger.LogMessage($"Property with name '{propertyName}' not found.");
            }
            catch (Exception ex)
            {
                Logger.LogMessage($"Error getting property: {ex.Message}");
            }
            return propertyFound;
        }

        public static bool JsonDeleteNode(string filePath, string nodePath /*node1[0]/node2[0]*/)
        {
            bool nodeDeleted = false;
            try
            {
                // Load the JSON configuration from the file
                var json = File.ReadAllText(filePath);
                var config = JObject.Parse(json);

                // Parse the nodePath to extract node indices
                var nodes = nodePath.Split('/');
                JToken nodeFound = config;
                List<string> nodesNames = new List<string>();
                List<int?> indices = new List<int?>();

                // Decode node path and indices
                foreach (var node in nodes)
                {
                    if (node.Contains('['))
                    {
                        var parts = node.TrimEnd(']').Split('[');
                        nodesNames.Add(parts[0]);
                        indices.Add(int.Parse(parts[1]));
                    }
                    else
                    {
                        nodesNames.Add(node);
                        indices.Add(null);
                    }
                }

                // Navigate to the specified node
                for (int i = 0; i < nodesNames.Count; i++)
                {
                    nodeFound = nodeFound.SelectToken(nodesNames[i]);
                    if (nodeFound == null)
                    {
                        Logger.LogMessage($"Node {nodesNames[i]} not found.");
                        return nodeDeleted;
                    }

                    if (indices[i].HasValue && nodeFound is JArray array)
                    {
                        if (indices[i] == -1)
                            indices[i] = nodeFound.Count() - 1;

                        // Access specific indexed node if index exists
                        nodeFound = array[indices[i].Value];
                    }
                    else
                        return nodeDeleted;
                }

                //// Find the final node to delete
                //var nodeToDelete = nodeFound.SelectToken(nodesNames.Last());
                //if (nodeToDelete == null)
                //{
                //    Console.WriteLine($"Node {nodesNames.Last()} not found.");
                //    return nodeDeleted;
                //}

                // Get the name of the final node to delete
                string finalNodeName = nodesNames.Last();
                int? finalNodeIndex = indices.Last();

                // Delete the node
                if (finalNodeIndex.HasValue)
                {
                    if (nodeFound.Parent is JArray parentArray)
                    {
                        // Remove the specific element from the array
                        parentArray.RemoveAt(finalNodeIndex.Value);
                        nodeDeleted = true;
                    }
                    else if (nodeFound.Parent is JObject parentObject)
                    {
                        // Remove the property from the object
                        parentObject.Remove(finalNodeName);
                        nodeDeleted = true;
                    }

                }

                if (nodeDeleted)
                {
                    // Save the updated JSON configuration back to the file
                    File.WriteAllText(filePath, config.ToString(Formatting.None));
                    Logger.LogMessage("Node deleted successfully.");
                }
                else
                {
                    Logger.LogMessage("Node could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage($"Error deleting node: {ex.Message}");
            }
            return nodeDeleted;
        }

        public static OrderedDictionary<TKey, TValue> CreateOrderedDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            if (keys.Count != values.Count)
                throw new ArgumentException("Keys and values lists do not have the same length.");
            OrderedDictionary<TKey, TValue> result = new OrderedDictionary<TKey, TValue>(keys.Count);

            for (int i = 0; i < keys.Count; i++)
            {
                result.Insert(i, keys[i], values[i]);  // Add each key-value pair to the dictionary
            }

            return result;
        }

        public static ConcurrentDictionary<TKey, TValue> CreateConcurrentDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            if (keys.Count != values.Count)
                throw new ArgumentException("Keys and values lists do not have the same length.");
            ConcurrentDictionary<TKey, TValue> result = new ConcurrentDictionary<TKey, TValue>();

            for (int i = 0; i < keys.Count; i++)
            {
                result[keys[i]] = values[i];  // Add each key-value pair to the dictionary
            }

            return result;
        }

        public static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            if (keys.Count != values.Count)
                throw new ArgumentException("Keys and values lists do not have the same length.");
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            for (int i = 0; i < keys.Count; i++)
            {
                result[keys[i]] = values[i];  // Add each key-value pair to the dictionary
            }

            return result;
        }

        public static object ParseStringToObjectOfType(this string str)
        {
            // if str is null, return null
            if (str.IsNull()) return null;

            // Try to parse as an integer (Long integer) > since JToken.Integer is type of long
            if (long.TryParse(str, out long intValue))
            {
                return intValue;
            }

            // Try to parse as a float (double precision) > since JToken.Float is type of double
            if (double.TryParse(str, out double floatValue))
            {
                return floatValue;
            }

            // Try to parse as a boolean
            if (bool.TryParse(str, out bool boolValue))
            {
                return boolValue;
            }

            // If it can't be parsed to int, float, or bool, return as string
            return str;
        }
    }
}
