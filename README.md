# cf-database-export

Tool for exporting data from any OLEDB database to CSV, HTML, XML, Excel, text file or a SQL script. The tool provides
the user with a list of databases and each database has a set of available queries. The user selects the database, then
selects the query, selects the output format (CSV, HTML etc), selects output format parameters and then executes the
query to produce the output.

Output Formats
--------------

Delimited file (CSV): Output each resultset to a separate file.
Excel file: This may be removed later because it uses a third party library.
HTML: Takes a template HTML file and inserts an HTML Table element with each resultset returned by the query.
XML: Exports all rows to an XML file.
JSON: Exports all rows to a JSON file.
Screen: Displays the output on screen.
SQL file: Takes a SQL template file containing placeholders and a row template and creates a new SQL file. E.g. Create a SQL file 
that transforms the data and inserts it in to another table.
Text file from XSL transform: Takes an XSLT file, processes the query results and creates a new file.

Query Parameters
----------------

The user can be prompted for parameters for the SQL query at runtime. Typically the parameters would filter the data
that is returned from the query. E.g. The user is prompted to select one or more countries from a list and the query
filters sales data for the specific countries.

The SQL script template can contain one or more placeholders that indicate which parameter to prompt for. The placeholder 
is then replaced with the values to use. Each placeholder refers to a named function and the function details are defined 
in an XML file. At runtime then the placeholder in the SQL script template is replaced with a SQL variable being assigned
the parameter value.

Example 1 (Select single parameter from a query list)
- The SQL script template defines a variable @CountryID that filters the data for a specific country.
- The SQL script template refers to a function: 
	"##SelectCountry(Prompt='Select a country',Variable=@CountryID')##"
- The XML file for the SelectCountry function defines a query that returns the ID & Name from the COUNTRIES table.
- At runtime then the user is presented with a list of country names and they select one country.
- The SQL is changed to replace the ##SelectCountry reference and assign the @CountryID variable.
- The SQL query is executed and it filters the data for the selected country.

Example 2 (Select multiple parameters from a query list)
- The SQL script template defines a table variable @CountryIDs that filters the data for specific countries.
- The SQL script template refers to a function:
	"##SelectCountries(Prompt='Select 1-3 countries', Variable=@CountryIDs', MinItems=1, MaxItems=3)##"
- The XML file for the SelectCountries function defines a query that returns the ID & Name from the COUNTRIES table.
- At runtime then the user is presented with a list of country names and they select multiple countries.
- The SQL is changed to replace the ##SelectCountries reference and assign the @CountryIDs table variable.
- The SQL query is executed and it filters the data for the selected countries.





