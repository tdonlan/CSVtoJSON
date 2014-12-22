CSVtoJSON
=========

CSVtoJSON is a tool to convert CSV files to JSON, using the column headings as key names.

To Use: Once compiled, call CSVtoJSON.exe <input csv file> <output json file>

Automatic conversions will be done based on data in CSV:

Integers: converted to integeters in JSON
"Null": converted to null in JSON
JSON string: converted to ojbect and exported as valid JSON, not wrapped in quotes.
