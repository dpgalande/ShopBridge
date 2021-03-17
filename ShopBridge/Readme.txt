ShopBridge Demo How To Run

1) Open Visual Studio (2017) As Administrator Mode 

2) Open ShopBridge.sln file

4) Open DBScript Folder And Run Script On Sql Server (1.Create DataBase , 2.Create Table , 3.Insert ) 

7) Open Web.config File Form ShopBridge.WebAPI Projct 

8) Update connectionString In Web.config File  
	 <add name="ShopBridgeDbContext" connectionString="Server=<Server_Name>;Database=DB_ShopBridge;user id=<SQL_Server_UserName>;password=<SQL_Server_Password>;" providerName="System.Data.SqlClient" />
	 <SQL_Server_Name/SQL_Server_IP> 
	 <SQL_Server_UserName>
	 <SQL_Server_Password>
	 Example : <add name="ShopBridgeDbContext" connectionString="Server=192.168.1.100;Database=DB_ShopBridge;user id=sa;password=Admin@12345;" providerName="System.Data.SqlClient" />
9) Set ShopBridge.UI Project As Default Run

10) Clean And Build Solution 

11) Run Project 

Note (Web API And UI Project Run On Local IIS Right Click On UI and API Project = > Go To Web Tab And Select Local IIS From Server Field And Create Virtual Directory)
