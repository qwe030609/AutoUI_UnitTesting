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
                        Console.WriteLine($"Node {nodesNames[i]} not found.");
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

                Console.WriteLine($"NodeArray count is: '{nodeCount}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting NodeArray count: {ex.Message}");
            }
            return true;
        }

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
                var propertyAssignment = atSplit[1].Split('='); // propertyName=newValue
                string propertyName = propertyAssignment[0];
                var newValue = JToken.FromObject(propertyAssignment[1]);

                JToken nodeFound = config;
                JToken parent = null;
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
                        Console.WriteLine($"Node {nodesNames[i]} not found.");
                        return propertyUpdated;
                    }

                    if (indices[i].HasValue && nodeFound is JArray array)
                    {
                        // Access specific indexed node if index exists
                        nodeFound = array[indices[i].Value];
                    }
                }
                parent = nodeFound.Parent; // Store parent reference

                // Copy and update the node
                if (nodeFound is JObject obj)
                {
                    var newNode = (JObject)obj.DeepClone();
                    newNode[propertyName] = newValue;

                    if (parent is JArray parentArray)
                    {
                        // Add the new node to the end of the parent array
                        parentArray.Add(newNode);
                        propertyUpdated = true;
                    }
                    else
                    {
                        Console.WriteLine("Parent node is not an array.");
                        return propertyUpdated;
                    }
                }
                else
                {
                    Console.WriteLine("Target node is not an object.");
                    return propertyUpdated;
                }

                if (!propertyUpdated)
                {
                    Console.WriteLine($"Property with name '{propertyName}' not updated.");
                }
                else
                {
                    // Save the updated JSON configuration back to the file
                    File.WriteAllText(filePath, config.ToString(Formatting.None));
                    Console.WriteLine($"Node copied, property '{propertyName}' updated, and new node added successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating property: {ex.Message}");
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
                    Console.WriteLine($"Property with name '{propertyName}' not found.");
                }

                // Save the updated JSON configuration back to the file
                File.WriteAllText(filePath, config.ToString(Formatting.None));
                Console.WriteLine($"Property '{propertyName}' updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating property: {ex.Message}");
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
                        Console.WriteLine($"Node {nodes[i]} not found.");
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
                    Console.WriteLine($"Property with name '{propertyName}' not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting property: {ex.Message}");
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
                        Console.WriteLine($"Node not found.");
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
                    Console.WriteLine($"Property with name '{propertyName}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting property: {ex.Message}");
            }
            return propertyFound;
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
    }
}
