<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="xml"/>
  <xsl:template match="/">
    <xmp>
      <Labels_Description>
        <product>
          <product_name> </product_name>
          <version> </version>
          <client_name> </client_name>
          <xsl:apply-templates/>
        </product>
      </Labels_Description>
    </xmp>
  </xsl:template>

  <xsl:template match ="database">
    <xsl:apply-templates select="tables"/>
  </xsl:template>

  <xsl:template match ="tables">
    <xsl:apply-templates select="table"/>
  </xsl:template>
  
  <xsl:template match="table">
    <form>
      <xsl:attribute name ="form_id">
        <xsl:value-of select="@form_id"/>
      </xsl:attribute>
      <formname> </formname>
      <description> </description>
      <header> </header>
      <xsl:apply-templates select="columns"/>
    </form>
  </xsl:template>

  <xsl:template match="columns">
    <labels>
      <xsl:apply-templates select="column"/>
    </labels>
  </xsl:template>

  <xsl:template match="column">
    <label>
      <xsl:attribute name="label_id"/>
      <program_name> </program_name>
      <original_text> </original_text>
      <client_text> </client_text>
      <label_description><xsl:value-of select="columndescription"/></label_description>
      <client_label_description><xsl:value-of select="columndescription"/></client_label_description>
      <program_type> </program_type>
      <tooltip><xsl:value-of select="columndescription"/></tooltip>
      <SQL_variable_type><xsl:value-of select="columntype"/> </SQL_variable_type>
      <SQL_column_name><xsl:value-of select="columnname"/> </SQL_column_name>
    </label>
  </xsl:template>
</xsl:stylesheet>