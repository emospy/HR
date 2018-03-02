<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml"/>
  <xsl:template match="/">
    <xsl:apply-templates/>
  </xsl:template>

  <xsl:template match ="Labels_Description">
    <Labels_Description>
      <xsl:apply-templates select="product"/>      
    </Labels_Description>    
  </xsl:template>    

  <xsl:template match ="product">
    <product>
      <xsl:attribute name="prod_id"><xsl:value-of select="@prod_id"/></xsl:attribute>
      <product_name><xsl:value-of select="product_name"/></product_name>
      <version><xsl:value-of select="version"/></version>
      <client_name><xsl:value-of select="client_name"/></client_name>
      <xsl:apply-templates select="forms"/>
    </product>
  </xsl:template>

  <xsl:template match="forms">
    <forms><xsl:apply-templates select="form"/></forms>
  </xsl:template>

  <xsl:template match="form">
    <form>
      <xsl:attribute name ="form_id"><xsl:value-of select="@form_id"/> </xsl:attribute>
      <formname><xsl:value-of select="formname"/></formname>
      <description><xsl:value-of select="description"/></description>
      <header><xsl:value-of select="header"/></header>
      <xsl:apply-templates select="labels"/>
    </form>
  </xsl:template>

  <xsl:template match="labels">
    <labels><xsl:apply-templates select="label[@important = 'true']"/></labels>
  </xsl:template>
  
  <xsl:template match="label">
    <label>
      <xsl:attribute name="label_id"> <xsl:value-of select="@label_id"/> </xsl:attribute>
      <program_name><xsl:value-of select="program_name"/></program_name>
      <original_text><xsl:value-of select="original_text"/></original_text>
      <client_text><xsl:value-of select="client_text"/></client_text>
      <label_description><xsl:value-of select="label_description"/></label_description>
      <client_label_description><xsl:value-of select="client_label_description"/></client_label_description>
      <program_type><xsl:value-of select="program_type"/></program_type>
      <tooltip><xsl:value-of select="tooltip"/></tooltip>
    </label>
  </xsl:template>
</xsl:stylesheet>