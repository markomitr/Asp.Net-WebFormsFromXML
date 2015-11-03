# Asp.Net-WebFormsFromXML
Generating Asp.Net WebForms from structured well defined XML documents

Project: Web platform for generating reports from ERP database
Developed and Designed by Marko Mitreski	
	
Problem/Request:	
Reports to be available on web	
	
Current situation:	
•	Reports only available on the Desktop App
•	No web platform
•	No service for generating reports
•	Most of developers are NOT familiar with web development and  asp.net environment  - mainly WinForms dev.	
	
Solution:	
•	Design and develop web platform with main focus/goals to be:
o	User friendly with UI and functionality same as Desktop app for the end users (no training needed)
o	 Easily configurable and customizable for the other developers – similar to the desktop environment 
	
	
Software solution key achievements:	
•	Generating WebForms from structured well defined XML documents
o	XML atributes
	Control Type
	UI appearance (visibility, font, size, group..)
	 RegEx constraints
•	Advanced authorization, Ajax and constraint settings for the web control define in SQL database table
•	 Procedure to parse and collect all the values from controls on the WebForm
o	Collect all the values in dictionary and access them with their Unique-ID -> Easy for .net WinForms dev
o	Define Interface which implements all the controls, to easy get the values which abstract the need for different approach to take value for different type of controls(TextField, RadioButtons UserControls, Overloaded Controls)
•	Generating Menu from XML
•	SOAP service for generating Crystal Report in PDF
•	Responsive web design 
•	Javasript AJAX and classes
•	Jquery

	
Toolset:	
Back-end: Asp.net | C# | XML | ADO.net | MSSQL | SOAP | WebService  
Front-end: Javascript | Jquery | AJAX | HTML | CSS | Responsive  Web

![Preview](https://github.com/markomitr/Asp.Net-WebFormsFromXML/blob/master/Diagram.png)
