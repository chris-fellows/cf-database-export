declare @CustomerId int
declare @Countries table (ID int)

##SelectCustomer(prompt='Select customer',variable=@CustomerId,minitems=1,maxitems=1)##
##SelectCountry(prompt='Select countries',variable=@Countries,minItems=1,maxItems=9999)##

select C.Name AS CustomerName, O.*, 
	(select sum(OI.Quantity) from OrderItems.txt OI where OI.OrderID = O.OrderID) AS ItemCount,
	(select sum(OI.ItemCost * OI.Quantity) from OrderItems.txt OI where OI.OrderID = O.OrderID) AS OrderTotal
from Orders.txt O 
	INNER JOIN Customers.txt C ON C.CustomerID = O.OrderID 
order by C.CustomerID, O.OrderID