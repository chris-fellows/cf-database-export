CREATE TABLE NewCustomerList(CustomerID INT, Name NVARCHAR(1000), Email NVARCHAR(100)) 

--The row template is inserted here for each row returned by the main query
##ROW_TEMPLATES##
 
DELETE FROM NewCustomerList WHERE Email IS NULL