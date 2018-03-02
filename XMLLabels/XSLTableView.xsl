<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html>
  <head>
    <title> <xsl:value-of select="Labels_Description/product/product_name"/> Forms Description </title> </head>
  <body>
    <h2> Клиент: <xsl:value-of select="Labels_Description/product/client_name"/> </h2>
    <xsl:for-each select="Labels_Description/product/forms/form">
		<h3> Име на формата: <xsl:value-of select="formname"/> </h3>
		<h4> Предназначение: <xsl:value-of select="description"/> </h4>
		<h4> Заглавие: <xsl:value-of select="header"/> </h4>
		<h4> Етикети:</h4>
		<table border="1" >
			<tr bgcolor="#9acd32">
				<th align="left">Програмно име на контролата</th>
				<th align="left">Оригинален текст</th>
				<th align="left">Клиентски текст</th>
				<th align="left">Оригинално описание</th>
				<th align="left">Клиентско описание</th>
				<th align="left">Тип в програмата</th>
				<th align="left">Помощен текст</th>
				<th align="left">SQL тип на променливата</th>
				<th align="left">Име на колона в SQL таблицата</th>
				<th align="left">SQL таблица</th>
				<th align="left">Асоциирана конторла</th>
				<th align="left">Максимален размер</th>
				<th align="left">Минимална стойност</th>
				<th align="left">Максимална стойност</th>
			</tr>
			<xsl:for-each select="labels/label">
				<tr>
					<td><xsl:value-of select="program_name"/></td>
					<td><xsl:value-of select="original_text"/></td>
					<td><xsl:value-of select="client_text"/></td>
					<td><xsl:value-of select="label_description"/></td>
					<td><xsl:value-of select="client_label_description"/></td>
					<td><xsl:value-of select="program_type"/></td>
					<td> <xsl:value-of select="tooltip"/></td>
					<td> <xsl:value-of select="SQL_variable_type"/></td>
					<td> <xsl:value-of select="SQL_column_name"/></td>
					<td> <xsl:value-of select="SQL_table"/></td>
					<td> <xsl:value-of select="associated_control"/></td>
					<td> <xsl:value-of select="max_size"/></td>					
					<td> <xsl:value-of select="max_value"/></td>
					<td> <xsl:value-of select="min_value"/></td>
				</tr>
			</xsl:for-each>
		</table>
    </xsl:for-each>
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>

  