<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
<html>
<body>
<h2>Customer List</h2>
<table border="1">
<tr bgcolor="#9acd32">
<th>Customer ID</th>
<th>Name</th>
<th>Email</th>
</tr>
<xsl:for-each select="//Row">
<tr>
<td><xsl:value-of select="CustomerID"/></td>
<td><xsl:value-of select="Name"/></td>
<td><xsl:value-of select="Email"/></td>
</tr>
</xsl:for-each>
</table>
</body>
</html>
</xsl:template>

</xsl:stylesheet>