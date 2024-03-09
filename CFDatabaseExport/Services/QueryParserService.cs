using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CFDatabaseExport.Forms;
using CFDatabaseExport.Models;
using CFUtilities.Databases;

namespace CFDatabaseExport
{
    /// <summary>
    /// Parses the query, prompts used for parameters, replaces any functions in the query script and assigns the
    /// parameter value to SQL variables. E.g. User is prompted to select a list of countries and the @COUNTRY_IDs
    /// table variable is populated with Country IDs
    /// </summary>
    internal class QueryParserService
    {
        private const string _functionPrefix = "##";

        public string Parse(OleDbDatabase database, SQLQuery query, 
                            IQueryService queryRepository, IQueryFunctionService queryFunctionRepository,
                            ISQLGenerator sqlGenerator)
        {
            string querySql = query.SQL;

            // Get all query functions
            List<QueryFunction> queryFunctions = queryFunctionRepository.GetAll();

            // Get all script functions
            List<ScriptFunction> scriptFunctions = new List<ScriptFunction>();
            ScriptFunction scriptFunction = null;
            do
            {
                scriptFunction = GetNextScriptFunction(querySql, 0, queryFunctions);
                if (scriptFunction != null)
                {
                    scriptFunctions.Add(scriptFunction);
                    querySql = querySql.Replace(scriptFunction.ScriptText, "--Function removed");    // Remove function
                }
            } while (scriptFunction != null);
          
            // Prompt for parameters
            string querySqlToReturn = query.SQL;
            if (scriptFunctions.Any())
            {
                ParametersForm parametersForm = new ParametersForm(database, query, queryFunctions, scriptFunctions);
                if (parametersForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (ScriptFunction currentScriptFunction in scriptFunctions)
                    {
                        if (currentScriptFunction is ScriptFunctionSelectItem)
                        {
                            QueryFunctionListItem queryFunctionListItem = (QueryFunctionListItem)queryFunctions.FirstOrDefault(qf => qf.Name.ToUpper() == currentScriptFunction.FunctionName.ToUpper());

                            // Get selected list items
                            var selectedItems = parametersForm.GetSelectedListItems(currentScriptFunction);
                      
                            // Create SQL for setting variable
                            string selectedListItemsSQL = GetSelectedListItemsSQL(selectedItems, (ScriptFunctionSelectItem)currentScriptFunction, queryFunctionListItem, sqlGenerator);

                            // Replace original function text with SQL for setting variable
                            querySqlToReturn = querySqlToReturn.Replace(currentScriptFunction.ScriptText, selectedListItemsSQL);
                        }
                    }
                }
            }
            return querySqlToReturn;
        }

        /// <summary>
        /// Returns SQL to set the selected list items for the script function. For single items then we set a single variable value and
        /// for multiple items then we insert each item in to the table variable.
        /// </summary>
        /// <param name="selectedItems"></param>
        /// <param name="scriptFunction"></param>
        /// <returns></returns>
        private static string GetSelectedListItemsSQL(List<LocalNameValuePair> selectedItems, 
                                ScriptFunctionSelectItem scriptFunction, QueryFunctionListItem queryFunction,
                                ISQLGenerator sqlGenerator)
        {
            StringBuilder sql = new StringBuilder("");
            if (scriptFunction.MinItems == 1 && scriptFunction.MaxItems == 1)   // Select item, non table variable
            {                              
                sql.Append(sqlGenerator.GetSQLForComment(scriptFunction.ScriptText));
                sql.Append(sqlGenerator.GetSQLToSetVariableValue(scriptFunction.Variable, selectedItems.First().Value, queryFunction.ValueType));                                
            }
            else    // Multiple items, table variable
            {
                sql.Append(sqlGenerator.GetSQLForComment(scriptFunction.ScriptText));
                //sql.Append(sqlGenerator.GetSQLToTruncateTable(scriptFunction.Variable));    // Truncate table
                foreach (var item in selectedItems)
                {
                    sql.Append(sqlGenerator.GetSQLToSetTableVariableValue(scriptFunction.Variable, item.Value, queryFunction.ValueType));
                }
            }
            return sql.ToString();
        }

        /// <summary>
        /// Returns the next script function from the input SQL
        /// </summary>
        /// <param name="querySql"></param>
        /// <param name="startPosition"></param>
        /// <param name="queryFunctions"></param>
        /// <returns></returns>
        private ScriptFunction GetNextScriptFunction(string querySql, int startPosition, List<QueryFunction> queryFunctions)
        {           
            int position = querySql.IndexOf(_functionPrefix, startPosition);
            if (position > -1)
            {
                int endPosition = querySql.IndexOf(_functionPrefix, position + _functionPrefix.Length + 1);
                string scriptFunctionString = querySql.Substring(position, endPosition - position + _functionPrefix.Length);
               
                // Get function name
                string functionName = GetFunctionName(scriptFunctionString);

                // Check if valid function name
                QueryFunction queryFunction = queryFunctions.FirstOrDefault(qf => qf.Name.ToUpper() == functionName.ToUpper());
                if (queryFunction != null)
                {
                    // Get parameters
                    List<string> functionParameters = GetFunctionParameters(scriptFunctionString);

                    if (queryFunction is QueryFunctionListItem)
                    {
                        QueryFunctionListItem queryFunctionListItem = (QueryFunctionListItem)queryFunction;

                        ScriptFunctionSelectItem scriptFunctionSelectItems = new ScriptFunctionSelectItem()
                        {
                            FunctionName = functionName,
                            ScriptText = scriptFunctionString
                        };

                        foreach (string functionParameter in functionParameters)
                        {
                            string[] parameterElements = GetParameterElements(functionParameter);
                            switch (parameterElements[0].ToUpper())
                            {
                                case "PROMPT": scriptFunctionSelectItems.Prompt = parameterElements[1]; break;
                                case "VARIABLE": scriptFunctionSelectItems.Variable = parameterElements[1]; break;
                                case "MINITEMS": scriptFunctionSelectItems.MinItems = Convert.ToInt32(parameterElements[1]); break;
                                case "MAXITEMS": scriptFunctionSelectItems.MaxItems = Convert.ToInt32(parameterElements[1]); break;
                            }
                        }
                        return scriptFunctionSelectItems;
                    }                    
                }
            }
            return null;
        }

        private static string[] GetParameterElements(string parameter)
        {
            List<string> elements = new List<string>();
            int equalsPosition = parameter.IndexOf("=");
            if (equalsPosition == -1)   // No parameter name indicator
            {
                return new string[] { "", parameter };
            }
            return new string[] { parameter.Substring(0, equalsPosition), parameter.Substring(equalsPosition + 1) };
        }

        private static List<string> GetFunctionParameters(string scriptFunctionString)
        {
            if (scriptFunctionString.StartsWith(_functionPrefix))
            {
                scriptFunctionString = scriptFunctionString.Substring(_functionPrefix.Length);
            }
            if (scriptFunctionString.EndsWith(_functionPrefix))
            {
                scriptFunctionString = scriptFunctionString.Substring(0, scriptFunctionString.Length - _functionPrefix.Length);
            }

            // Get brackets
            int openBracketPosition = scriptFunctionString.IndexOf("(");
            int closeBracketPosition = scriptFunctionString.IndexOf(")");

            // Get parameters. 
            // TODO: Improve this to be more robuest
            string parametersString = scriptFunctionString.Substring(openBracketPosition + 1, closeBracketPosition - openBracketPosition - 1);
            List<string> parameters = new List<string>();
            string[] items = parametersString.Split(',');
            foreach(string item in items)
            {
                parameters.Add(item.Trim());
            }
            return parameters;
        }
        
        private static string GetFunctionName(string scriptFunctionString)
        {
            if (scriptFunctionString.StartsWith(_functionPrefix))
            {
                scriptFunctionString = scriptFunctionString.Substring(_functionPrefix.Length);
            }
            if (scriptFunctionString.EndsWith(_functionPrefix))
            {
                scriptFunctionString = scriptFunctionString.Substring(0, scriptFunctionString.Length - _functionPrefix.Length);
            }

            // Get bracket
            int openBracketPosition = scriptFunctionString.IndexOf("(");

            return scriptFunctionString.Substring(0, openBracketPosition);
        }
    }
}
