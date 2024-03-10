<?xml version="1.0"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:key name="CustomerKey" match="//Row/CustomerID/text()" use="." />
<xsl:template match="/">
<html>
<body>
<h2>Orders by Customer</h2>
<xsl:for-each select="//Row/CustomerID/text()[generate-id() = generate-id(key('CustomerKey',.)[1])]">
   <xsl:variable name="customerid" select="."/>
   Customer ID: <xsl:value-of select="."/><br/>
   <table border="1">
      <tr>
         <th>Order ID</th>
         <th>Order Date</th>
         <th>Status</th>
         <th>Dispatch Date</th>
         <th>Item Count</th>
         <th>Total</th>
      </tr>
   <xsl:for-each select="//Row[CustomerID=$customerid]">
      <xsl:sort select="CustomerID"/>
      <tr>
         <td><xsl:value-of select="OrderID"/></td>
         <td><xsl:value-of select="OrderDate"/></td>
         <td><xsl:value-of select="Status"/></td>
         <td><xsl:value-of select="DispatchDate"/></td>
         <td><xsl:value-of select="ItemCount"/></td>
         <td><xsl:value-of select="OrderTotal"/></td>
      </tr>
   </xsl:for-each>
   </table>
<hr/>
</xsl:for-each>
</body>
</html>
</xsl:template>
</xsl:stylesheet>