<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <header>
        <h1>
          <xsl:value-of select="Labels_Description/product/product_name"/> Forms Description
        </h1>
      </header>
      <body>
        <h2>
          Клиент: <xsl:value-of select="Labels_Description/product/client_name"/>
        </h2>
        <xsl:for-each select="Labels_Description/product/forms/form">
          <h3>
            Име на формата: <xsl:value-of select="formname"/>
          </h3>
          <h4>
            Предназначение: <xsl:value-of select="description"/>
          </h4>
          <h4>
            Заглавие: <xsl:value-of select="header"/>
          </h4>
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
            </tr>
            <xsl:for-each select="labels/label">
              <xsl:choose>
                <xsl:when test="@important = 'true'">
                  <tr bgcolor="#aaff00">
                    <td>
                      <xsl:value-of select="program_name"/>
                    </td>
                    <td>
                      <xsl:value-of select="original_text"/>
                    </td>
                    <td>
                      <xsl:value-of select="client_text"/>
                    </td>
                    <td>
                      <xsl:value-of select="label_description"/>
                    </td>
                    <td>
                      <xsl:value-of select="client_label_description"/>
                    </td>
                    <td>
                      <xsl:value-of select="tooltip"/>
                    </td>
                  </tr>
                </xsl:when>
                <xsl:otherwise>
                  <tr >
                    <td>
                      <xsl:value-of select="program_name"/>
                    </td>
                    <td>
                      <xsl:value-of select="original_text"/>
                    </td>
                    <td>
                      <xsl:value-of select="client_text"/>
                    </td>
                    <td>
                      <xsl:value-of select="label_description"/>
                    </td>
                    <td>
                      <xsl:value-of select="client_label_description"/>
                    </td>
                    <td>
                      <xsl:value-of select="tooltip"/>
                    </td>
                  </tr>                  
                </xsl:otherwise>
              </xsl:choose>
            </xsl:for-each>
          </table>
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
