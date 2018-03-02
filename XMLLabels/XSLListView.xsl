<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <header><h1> <xsl:value-of select="Labels_Description/product/product_name"/> Forms Description</h1></header>
      <body>
        <h2> Клиент: <xsl:value-of select="Labels_Description/product/client_name"/> </h2>
       <xsl:apply-templates/>
       
      </body>
    </html>
  </xsl:template>

  <xsl:template match="form">
    <p>
      <xsl:apply-templates select="formname"/>
      <xsl:apply-templates select="description"/>
      <xsl:apply-templates select="labels/label"/>
    </p>
  </xsl:template>
  
  <xsl:template match="formname">
    <h4>
      Име на формата: <span style="color:#ff0000">
        <xsl:value-of select="."/>
      </span>
      <br />
    </h4>
  </xsl:template>
  
  <xsl:template match="description">
    Описание: <span style="color:#00ff00">
      <xsl:value-of select="."/>
    </span>
    <br />
  </xsl:template>

  <xsl:template match="label">
    <p>
      <xsl:apply-templates select="original_text"/>
      <xsl:apply-templates select="client_text"/>
      <xsl:apply-templates select="label_description"/>
      <xsl:apply-templates select="client_label_description"/>
    </p>
  </xsl:template>

  <xsl:template match="original_text">
    Оригинален текст: <span style="color:#00ff00">
      <xsl:value-of select="."/>
    </span>
    <br />
  </xsl:template>

  <xsl:template match="client_text">
    Клиентски текст: <span style="color:#00ff00">
      <xsl:value-of select="."/>
    </span>
    <br />
  </xsl:template>

  <xsl:template match="label_description">
    Оригинално описание: <span style="color:#00ff00">
      <xsl:value-of select="."/>
    </span>
    <br />
  </xsl:template>

  <xsl:template match="client_label_description">
    Клиентско описание: <span style="color:#00ff00">
      <xsl:value-of select="."/>
    </span>
    <br />
  </xsl:template>
  
  
</xsl:stylesheet>

