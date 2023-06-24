<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  version="3.0"
  xmlns:xs="http://www.w3.org/2001/XMLSchema"
  xmlns:mf="http://example.com/mf"
  exclude-result-prefixes="#all">

  <xsl:param name="cancellation-token" as="item()" required="yes"/>

  <xsl:template match="node()">
    <xsl:if test="mf:cancel($cancellation-token)">
      <xsl:message terminate="yes">Cancelled.</xsl:message>
    </xsl:if>
    <xsl:next-match/>
  </xsl:template>

</xsl:stylesheet>